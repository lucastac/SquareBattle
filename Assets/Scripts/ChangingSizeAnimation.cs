using UnityEngine;
using System.Collections;

public class ChangingSizeAnimation : MonoBehaviour {

    public float sizeRange; //variação do tamanho
    public float speed;

    private float normalSize; //tamanho normal
    private int expanding = 1;
	

	// Use this for initialization
	void Start () {
        normalSize = transform.localScale.x;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.localScale = transform.localScale + new Vector3(expanding * speed * Time.deltaTime, expanding * speed * Time.deltaTime,0);

		if(transform.localScale.x <= normalSize)
			expanding=1;
		else if(transform.localScale.x >= normalSize + sizeRange)
			expanding = -1;
	

	}
}
