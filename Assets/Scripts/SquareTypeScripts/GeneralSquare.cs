using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralSquare : MonoBehaviour {
    public int maxHealth; //initial and the maximum amount of health of a square
    public int health; //actual amount of health of a square
    public int attack; //amount of attack of a square
    public int level; //actual level (start with 1 and max is 3)
    public int experience;//actual experience (2 for level 1-2 and 4 for level 2-3)
    private int BonusAtk = 0;
    public int bonusAtk {

        get
        {
            return this.BonusAtk;
        }

        set
        {
            this.BonusAtk = value;
            if (bonusAttackEffect != null)
            {
                if (bonusAtk > 0)
                    bonusAttackEffect.SetActive(true);
                else
                    bonusAttackEffect.SetActive(false);
            }

        }
    } //amount of extra attack until the end of turn
    public GameObject bonusAttackEffect; //particle that indicates when a bonus attack is activated

    public Player myPlayer;
    public GameObject popUpPrefab; //prefab for a popUp
    public GameObject attackProjectil; //also can be a healing projectil

    protected int initialMaxHealth; //maxHealth on level 1
    protected int initialAttack; //attack on level 1
    protected bool isDisabled = false;
    public Animator anim;
	// Use this for initialization
	public virtual void Start () {
        initialMaxHealth = maxHealth;
        initialAttack = attack;
        anim = GetComponentInChildren<Animator>();
        bonusAtk = 0;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
	}

    public void Act()
    {
        if (isDisabled) return;

        Invoke("LatedAct", 0.5f);
    }

    public virtual void LatedAct()
    {

        GeneralSquare target = myPlayer.enemyPlayer.GiveRandomTarget();

        ShootOnTarget(target);

        //target.ApplyDamage(attack + bonusAtk);
    }

    public void ShootOnTarget(GeneralSquare target)
    {
        GameObject newProjectil = Instantiate<GameObject>(attackProjectil);
        Projectil pro = newProjectil.GetComponent<Projectil>();

        pro.Go(this, target, 3, attack + bonusAtk);
    }

    public virtual void ResetStatus()
    {
        maxHealth = initialMaxHealth;
        attack = initialAttack;
        health = maxHealth;
        level = 1;
        experience = 0;
        SetDisabled(false);
    }

    public virtual void ApplyDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            gameObject.SetActive(false);

        GameObject popup = Instantiate<GameObject>(popUpPrefab, transform.position, Quaternion.identity);
        Text t = popup.GetComponentInChildren<Text>();
        t.text = "-" + damage;
        t.color = Color.red;
    }

    public virtual void Heal(int amountHeal)
    {
        health = Mathf.Min(maxHealth, health + amountHeal);

        GameObject popup = Instantiate<GameObject>(popUpPrefab, transform.position, Quaternion.identity);
        Text t = popup.GetComponentInChildren<Text>();
        t.text = "+" + amountHeal;
        t.color = Color.green;
    }

    public bool IsAlive()
    {
        return health > 0 && gameObject.activeInHierarchy;
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

    public virtual void SetDisabled(bool disable)
    {
        isDisabled = disable;
        if(anim != null) anim.enabled = !isDisabled;
    }

    
}
