using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {
    public float speed;

    private float startTime;
    private bool moving;
    private Vector3 destPosition;
    private Vector3 startPosition;
	
	// Update is called once per frame
	void Update () {
        if(moving) transform.position = Vector3.Lerp(startPosition, destPosition, (Time.time - startTime) / speed);
        if (Time.time - startTime >= speed) moving = false;
	}

    public void Move(Vector3 endPosition) {
        startTime = Time.time;
        moving = true;
        startPosition = this.transform.position;
        destPosition = endPosition;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 200);
    }
}
