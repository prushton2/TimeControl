using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gun : MonoBehaviour
{

    public GameObject bulletOrigin;
    public GameObject bullet;
    public TimeController timeController;

    //YOU DEFINE THESE IN THE SUBCLASS
    public int damage = 0;
    public int magazine = 0;
    public int timeToRecharge = 0;
    public int minTimeToRecharge = 0;
    public int cooldown;
    public int range;
    public int bulletLifespan;
    public int discountIterator;

    //NOT THESE
    public int discountChargeTime = 0;

    public void Start() {
        bulletOrigin = GameObject.Find("Main Camera");
        this.timeController = GameObject.Find("Time Controller").GetComponent<TimeController>();
        cooldown = timeToRecharge;
    }

    void Update() {}


    public void FixedUpdate() {
        

        if(!timeController.isControlling) {
            if(canShoot()) {
                discountChargeTime -= 1;
            }
            cooldown -= 1;
        }


        cooldown = Math.Clamp(cooldown, 0, timeToRecharge);
        discountChargeTime = Math.Clamp(discountChargeTime, 0, timeToRecharge - minTimeToRecharge);

        if(timeToRecharge - cooldown >= bulletLifespan) {
            bullet.SetActive(false);
        }


    }

    public virtual GameObject fire() {
        if(!canShoot()) {
            return null;
        }

        cooldown = timeToRecharge - discountChargeTime;
        discountChargeTime += discountIterator;

        RaycastHit hit;
        RaycastHit bulletDestination;

        bullet.SetActive(true);

        if(Physics.Raycast(bullet.transform.position, transform.forward, out bulletDestination, this.range, ~(1<<8))) {
            bullet.transform.localScale = new Vector3(0, 0, bulletDestination.distance);
        } else {
            bullet.transform.localScale = new Vector3(0, 0, range);
        }

        if(Physics.Raycast(bulletOrigin.transform.position, transform.forward, out hit, this.range, ~(1<<8))) {
         
            if(hit.transform.gameObject != null) {
                Transform hitTransform = hit.transform;

                while(hitTransform.gameObject.GetComponent<HealthPool>() == null) { //ascend the ladder until there is a GO with health
                    hitTransform = hitTransform.parent;
                }

                if(hitTransform == null) {
                    return null;
                }

                hitTransform.gameObject.GetComponent<HealthPool>().dealDamage(damage);
                return hitTransform.gameObject;
            }
        }

        return null;

    }

    bool canShoot() {
        return cooldown == 0;
    }

}
