using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gun : MonoBehaviour
{
    public TimeController timeController;

    public GameObject bulletOrigin;

    public int damage = 0;
    public int magazine = 0;
    public int timeToRecharge = 0;
    public int cooldown;

    public bool canShoot = true;

    void Start() {
        bulletOrigin = GameObject.Find("Main Camera");
        cooldown = timeToRecharge;
        this.timeController = GameObject.Find("Time Controller").GetComponent<TimeController>();
    }

    void Update() {
        canShoot = cooldown == 0;
    }

    void FixedUpdate() {
        cooldown = Math.Clamp(cooldown-1, 0, timeToRecharge);
    }

    public virtual void fire() {
        cooldown = timeToRecharge;
        RaycastHit hit;

        if(Physics.Raycast(bulletOrigin.transform.position, transform.forward, out hit, 100, ~(1<<8))) {
            if(hit.transform.gameObject != null) {
                Transform hitTransform = hit.transform;

                while(hitTransform.gameObject.GetComponent<HealthPool>() == null) { //ascend the ladder until there is a GO with health
                    hitTransform = hitTransform.parent;
                }

                if(hitTransform == null) {
                    return;
                }

                hitTransform.gameObject.GetComponent<HealthPool>().dealDamage(damage);
                Debug.Log("damage pogu");
            }
            Debug.Log(hit.transform.gameObject.name);
            Debug.Log("hit");
        }

        Debug.Log("boom");
    }

}
