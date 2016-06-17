using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
    public float rayDistance;
    public float force;
    public GameObject crosshair;

    private Camera currentCamera;
    private Ray crossRay;
    private RaycastHit hit;
    private LineRenderer line;

	// Use this for initialization
	void Start () {
        currentCamera = this.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        crossRay = currentCamera.ScreenPointToRay(Input.mousePosition);
        crosshair.SetActive(Physics.Raycast(crossRay, out hit, rayDistance));
        if (crosshair.activeSelf) {
            crosshair.transform.position = hit.point + (hit.normal * 0.02f);
            crosshair.transform.forward = -hit.normal;
            if (Input.GetMouseButtonDown(0) && hit.collider.tag == "Sphere") {
                hit.rigidbody.AddForce(crossRay.direction * force, ForceMode.Acceleration);
            }
        }

    }
}
