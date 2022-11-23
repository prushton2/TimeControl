using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class is to be used as a superclass for weapons bosses use
public class Weapon : MonoBehaviour
{
 
    public Vector3 hidePos = new Vector3(0, -100, 0);

    //define these in the instance of your weapon subclass
    public Vector3 weaponStartPosition;
    public Vector3 weaponStartRotation;
    public Vector3 weaponEndPosition;
    public Vector3 weaponEndRotation;

    public Vector3 damageAreaPosition;
    public Vector3 damageAreaRotation;

    public GameObject weapon;
    public GameObject damageArea;
 
    protected void show(bool show) { //shows the damage area and the weapon
        if(show) {
            this.weapon.transform.localPosition = weaponStartPosition;
            this.weapon.transform.localRotation = Quaternion.Euler(weaponStartRotation);

            this.damageArea.transform.localPosition = damageAreaPosition;
            this.damageArea.transform.localRotation = Quaternion.Euler(damageAreaRotation);
        } else {
            this.weapon.transform.localPosition = hidePos;
            this.damageArea.transform.localPosition = hidePos;
        }
    }

    public virtual void dealDamage(Collider[] entities, int damage) { //feed a list of stuff to damage, and this will damage it
        foreach(Collider col in entities) {
            col.gameObject.GetComponent<HealthPool>().dealDamage(100);
            col.gameObject.GetComponent<HealthPool>().isInvincible = true;
        }

        foreach(Collider col in entities) {
            col.gameObject.GetComponent<HealthPool>().isInvincible = false;
        }
    }

    public virtual void reset() {
        show(false);
    }

    public virtual void charge(int progress) {} //create overrides for these that dictate how the attack works
    public virtual void cast(int progress) {}
}
