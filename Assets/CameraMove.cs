using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
	public GameObject target = null;
	public Vector3 MoveVector{ set; get; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MoveVector = GetRotateInput ();
		Func<Vector3> RotateWithView = () => {
			var dir = transform.TransformDirection (MoveVector);
			// Debug.Log ("CameraMove RotateWithView dir:" + dir.ToString());
			return dir;
		};
		if (target != null) {
			// transform.LookAt (target.transform);
			MoveVector = RotateWithView ();
			// transform.RotateAround (target.transform.position, new Vector3 (0, 1, 0), Time.deltaTime * 15);
			transform.RotateAround(target.transform.position, MoveVector, Time.deltaTime * 50);
		}
	}

	Vector3 GetRotateInput(){
		var dir = new Vector3 ();
		dir.x = Input.GetAxisRaw ("Vertical");
		dir.y = Input.GetAxisRaw ("Horizontal");
		Debug.Log ("CameraMove GetRotateInput dir:" + dir.ToString());
		return dir;
	}
}
