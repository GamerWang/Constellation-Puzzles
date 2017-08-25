using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    public GameObject Camera;
    public GameObject StartText;
    public GameObject EndText;
    public GameObject Constellation;
    public GameObject TextMask;
    public Vector3 StartRotation = new Vector3(15, -135, -15);

    public void Founded()
    {
        Constellation.GetComponent<ConstellationControl>().StartShine();
        TextMask.GetComponent<MaskControll>().Reveal();
        EndText.GetComponent<Animator>().enabled = true;
    }

    void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        Camera.GetComponent<CameraMove>().Rotate(StartRotation);
        StartText.GetComponent<Animator>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
