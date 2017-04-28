using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour {

    public float speed;
    public float lifeTime;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, speed * Time.deltaTime, 0);
	}
}
