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
        if(progress < 10) {
            return;
        }

        if(progress < 20) {
            
            base.weapon.transform.localPosition = base.weaponEndPosition;
            base.weapon.transform.localRotation = Quaternion.Euler(base.weaponEndRotation);
            
            base.dealDamage(100);

            return;
        }

        if(progress < 25) {
            base.reset();
            return;
        }

    }
}
