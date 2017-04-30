using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour {

    protected GeneralSquare source;
    protected GeneralSquare target;
    public GameObject hitEffect;
    public float speed;
    public int amountEffect;

    public Rigidbody2D rig2D;
	// Use this for initialization
	void Start () {
        if (rig2D == null)
            rig2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void FixedUpdate()
    {
        Vector2 dist = target.transform.position - transform.position;
        if(dist.magnitude <= 0.1f)
        {
            ReachTarget();
        }
    }

    public void Go(GeneralSquare source, GeneralSquare target, float speed, int amountEffect)
    {
        this.source = source;
        this.target = target;
        this.speed = speed;
        this.amountEffect = amountEffect;

        rig2D.MovePosition(source.transform.position + new Vector3(0, 0, -0.25f));

        Vector2 dir = target.transform.position - source.transform.position;

        rig2D.velocity = dir.normalized * speed;
    }

    public virtual void ReachTarget()
    {
        if (hitEffect != null)
        {
           GameObject hit = Instantiate<GameObject>(hitEffect
                , target.transform.position + new Vector3(0, 0, -0.25f),
                hitEffect.transform.rotation);
            hit.SetActive(true);
            Destroy(hit, 1);
        }
        Destroy(gameObject);
    }
}
