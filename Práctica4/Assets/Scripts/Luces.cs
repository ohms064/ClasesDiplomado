using UnityEngine;
using System.Collections;

public class Luces : MonoBehaviour {
    public float colorDuration = 1.0f, rotationDuration = 1.0f;
    public Vector3 _endRotation;

    private Vector3 _startRotation;
    private Color _startColor, _endColor;
    private float _colorStartTime, _rotationStartTime;
    private Light _light;
    private bool _changingColor;

	// Use this for initialization
	void Start () {
        _startRotation = this.transform.localEulerAngles;
        _light = this.GetComponent<Light>();
        _startColor = _light.color;
        StartCoroutine("ForwardRotation");
    }

    void Update() {
        if (!_changingColor) {
            _endColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            _colorStartTime = Time.time;
            _changingColor = true;
        }
        if (_changingColor ) {
            _light.color = Color.Lerp(_startColor, _endColor, (Time.time - _colorStartTime) / colorDuration);
            if( Time.time - _colorStartTime >= colorDuration ) {
                _changingColor = false;
                _startColor = _light.color;
            }
        }
    }

    IEnumerator ForwardRotation() {
        _rotationStartTime = Time.time;
        while(Time.time - _rotationStartTime < rotationDuration) {
            transform.localEulerAngles = Vector3.Lerp(_startRotation, _endRotation, (Time.time - _rotationStartTime) / rotationDuration);
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine("BackwardRotation");
        yield return null;
    }

    IEnumerator BackwardRotation() {
        _rotationStartTime = Time.time;
        while (Time.time - _rotationStartTime < rotationDuration) {
            transform.localEulerAngles = Vector3.Lerp(_endRotation, _startRotation, (Time.time - _rotationStartTime) / rotationDuration);
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine("ForwardRotation");
        yield return null;
    }
}
