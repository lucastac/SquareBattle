using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterSquare : GeneralSquare {
    public int number_of_attacks = 2;

    public override void Act()
    {
        for(int i = 0; i < number_of_attacks; i++)
            base.Act();
    }

    public override void ResetStatus()
    {
        base.ResetStatus();
        number_of_attacks = 2;
    }

    protected override void LevelUp()
    {
        attack++;
        number_of_attacks++;
        maxHealth += 2;
        health += 2;
        base.LevelUp();
    }
}
