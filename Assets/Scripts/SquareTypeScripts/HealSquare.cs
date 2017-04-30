using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSquare : GeneralSquare {

    public int number_of_heal_targets = 1;

    public override void Start()
    {
        base.Start();
        gameObject.SetActive(false);
    }

    public override void LatedAct()
    {
        if (level == 3)
            ShootOnTarget(myPlayer.MainSquare);
           // myPlayer.MainSquare.Heal(attack + bonusAtk);


        for (int j = 0; j < number_of_heal_targets; j++)
        {
            GeneralSquare target = myPlayer.GiveRandomTarget(false);
            ShootOnTarget(target);
            //target.Heal(attack + bonusAtk);
        }
    }

    public override void ResetStatus()
    {
        base.ResetStatus();
        number_of_heal_targets = 1;
    }

    protected override void LevelUp()
    {
        number_of_heal_targets++;
        attack++;
        maxHealth++;
        health++;
        base.LevelUp();
    }
}
