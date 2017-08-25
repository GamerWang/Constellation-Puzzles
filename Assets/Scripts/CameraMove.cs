using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
	public GameObject target = null;
    public GameObject Manager;
	public int AngularVelocity = 50;
	public float resetVelocity = 0.3f;
	public float resetAngularVelocity = 2.0f;
	public Vector3 moveVector{ set; get; }
	public bool Controllable{ get; private set;}

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

    public void Rotate(Vector3 startRotation)
    {
        if (target != null)
        {
            transform.RotateAround(target.transform.position, new Vector3(0, 1, 0), startRotation.y);
            transform.RotateAround(target.transform.position, new Vector3(0, 0, 1), startRotation.z);
            transform.RotateAround(target.transform.position, new Vector3(1, 0, 0), startRotation.x);
        }
    }

	// Use this for initialization
	void Start () {
		Controllable = true;
	}
	
	// Update is called once per frame
	void Update () {
		moveVector = GetRotateInput ();
		Func<Vector3> RotateWithView = () => {
			var dir = transform.TransformDirection (moveVector);
			return dir;
		};
		if (target != null) {
			if (Controllable) {
				moveVector = RotateWithView ();
				transform.RotateAround (target.transform.position, moveVector, Time.deltaTime * AngularVelocity);
				if (!IsControllable ()) {
					Controllable = IsControllable ();
                    // action here
                    Manager.GetComponent<SceneManager>().Founded();
				}
			} else {
				if (!IsPositionReset ()) {
					transform.RotateAround (target.transform.position, GetAxis (), Time.deltaTime * resetVelocity);
				} 
				if (!IsRotationReset ()) {
					var rotationZ = transform.eulerAngles.z;
					if (rotationZ > 180)
						rotationZ -= 360;
					var direction = rotationZ > 0 ? -1 : 1;
                    
					transform.Rotate (0, 0, direction * Time.deltaTime * resetAngularVelocity);
				}
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

	bool IsControllable(){
		var r = transform.eulerAngles;
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
		return !(v.x <= 1.5 && v.y <= 1.5 && v.z <= 5);
	}

	bool IsDefaultPos(){
		var r = transform.eulerAngles;
		var p = transform.position;
		bool isRotationReset = (r.x == 0 && r.y == 0 && r.z == 0);
		bool isPositionReset = (p.x == 0 && p.y == 0 && p.z == 0);
		return isRotationReset && isPositionReset;
	}

	bool IsPositionReset(){
		var p = transform.position;
		bool isPositionReset = (Math.Abs (p.x) <= 0.06 && Math.Abs (p.y) <= 0.06 && Math.Abs (p.z + 10) <= 0.01);
		return isPositionReset;
	}

	bool IsRotationReset(){
		var r = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
		if (r.x > 180)
			r.x -= 360;
		if (r.y > 180)
			r.y -= 360;
		if (r.z > 180)
			r.z -= 360;
		r.x = Math.Abs (r.x);
		r.y = Math.Abs (r.y);
		r.z = Math.Abs (r.z);
		bool isRotationReset = (r.x <= 0.5 && r.y <= 0.5 && r.z <= 0.1);
		return isRotationReset;
	}

	Vector3 GetAxis(){
		var aimPos = new Vector3 (0, 0, -10);
		var currentPos = transform.position;
        // Bellow is the former get cross function
		// var axisX = currentPos.y * aimPos.z - currentPos.z * aimPos.y;
		// var axisY = currentPos.z * aimPos.x - currentPos.x * aimPos.z;
		// var axisZ = currentPos.x * aimPos.y - currentPos.y * aimPos.x;
		// var axis = new Vector3 (axisX, axisY, axisZ);
        var axis = Vector3.Cross(currentPos, aimPos);
		return axis;
	}
}
