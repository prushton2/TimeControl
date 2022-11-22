using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss1AI : AI
{
    

    //Weapons
    public Weapon Axe;

    public int AxeCooldown = 250;
    public int lastUsedAxe;


    new void Start() {
        base.enemyName = "Akirro, the Virgin";
        base.state = "idle";
        base.Start();
    }

    // Update is called once per frame
    new void Update() {

        

        if(base.state == "idle") {
            base.state = "walkNearPlayer";
        }

        if(Array.Find(base.interruptableStates, element => element == state) != null) { //if we arent in an uninterruptable state (so walking basically)

            //Look for opportunity to attack


            if(base.timeController.time%250 == 0) {//is it time to attack?

                if(Vector3.Distance(transform.position, base.player.transform.position) < 30) { //if player is close enough, do an axe slam
                    base.updateState("AxeAttack_Charge");
                } else {
                    //do another attack instead pogu (meteorite, gun, bomb)
                }

            }

        }


    }



    new void FixedUpdate() {

        if(base.state == "dead") {
            return;
        }

        if(base.healthPool.isDead) {
            Rigidbody rb = this.gameObject.transform.Find("Chest").gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb = this.gameObject.transform.Find("Head").gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb = this.gameObject.transform.Find("Left Arm").gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb = this.gameObject.transform.Find("Left Leg").gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb = this.gameObject.transform.Find("Right Arm").gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb = this.gameObject.transform.Find("Right Leg").gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            base.updateState("dead");
        }


        if(base.FixedUpdate() == 1) { //if the AI state handler cant handle a state, give it to this program
            switch(base.state) {
                case "AxeAttack_Charge":
                    executeAxeAttackCharge();
                    break;
                case "AxeAttack_Cast":
                    executeAxeAttackCast();
                    break;
                default:
                    break;
            }
        }



    }

    private void executeAxeAttackCharge() {
        base.stateProgress += 1;

        Axe.charge(base.stateProgress);

        if(base.stateProgress == 5) {
            base.updateState("AxeAttack_Cast");
        }
    }

    private void executeAxeAttackCast() {
        base.stateProgress += 1;

        Axe.cast(base.stateProgress);

        if(base.stateProgress == 25) {
            lastUsedAxe = base.timeController.time;
            Axe.reset();
            base.updateState("idle");
        }
    }

    

}