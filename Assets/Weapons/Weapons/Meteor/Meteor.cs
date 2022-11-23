using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Weapon
{
    // Start is called before the first frame update    
    void Start()
    {
        reset();
    }

    // Update is called once per frame
    override public void charge(int progress) {
        base.show(true);
    }

    override public void cast(int progress) {
        if(progress < 21) {
            base.weapon.transform.position -= new Vector3(0, 5, 0);
            return;
        }

        if(progress < 22) {
            base.weapon.transform.position = base.weaponEndPosition;
            this.dealDamage(100);
            base.reset();
            return;
        }
    }

    public void dealDamage(int damage) { 
        Collider[] entities = Physics.OverlapSphere(base.damageArea.transform.position, base.damageArea.transform.Find("Cylinder").transform.localScale.x/2f, (1<<3));
        base.dealDamage(entities, damage);
    }

    public void targetTransform(Transform target, Vector3 offset) {
        this.transform.position = target.position + offset;
    }
}
