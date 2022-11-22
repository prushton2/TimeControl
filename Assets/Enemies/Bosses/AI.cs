using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class AI : MonoBehaviour
{

    public string state = "idle";
    public bool isEnabled = false;
    public int stateProgress = 0;
    public string enemyName;

    protected RNG rng;
    protected GameObject player;
    protected TimeController timeController;
    protected CharacterController characterController;
    protected HealthPool healthPool;

    protected string[] interruptableStates = {"idle", "walkNearPlayer"};

    protected Vector3 direction = Vector3.zero;
    protected int lastChangedTime = 0;

    protected void Start()
    {
        rng = GameObject.Find("GameController").GetComponent<RNG>();
        player = GameObject.Find("Player");
        timeController = GameObject.Find("Time Controller").GetComponent<TimeController>();
        characterController = GetComponent<CharacterController>();
        healthPool = GetComponent<HealthPool>();
    }

    // Update is called once per frame
    protected void Update() {}

    protected int FixedUpdate() {
        if(!timeController.isControlling && isEnabled) {
            return executeState(state);
        } else {
            return 0;
        }
    }

    protected int executeState(string state) {
        switch(state) {
            case "idle":
                break;
            case "walkNearPlayer":
                executeWalkNearPlayer();
                break;
            default:
                return 1;
        }
        return 0;
    }


    protected void executeWalkNearPlayer() {

        LookAt(player.transform);
        
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

    protected void LookAt(Transform direction) {
        this.transform.LookAt(direction);
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        this.transform.Find("Head").LookAt(direction);
    }

    public JObject getData() {
        return JObject.FromObject(new {
            state = this.state,
            isEnabled = this.isEnabled,
            stateProgress = this.stateProgress
        });
    }

    public void setData(JObject ai) {
        this.state = (string)ai["state"];
        this.isEnabled = (bool)ai["isEnabled"];
        this.stateProgress = (int)ai["stateProgress"];
    }

    public string stringify() {
        return "AI data: state: " + this.state + " isEnabled: " + this.isEnabled; 
    }

    public void updateState(string newState, bool resetProgress = true) {
        this.state = newState;
        if(resetProgress) {
            this.stateProgress = -1;
        }
    }

    public bool checkCooldown(int cooldown, int lastUsed) {
        return timeController.time - lastUsed > cooldown;
    }
}