using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskControll : MonoBehaviour {
    Animator anim;

    public void Reveal()
    {
        anim.SetBool("TextShow", true);
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
