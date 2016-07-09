using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
    public Transform target;
    public float distance;
    public float height;
    public float duration;
    public float smooth = 0.3f;

    private float yVelocity = 0.0f;
    private Vector3 _camPosition;
    private Ray camRay;
    private float currentDistance = 0.0f;
    void Start() {
        camRay = new Ray();
    }

	// Update is called once per frame
	void Update () {
        var layer = 1 << 3;
        camRay.origin = this.transform.position;
        camRay.direction = target.position - camRay.origin;
        if(Physics.Raycast(camRay, currentDistance, layer)) {
            currentDistance -= 0.5f;
        }
        else if(currentDistance < distance) {
            currentDistance += 0.5f;
        }
        /*
        _camPosition = target.position - currentDistance * target.forward + Vector3.up * height;
        this.transform.position = Vector3.Lerp(this.transform.position, _camPosition, Time.deltaTime * duration);
        this.transform.LookAt(target);
        */
        float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref yVelocity, smooth);
        _camPosition = target.position + Quaternion.Euler(0, yAngle, 0) * new Vector3(0, height, -distance);
        transform.position = _camPosition;
        transform.LookAt(target);
    }

    void OnDrawGizmos() {
        if (Physics.Raycast(camRay)) Gizmos.color = Color.red;
        else Gizmos.color = Color.white;
        Gizmos.DrawRay(camRay);
    }
}
