using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealProjectil : Projectil {

    public override void ReachTarget()
    {
        target.Heal(amountEffect);
        base.ReachTarget();
    }
}
