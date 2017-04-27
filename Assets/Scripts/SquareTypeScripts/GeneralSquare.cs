using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSquare : MonoBehaviour {
    public int maxHealth; //initial and the maximum amount of health of a square
    public int health; //actual amount of health of a square
    public int attack; //amount of attack of a square
    public int level; //actual level (start with 1 and max is 3)
    public int experience;//actual experience (2 for level 1-2 and 4 for level 2-3)

    public Player myPlayer;
    public Player enemyPlayer;

    protected int initialMaxHealth; //maxHealth on level 1
    protected int initialAttack; //attack on level 1
	// Use this for initialization
	public virtual void Start () {
        initialMaxHealth = maxHealth;
        initialAttack = attack;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
	}

    public virtual void Act()
    {
        int i;
        GeneralSquare target;
        if (enemyPlayer.HasCombatSquare())
        {
            do
            {
                i = Random.Range(0, enemyPlayer.mySquares.Count);
            } while (enemyPlayer.mySquares[i].gameObject.activeInHierarchy);

            target = enemyPlayer.mySquares[i];
        }
        else
            target = enemyPlayer.MainSquare;


        target.ApplyDamage(attack);
    }

    public virtual void ResetStatus()
    {
        maxHealth = initialMaxHealth;
        attack = initialAttack;
        health = maxHealth;
        level = 1;
        experience = 0;
    }

    public virtual void ApplyDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            gameObject.SetActive(false);
    }

    public virtual void Heal(int amountHeal)
    {
        health = Mathf.Min(maxHealth, health + amountHeal);
    }

    public virtual void EarnExperience(int xp)
    {
        if(level < 3) experience += xp; //do not earn xp on max level

        if (experience >= 2 && level == 1 || experience >= 4 && level == 2)
            LevelUp();

    }

    protected virtual void LevelUp()
    {
        level++;
        if (level == 2)
        {
            experience -= 2;
            EarnExperience(0);
        }
        else experience = 0;
    }

    
}
