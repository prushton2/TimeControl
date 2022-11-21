using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AI : AI
{

    new void Start() {
        base.state = "walkNearPlayer";
        base.Start();
    }

    // Update is called once per frame
    new void Update() {
        base.Update();
    }

    new void FixedUpdate() {

        // if(rng.Next(timeController.time)%15 == 0) {
        //     state = "walkNearPlayer"; //validStates[ rng.Next(timeController.time) % (validStates.Length-1) ];
        // }

        base.FixedUpdate();
    }

}