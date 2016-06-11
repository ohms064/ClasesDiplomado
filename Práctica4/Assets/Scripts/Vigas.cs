using UnityEngine;
using System.Collections;

public class Vigas : MonoBehaviour {
    public Vector3 endPosition;
    public float duration = 1.0f;

    private float _startTime;
    private Vector3 _startPosition;
    private bool _goingForward;
    // Use this for initialization
    void Start() {
        _startPosition = this.transform.position;
        StartCoroutine("goForward");
    }

    public IEnumerator goForward() {
        _startTime = Time.time;
        while(Time.time - _startTime < duration) {
            transform.position = Vector3.Lerp(_startPosition, endPosition, (Time.time - _startTime) / duration);
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine("goBackward");
    }

    public IEnumerator goBackward() {
        _startTime = Time.time;
        while (Time.time - _startTime < duration) {
            transform.position = Vector3.Lerp(endPosition, _startPosition, (Time.time - _startTime) / duration);
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine("goForward");
    }


}
