using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterSquare : GeneralSquare {
    public int number_of_attacks = 2;

    public override void Start()
    {
        base.Start();
        gameObject.SetActive(false);
    }

    public override void LatedAct()
    {
        for (int i = 0; i < number_of_attacks; i++)
            Invoke("LatedShoot", 0.5f * i);
    }

    private void LatedShoot()
    {
        base.LatedAct();
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
