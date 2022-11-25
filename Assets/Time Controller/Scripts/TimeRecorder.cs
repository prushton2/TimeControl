using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TimeRecorder : MonoBehaviour
{
    public TimeController timeController; 

    public bool savePosition = true;
    public bool loadPosition = true;
    public Transform attachedTransform;

    public bool saveAI = false;
    public bool loadAI = true;
    public AI attachedAI;

    public bool saveHealth = false;  
    public bool loadHealth = true;  
    public HealthPool attachedHealthPool;

    private RigidBody attachedRB;

    private bool oldUseGravity;
    private bool oldIsKinematic;
    private bool lastStatus = false;
    private List<Point> allPointsInTime;

    void Start()
    {
        //init stuff
        allPointsInTime = new List<Point>();
        timeController = GameObject.Find("Time Controller").GetComponent<TimeController>();
        attachedTransform = this.gameObject.GetComponent<Transform>();
        attachedRB = attachedTransform.gameObject.GetComponent<Rigidbody>()

        //remember variables that will change
        if(getrb() != null) {
            oldUseGravity  = attachedTransform.gameObject.GetComponent<Rigidbody>().useGravity;
            oldIsKinematic = attachedTransform.gameObject.GetComponent<Rigidbody>().isKinematic;
        }
        
        //add in default points to prevent errors with indexing
        allPointsInTime.Add(new Point(-1, attachedTransform, getrb(), null));
        allPointsInTime.Add(new Point(0, attachedTransform, getrb(), null));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {


        if(timeController.isControlling != lastStatus && savePosition) { //if the timeController just changed, we have some work regarding saving some stats

            if(!timeController.isControlling) { //if the timeController just stopped controlling
            
                Point determinedSpot = getPointAtTime(timeController.time); //apply the forces to the object
                
                if(getrb() != null) { //tries exist to prevent error spam for objects without a rigidbody
                    GetComponent<Rigidbody>().velocity = determinedSpot.velocity;
                    GetComponent<Rigidbody>().angularVelocity = determinedSpot.angularVelocity;

                    attachedTransform.gameObject.GetComponent<Rigidbody>().useGravity  = oldUseGravity; //reset these values
                    attachedTransform.gameObject.GetComponent<Rigidbody>().isKinematic = oldIsKinematic;
                }

            } else { //if it just took control
                if(getrb() != null) {
                    oldUseGravity = attachedTransform.gameObject.GetComponent<Rigidbody>().useGravity; //save these values so they can be changed non destructively
                    oldIsKinematic  = attachedTransform.gameObject.GetComponent<Rigidbody>().isKinematic;

                    attachedTransform.gameObject.GetComponent<Rigidbody>().useGravity = false; //set them tpo what they need to be
                    attachedTransform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }

        }

        lastStatus = timeController.isControlling; //how we know it was just changed

        if(timeController.isControlling) { //if the controller is in control

            Point determinedSpot = getPointAtTime(timeController.time); //go to the point the controller says
            if(loadPosition && savePosition) {
                attachedTransform.position = determinedSpot.position;
                attachedTransform.rotation = determinedSpot.rotation;
            }

            if(loadAI && saveAI) {
                attachedAI.setData(determinedSpot.ai);
            }

            if(loadHealth && saveHealth) {
                attachedHealthPool.setData(determinedSpot.healthPool);   
            }

            
        } else { //if it isnt in control

            while(allPointsInTime[allPointsInTime.Count-1].time > timeController.time) { //remove all points in the future
                allPointsInTime.RemoveAt(allPointsInTime.Count-1);
            }

            Point point = new Point(timeController.time, attachedTransform, getrb(), null);
            
            if(!savePosition) {
                point.position = new Vector3(0, 0, 0);
                point.rotation = new Quaternion();

                point.velocity = new Vector3(0, 0, 0);
                point.angularVelocity = new Vector3(0, 0, 0);
            }

            if(saveAI) {
                point.ai = attachedAI.getData();
            }

            if(saveHealth) {
                point.healthPool = attachedHealthPool.getData();
            }
                

            if(!allPointsInTime[allPointsInTime.Count-1].equals(point) ) { //if the object has moved, add the position to the list
                allPointsInTime.Add(point);
            }
        }



    }

    Point getPointAtTime(int time) {
        for(int i = 0; i<allPointsInTime.Count; i++) {
            if(time < allPointsInTime[i].time) {
                return allPointsInTime[i-1];
            }
        }
        return allPointsInTime[allPointsInTime.Count-1];
    }


    Rigidbody getrb() { //returns null if no rb
        return attachedRB;
    }

}
