using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkingStar : MonoBehaviour {
    public bool Shine;

	Animator anim;

	void Awake() {
        anim = GetComponent<Animator>();
        Shine = false;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void FixedUpdate()
    {
        anim.SetBool("Shine", Shine);
    }
}
