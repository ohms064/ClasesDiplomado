using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {

    private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        _rigidbody = this.GetComponent<Rigidbody>();
	}
	
    void OnCollisionEnter(Collision collisionObject) {
        if (collisionObject.gameObject.CompareTag("Player")) {
            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    void OnCollisionExit(Collision collisionObject) {
        if (collisionObject.gameObject.CompareTag("Player")) {
            this.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    void OnCollisionStay(Collision collisionObject) {
        if (collisionObject.gameObject.CompareTag("Player")) {
            this.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}
