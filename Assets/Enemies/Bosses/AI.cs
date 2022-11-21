using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class AI : MonoBehaviour
{

    public string state = "idle";
    public bool isEnabled = false;

    private RNG rng;
    private GameObject player;
    private TimeController timeController;
    private CharacterController characterController;

    private string[] validStates = {"idle", "walkNearPlayer"};

    private Vector3 direction = Vector3.zero;
    private int lastChangedTime = 0;

    protected void Start()
    {
        rng = GameObject.Find("GameController").GetComponent<RNG>();
        player = GameObject.Find("Player");
        timeController = GameObject.Find("Time Controller").GetComponent<TimeController>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    protected void Update() {}

    protected void FixedUpdate() {
        if(!timeController.isControlling && isEnabled) {
            executeState(state);
        }
    }

    protected void executeState(string state) {
        switch(state) {
            case "idle":
                break;
            case "walkNearPlayer":
                executeWalkNearPlayer();
                break;
            default:
                break;
        }
    }


    protected void executeWalkNearPlayer() {

        this.transform.LookAt(player.transform);
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, this.transform.eulerAngles.z);

        this.transform.Find("Head").LookAt(player.transform);
        
        if(rng.Next(timeController.time)%10 == 0 || direction == Vector3.zero || timeController.time - lastChangedTime > 300) {
            
            Vector3[] result = {transform.forward, transform.right, -transform.forward, -transform.right};
            lastChangedTime = timeController.time;

            direction = result[rng.Next(timeController.time+1)%4];
        }


        if(Vector3.Distance(transform.position, player.transform.position) < 10) {
            characterController.Move(-transform.forward*.1f);
        } else if(Vector3.Distance(transform.position, player.transform.position) > 30) {
            characterController.Move(transform.forward*.1f);
        } else {
            characterController.Move(direction*.05f);
        }
    }

    public JObject getData() {
        return JObject.FromObject(new {
            state = this.state,
            isEnabled = this.isEnabled
        });
    }

    public void setData(JObject ai) {
        this.state = (string)ai["state"];
        this.isEnabled = (bool)ai["isEnabled"];
    }

    public string stringify() {
        return "AI data: state: " + this.state + " isEnabled: " + this.isEnabled; 
    }
}