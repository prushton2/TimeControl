using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
 
    public Vector3 hidePos = new Vector3(0, -100, 0);

    public Vector3 weaponStartPosition;
    public Vector3 weaponStartRotation;
    public Vector3 weaponEndPosition;
    public Vector3 weaponEndRotation;

    public Vector3 damageAreaPosition;
    public Vector3 damageAreaRotation;
    

    public GameObject weapon;
    public GameObject damageArea;
 
    protected void show(bool show) {
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

    public virtual void dealDamage(int damage) {
        Debug.Log("Dealt Damage");
    }

    public virtual void reset() {
        show(false);
    }

    public virtual void charge(int progress) {}
    public virtual void cast(int progress) {}
}
