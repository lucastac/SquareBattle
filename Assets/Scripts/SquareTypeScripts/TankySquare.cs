using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankySquare : GeneralSquare {

    protected override void LevelUp()
    {
        maxHealth += 3;
        health += 3;
        attack += 2;
        base.LevelUp();
    }
}
