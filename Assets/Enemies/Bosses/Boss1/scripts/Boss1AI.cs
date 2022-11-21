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

        base.state = "idle";
        base.Start();
    }

    // Update is called once per frame
    new void Update() {

        if(base.state == "idle") {
            base.state = "walkNearPlayer";
        }

        if(Array.Find(base.interruptableStates, element => element == state) != null) {

            //Look for opportunity to attack

            if(Vector3.Distance(transform.position, base.player.transform.position) < 30 && base.checkCooldown(AxeCooldown, lastUsedAxe)) {
                base.updateState("AxeAttack_Charge");

            }

        }
    }



    new void FixedUpdate() {
        //some code to update the state (no clue how to do this part)




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