using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    public float speedTranslation;
    public float speedRotation;

    private Transform childCamera;
    private float axisH, axisV;
    private float angleX;
    private bool idle;
    private Rigidbody rb;


    // Use this for initialization
    void Start () {
        childCamera = this.transform.GetChild(0);
        idle = true;
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        axisV = Input.GetAxis("Vertical");
        axisH = Input.GetAxis("Horizontal");

        rb.MovePosition(rb.position + 
            ((childCamera.forward * axisV * Time.deltaTime * speedTranslation) +
            (childCamera.right * axisH * Time.deltaTime * speedTranslation)) * (Input.GetKey(KeyCode.LeftShift) ? 1.5f : 1.0f)) ;


        childCamera.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * speedRotation, Space.World);
        
        childCamera.Rotate(this.transform.right, -Input.GetAxis("Mouse Y") * Time.deltaTime * speedRotation);
        angleX = childCamera.localEulerAngles.x;
        angleX = (angleX > 180.0f) ? angleX - 360 : angleX;
        angleX = Mathf.Clamp(angleX, -45.0f, 45.0f);
        childCamera.localEulerAngles = new Vector3(angleX, childCamera.localEulerAngles.y, childCamera.localEulerAngles.z);


        if (Input.GetKeyDown(KeyCode.Space)) {
            //this.transform.position += new Vector3(0.0f, 10.0f * Time.deltaTime, 0.0f);
            rb.AddForce(this.transform.up * 200.0f);
        }

        if((axisV != 0.0f || axisH != 0.0f) && idle) {
            idle = false;
            //this.transform.position += new Vector3(0.0f, 1.5f * Time.deltaTime, 0.0f);
            StartCoroutine("Walk");
        }

	}

    IEnumerator Walk() {
        childCamera.Translate(this.transform.up * 0.02f);
        while (axisV != 0.0f || axisH != 0.0f) {
            yield return new WaitForSeconds(0.2f);
            childCamera.Translate(this.transform.up * -0.04f);
            yield return new WaitForSeconds(0.2f);
            childCamera.Translate(this.transform.up * 0.04f);   
        }
        idle = true;
        childCamera.Translate(this.transform.up * -0.02f);
    }
}
