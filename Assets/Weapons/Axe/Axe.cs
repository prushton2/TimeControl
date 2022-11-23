using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{
    // Start is called before the first frame update
    void Start() {
        reset();
    }

    override public void charge(int progress) {
        base.show(true);
    }

    override public void cast(int progress) {

        if(progress < 1) {
            
            base.weapon.transform.localPosition = base.weaponEndPosition;
            base.weapon.transform.localRotation = Quaternion.Euler(base.weaponEndRotation);
            
            dealDamage(100);

            return;
        }

        if(progress < 5) {
            base.reset();
            return;
        }

    }

    override public void dealDamage(int damage) {
        Collider[] entities = Physics.OverlapBox(base.damageArea.transform.position, new Vector3(17.5f, 0.05f, 5), base.damageArea.transform.rotation, (1<<3));

        foreach(Collider col in entities) {
            col.gameObject.GetComponent<HealthPool>().dealDamage(100);
            col.gameObject.GetComponent<HealthPool>().isInvincible = true;
        }

        foreach(Collider col in entities) {
            col.gameObject.GetComponent<HealthPool>().isInvincible = false;
        }
    
    }

}
