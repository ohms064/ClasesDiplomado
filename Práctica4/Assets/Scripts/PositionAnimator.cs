using UnityEngine;
using System.Collections;

public class PositionAnimator : MonoBehaviour {

    public Vector3 endPosition;
    public float duration;

    private float _startTime;
    private bool _animating = false;
    private Vector3 _startPosition;

    void Start() {
        _startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (duration == 0) return;
        if (Input.GetKeyDown(KeyCode.Space) && !_animating) {
            _startTime = Time.time;
            _animating = true;
        }

        if (_animating) {
            transform.position = Vector3.Lerp(_startPosition, endPosition, (Time.time - _startTime)/duration);
            if (Time.time - _startTime >= duration) {
                _animating = false;
            }
        }
	}
}
