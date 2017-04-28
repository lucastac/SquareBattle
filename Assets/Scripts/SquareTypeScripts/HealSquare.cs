using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSquare : GeneralSquare {

    public int number_of_heal_targets = 1;

    public override void LatedAct()
    {
        if (level == 3)
            myPlayer.MainSquare.Heal(attack);


        for (int j = 0; j < number_of_heal_targets; j++)
        {
            int i;
            do
            {
                i = Random.Range(0, myPlayer.mySquares.Count);
            } while (!myPlayer.mySquares[i].gameObject.activeInHierarchy);

            myPlayer.mySquares[i].Heal(attack);
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
