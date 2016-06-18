using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    public float velocidad;

    private SphereCoordinate polar;
	// Use this for initialization
	void Start () {
        polar = new SphereCoordinate(this.transform.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {


        polar.UpdateVar();
	}
}
