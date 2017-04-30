using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour {

    private void ProcessUnityJackpot(int[] result)
    {
        for (int i = 0; i < result.Length; i++)
        {
            if (mySquares[result[i]].IsAlive())
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
        int tierDrain = 0, tierBarrier = 0, tierBonusAtk = 0;

        foreach (int i in result)
        {
            if (i == 0)
                tierDrain++;
            else if (i == 1)
                tierBarrier++;
            else
                tierBonusAtk++;
        }

        bool[] called = { false, false, false };

        for (int i = 0; i < result.Length; i++)
        {
            if (result[i] == 0 && !called[0])
            {
                called[0] = true;
                DrainUtility(tierDrain);
            }
            else if (result[i] == 1 && !called[1])
            {
                called[1] = true;
                BarrierUtility(tierBarrier);
            }
            else if (result[i] == 2 && !called[2])
            {
                called[2] = true;
                BonusAtkUtility(tierBonusAtk);
            }
        }
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

        for (int i = 0; i < result.Length; i++)
        {
            if (result[i] == 0 && !called[0])
            {
                called[0] = true;
                AreaOffensive(tierArea);
            }
            else if (result[i] == 1 && !called[1])
            {
                called[1] = true;
                BlockOffensive(tierBlock);
            }
            else if (result[i] == 2 && !called[2])
            {
                called[2] = true;
                DirectOffensive(tierDirect);
            }
        }
    }

    

    ///////////////Utilities jackpot Effects//////////////////////////

    private void DrainUtility(int tier)
    {
        int drainValue = tier + 1;

        for (int j = 0; j < tier; j++)
        {
            GeneralSquare targetEnemy = enemyPlayer.GiveRandomTarget();
            GeneralSquare targetAlly = GiveRandomTarget(false);

            LatedDamageOnTarget(targetEnemy, drainValue);
            //targetEnemy.ApplyDamage(drainValue);
            targetAlly.Heal(drainValue);
        }


    }

    private void BarrierUtility(int tier)
    {
        if(!HasBarrier())
        {

            Barrier.gameObject.SetActive(true);
            Barrier.ResetStatus();
            tier--;
        }

        Barrier.EarnExperience(4 * tier);//1 level per tier
    }

    private void BonusAtkUtility(int tier)
    {
        foreach(GeneralSquare gs in mySquares)
        {
            if (gs.IsAlive())
                gs.bonusAtk = tier;
        }
    }

    ///////////////Offensives jackpot Effects//////////////////////// 

    private void AreaOffensive(int tier)
    {
        int damage = 2 * tier;
        if(enemyPlayer.HasBarrier())
        {
            LatedDamageOnTarget(enemyPlayer.Barrier, damage);

            //enemyPlayer.Barrier.ApplyDamage(damage);
        }
        else if (enemyPlayer.HasCombatSquare())
        {
            foreach (GeneralSquare gs in enemyPlayer.mySquares)
            {
                if (gs.IsAlive())
                    LatedDamageOnTarget(gs, damage);
                //gs.ApplyDamage(damage);
            }
        }
        else
        {
            LatedDamageOnTarget(enemyPlayer.MainSquare, damage);
            //enemyPlayer.MainSquare.ApplyDamage(damage);
        }
    }

   


    private void BlockOffensive(int tier)
    {
        for (int j = 0; j < tier; j++)
        {
            GeneralSquare target = enemyPlayer.GiveRandomTarget(false);
            if (target == enemyPlayer.MainSquare) return;

            target.SetDisabled(true);
        }
    }

    private void DirectOffensive(int tier)
    {
        int damage = 2 * tier;
        if (enemyPlayer.HasBarrier())
            LatedDamageOnTarget(enemyPlayer.Barrier, damage);
        //enemyPlayer.Barrier.ApplyDamage(damage);
        else
            LatedDamageOnTarget(enemyPlayer.MainSquare, damage);
        //enemyPlayer.MainSquare.ApplyDamage(damage);
    }

    private void LatedDamageOnTarget(GeneralSquare target,int damage)
    {
        Destroy(Instantiate<GameObject>(directDamageParticlePrefab,
                target.transform.position + new Vector3(0, 0, -0.25f),
                directDamageParticlePrefab.transform.rotation), 1);

        StartCoroutine(WaitToDamageOnTarget(target, damage, 0.5f));
    }

    IEnumerator WaitToDamageOnTarget(GeneralSquare target, int damage, float delayTime)
    {

        yield return new WaitForSeconds(delayTime);

        target.ApplyDamage(damage);

    }
}
