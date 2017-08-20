using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
	public GameObject target = null;
	public int AngularVelocity = 50;
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
	}
	
	// Update is called once per frame
	void Update () {
		moveVector = GetRotateInput ();
		Func<Vector3> RotateWithView = () => {
			var dir = transform.TransformDirection (moveVector);
			return dir;
		};
		if (target != null) {
			moveVector = RotateWithView ();
			transform.RotateAround(target.transform.position, moveVector, Time.deltaTime * AngularVelocity);
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
}
