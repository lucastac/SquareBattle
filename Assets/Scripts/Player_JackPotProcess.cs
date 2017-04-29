using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour {

    private void ProcessUnityJackpot(int[] result)
    {
        for (int i = 0; i < result.Length; i++)
        {
            if (mySquares[result[i]].gameObject.activeInHierarchy)
                mySquares[result[i]].EarnExperience(1);
            else
            {
                mySquares[result[i]].ResetStatus();
                mySquares[result[i]].gameObject.SetActive(true);
            }
        }
    }

    private void ProcessUtilityJackpot(int[] result)
    {

    }

    private void ProcessOffensiveJackpot(int[] result)
    {
        int tierArea = 0, tierBlock = 0, tierDirect = 0;

        foreach (int i in result)
        {
            if (i == 0)
                tierArea++;
            else if (i == 1)
                tierBlock++;
            else
                tierDirect++;
        }

        bool[] called = { false, false, false };

        for(int i = 0; i < result.Length; i++)
        {
            if(result[i] == 0 && !called[0])
            {
                called[0] = true;
                AreaOffensive(tierArea);
            }else if (result[i] == 1 && !called[1])
            {
                called[1] = true;
                BlockOffensive(tierBlock);
            }else if (result[i] == 2 && !called[2])
            {
                called[2] = true;
                DirectOffensive(tierDirect);
            }
        }
    }

    ///////////////Utilities jackpot Effects//////////////////////////

    ///////////////Offensives jackpot Effects//////////////////////// 

    private void AreaOffensive(int tier)
    {
        int damage = 2 * tier;
        if (enemyPlayer.HasCombatSquare())
        {
            foreach (GeneralSquare gs in enemyPlayer.mySquares)
            {
                if (gs.gameObject.activeInHierarchy)
                    gs.ApplyDamage(damage);
            }
        }
        else
        {
            enemyPlayer.MainSquare.ApplyDamage(damage);
        }
    }

    private void BlockOffensive(int tier)
    {
        for (int j = 0; j < tier; j++)
        {
            int i;
            if (enemyPlayer.HasCombatSquare())
            {
                do
                {
                    i = Random.Range(0, enemyPlayer.mySquares.Count);
                } while (!enemyPlayer.mySquares[i].gameObject.activeInHierarchy);

                enemyPlayer.mySquares[i].SetDisabled(true);
            }
            else
                return;
        }
    }

    private void DirectOffensive(int tier)
    {
        int damage = 2 * tier;
        enemyPlayer.MainSquare.ApplyDamage(damage);
    }
}
