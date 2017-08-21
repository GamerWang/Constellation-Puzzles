using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
	public GameObject target = null;
	public int AngularVelocity = 50;
	public Vector3 StartRotation = new Vector3 (15, -135, -15);
	public Vector3 moveVector{ set; get; }

	MouseData mouseData = new MouseData();

	class MouseData{
		public Vector2 startPosition = new Vector2 (-1, -1);
		public Vector2 currentPosition = new Vector2 (-1, -1);

		public void mouseStart(Vector2 startPos){
			startPosition = startPos;
		}

		public void mouseMove(Vector2 currentPos){
			currentPosition = currentPos;
		}
		public void mouseEnd(){
			startPosition = new Vector2 (-1, -1);
			currentPosition = new Vector2 (-1, -1);
		}
		public Vector3 getDir(){
			var dir = new Vector3 ();
			dir.x = -(currentPosition.y - startPosition.y);
			dir.y = currentPosition.x - startPosition.x;
			return dir;
		}
	}

	// Use this for initialization
	void Start () {
		if (target != null) {
			transform.RotateAround(target.transform.position, new Vector3(0,1,0), StartRotation.y);
			transform.RotateAround(target.transform.position, new Vector3(0,0,1), StartRotation.z);
			transform.RotateAround(target.transform.position, new Vector3(1,0,0), StartRotation.x);
		}
	}
	
	// Update is called once per frame
	void Update () {
		moveVector = GetRotateInput ();
		Func<Vector3> RotateWithView = () => {
			var dir = transform.TransformDirection (moveVector);
			return dir;
		};
		if (target != null) {
			if (Controllable()) {
				moveVector = RotateWithView ();
				transform.RotateAround(target.transform.position, moveVector, Time.deltaTime * AngularVelocity);
			}
		}
	}

	Vector3 GetRotateInput(){
		var dir = new Vector3 ();
		if (Input.GetMouseButtonDown (0)) {
			mouseData.mouseStart (Input.mousePosition);
		}
		if (Input.GetMouseButton (0)) {
			mouseData.mouseMove (Input.mousePosition);
		} else {
			mouseData.mouseEnd ();
		}
		dir = mouseData.getDir ();

		if (Input.GetAxisRaw ("Vertical") != 0 || Input.GetAxisRaw ("Horizontal") != 0) {
			dir.x = -Input.GetAxisRaw ("Vertical");
			dir.y = Input.GetAxisRaw ("Horizontal");
		}
		return dir;
	}

	bool Controllable(){
		var r = transform.eulerAngles;
		// var v = new Vector3 (Math.Abs(r.x), Math.Abs(r.y), Math.Abs(r.z));
		var v = new Vector3 (r.x,r.y, r.z);
		if (v.x > 180)
			v.x -= 360;
		if (v.y > 180)
			v.y -= 360;
		if (v.z > 180)
			v.z -= 360;
		v.x = Math.Abs (v.x);
		v.y = Math.Abs (v.y);
		v.z = Math.Abs (v.z);
		Debug.Log ("CameraMove Controllable : "+(v.x <= 1 && v.y <= 1 && v.z <= 5));
		return !(v.x <= 1 && v.y <= 1 && v.z <= 5);
	}
}
