using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationControl : MonoBehaviour {
    Animator anim;

    public void StartShine()
    {
        anim.SetBool("Shine", true);
    }

    public void StopShine()
    {
        anim.SetBool("Shine", false);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
