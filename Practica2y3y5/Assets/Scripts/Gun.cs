using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
    public GameObject bullet;
    private GameObject tmpBullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            tmpBullet = (GameObject)Instantiate(bullet, this.transform.position, bullet.transform.rotation);
            tmpBullet.transform.up = -this.transform.forward;
            tmpBullet.GetComponent<Rigidbody>().AddForce(this.transform.forward * 50.0f, ForceMode.Impulse);
        }
	}

    void OnDrawGizmo() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + (this.transform.forward * 5.0f));
    }

}
