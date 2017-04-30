using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProjectil : Projectil {


    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(!target.IsAlive())
        {
            ChangeTarget();
        }
    }

    public override void ReachTarget()
    {
        target.ApplyDamage(amountEffect);
        base.ReachTarget();
    }

    void ChangeTarget()
    {
        GeneralSquare newtarget = target.myPlayer.GiveRandomTarget();
        target = newtarget;

        Vector2 dir = target.transform.position - transform.position;

        rig2D.velocity = dir.normalized * speed;
    }

}
