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
    public float runMultiplier = 1.25f;
    public float jumpSpeed = 0.16f;
    public float gravity = 1f;
    public int life = 2;
    public float deltaX;
    private Vector3 moveDirection = Vector3.zero;
    public bool moving = false;
    public float Sensitivity = 1.15f;

    public bool rewindingDeath = false;

    void Start() {
        timeController = transform.Find("Time Controller").GetComponent<TimeController>();
        characterController = GetComponent<CharacterController>();
        cameraTimeRecorder = this.transform.Find("Main Camera").GetComponent<TimeRecorder>();
    }

    void Update()
    {

        if(rewindingDeath) {
            return;
        }

        deltaX += Input.GetAxisRaw("Mouse X");
        transform.rotation = Quaternion.Euler(0, Sensitivity*deltaX, 0);

        moving = false;
        if (Input.GetKey("w")) {
            if (Input.GetKey("left shift")) {
                characterController.Move(new Vector3(transform.forward.x*speed*runMultiplier*Time.deltaTime, moveDirection.y, transform.forward.z*speed*runMultiplier*Time.deltaTime));
                moving = true;
            }
            else {
                characterController.Move(new Vector3(transform.forward.x*speed*Time.deltaTime, moveDirection.y, transform.forward.z*speed*Time.deltaTime));
                moving = true;
            }
        }
        if (Input.GetKey("s")) {
            characterController.Move(new Vector3(-1*transform.forward.x*speed*Time.deltaTime, moveDirection.y, -1*transform.forward.z*speed*Time.deltaTime));
            moving = true;
        }
        if (Input.GetKey("d")) {
            characterController.Move(new Vector3(transform.right.x*speed*Time.deltaTime, moveDirection.y, transform.right.z*speed*Time.deltaTime));
            moving = true;
        }
        if (Input.GetKey("a")) {
            characterController.Move(new Vector3(-1*transform.right.x*speed*Time.deltaTime, moveDirection.y, -1*transform.right.z*speed*Time.deltaTime));
            moving = true;
        }
        if(!moving) {
            characterController.Move(new Vector3(0, moveDirection.y, 0));
        }


        if (!characterController.isGrounded) { //This code modifies the accumulator moveDirection.y, allowing jumping and falling properly
            moveDirection.y -= gravity*Time.deltaTime;
        }
        else {// (characterController.isGrounded) {
            moveDirection.y = 0;
        }
        if (characterController.isGrounded && Input.GetKey("space")) {//Input.GetKey(this.GetComponent<Config>().Jump)) {
            moveDirection.y = jumpSpeed;
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

