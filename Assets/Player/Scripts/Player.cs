using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    public CharacterController characterController;
    public TimeController timeController;
    public TimeRecorder timeRecorder;
    public TimeRecorder cameraTimeRecorder;
    public HealthPool healthPool;

    public float speed = 7f;
    public float runMultiplier = 1.5f;

    public float jumpSpeed = 0.16f;
    public float gravity = 1f;

    public float deltaX;
    private Vector3 moveDirection = Vector3.zero;

    public float Sensitivity = 1.15f;

    public bool rewindingDeath = false;

    void Start() {
        timeController = transform.Find("Time Controller").GetComponent<TimeController>();
        cameraTimeRecorder = this.transform.Find("Main Camera").GetComponent<TimeRecorder>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        if(rewindingDeath) { //needs to be changed sometime soon (its cringe af)
            return;
        }

        deltaX += Input.GetAxisRaw("Mouse X");
        transform.rotation = Quaternion.Euler(0, Sensitivity*deltaX, 0);

        moveDirection.x = 0;
        moveDirection.z = 0;

        if (Input.GetKey("w")) {
            moveDirection.x += transform.forward.x*speed*Time.deltaTime;
            moveDirection.z += transform.forward.z*speed*Time.deltaTime;
        }
        if (Input.GetKey("s")) {
            moveDirection.x += -transform.forward.x*speed*Time.deltaTime;
            moveDirection.z += -transform.forward.z*speed*Time.deltaTime;
        }
        if (Input.GetKey("d")) {
            moveDirection.x += transform.right.x*speed*Time.deltaTime;
            moveDirection.z += transform.right.z*speed*Time.deltaTime;
        }
        if (Input.GetKey("a")) {
            moveDirection.x += -transform.right.x*speed*Time.deltaTime;
            moveDirection.z += -transform.right.z*speed*Time.deltaTime;
        }
        
        if(Input.GetKey("left shift")) {
            moveDirection = new Vector3(moveDirection.x * runMultiplier, moveDirection.y, moveDirection.z * runMultiplier);
        }

        characterController.Move(moveDirection);

        if (!characterController.isGrounded) { //This code modifies the accumulator moveDirection.y, allowing jumping and falling properly
            moveDirection.y -= gravity*Time.deltaTime;
        } else {// (characterController.isGrounded) {
            moveDirection.y = 0;

            if(Input.GetKey("space")) {
                moveDirection.y = jumpSpeed;
            }
        }

        
        if(Input.GetKeyDown("c")) {
            timeController.setIsControlling(!timeController.isControlling);
        }


    }

    void FixedUpdate() {

        if(Input.GetKey("q")) {
            timeController.IncrementTime(-2);
        }
        if(Input.GetKey("e")) {
            timeController.IncrementTime(2);
        }

        if(this.healthPool.isDead || rewindingDeath) {
            rewindingDeath = true;
            
            this.timeRecorder.loadPosition = true;
            this.cameraTimeRecorder.loadPosition = true;

            this.timeController.setIsControlling(true);
            this.timeController.time -= 10;
            
            if(this.timeController.time <= 0) {
                this.timeRecorder.loadPosition = false;
                this.cameraTimeRecorder.loadPosition = false;
                rewindingDeath = false;
                this.timeController.setIsControlling(false);
            }
        }

    }

}

