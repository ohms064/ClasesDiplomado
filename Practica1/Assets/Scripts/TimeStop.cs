using UnityEngine;
using System.Collections;

public class TimeStop : MonoBehaviour {
    private Rigidbody rb;
    private Vector3 velocityAtStop, angularVelocityAtStop;
    private bool frozen;
	// Use this for initialization
	void Start () {
        this.rb = this.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(1.0f, 0.0f, 0.0f));
        frozen = false;
	}
	
    void Update() {
        if(!frozen)  Debug.Log(string.Format("La velocidad de {0} es {1}, {3} y la velocidad angular es {2}, {4}", this.name, this.rb.velocity, this.rb.angularVelocity, this.velocityAtStop, this.angularVelocityAtStop));
    }

	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.E)) {
            frozen = !frozen;
            this.rb.isKinematic = frozen;
            if (frozen) {
                print("Frozen!");
                this.rb.velocity = velocityAtStop;
                this.rb.angularVelocity = angularVelocityAtStop;
            }
            else {
                print("Unfrozen!");
            }

        }
        velocityAtStop = this.rb.velocity;
        angularVelocityAtStop = this.rb.angularVelocity;
    }
}
