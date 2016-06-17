using UnityEngine;
using System.Collections;

public class BallSelector : MonoBehaviour {
    public float rayDistance;
    public float force;

    private Camera currentCamera;
    private Ray mouseRay;
    private RaycastHit hit;
	// Use this for initialization
	void Start () {
        currentCamera = this.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            LaunchRay();
        }
	}

    private void LaunchRay() {
        mouseRay = currentCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(mouseRay, out hit, rayDistance)) {
            if(hit.collider.tag == "Sphere") hit.rigidbody.AddForce(this.transform.forward * force, ForceMode.Acceleration);
        }
    }
}
