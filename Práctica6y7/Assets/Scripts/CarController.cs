using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {
    public float translateSpeed;
    public float rotationSpeed;

    private float _startTime;
    private bool _moving;
    private bool _rotating;
    private Vector3 _destPosition;
    private Vector3 _startPosition;
    private Vector3 _startRotation;
    private Vector3 _destRotation;
    private Rigidbody _rigidbody;
    private float _step;
	
    void Start() {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (_moving) _rigidbody.MovePosition(Vector3.Lerp(_startPosition, _destPosition, (Time.time - _startTime) / translateSpeed));
        if (_rotating) {
            //_step = rotationSpeed * Time.deltaTime;
            //this.transform.rotation = Quaternion.(Vector3.RotateTowards(this.transform.localEulerAngles, _destPosition - this.transform.position, _step, 0.0f));

            //_rigidbody.MoveRotation(Quaternion.Euler(Vector3.Lerp(_startRotation, _destRotation, (Time.time - _startTime) / rotationSpeed)));
            this.transform.LookAt(_destPosition);
            print(string.Format("rotacion: {0} destino: {1} lerp: {2}", this.transform.localEulerAngles, _destRotation, Time.time - _startTime));
        }

        if (Time.time - _startTime >= translateSpeed && _moving) _moving = false;
        if (Time.time - _startTime >= rotationSpeed && _rotating) {
            _rotating = false;
            Move();
        }
    }

    public void Move() {
        _startTime = Time.time;
        _moving = true;
        _rotating = false;
        _startPosition = this.transform.position;
        //this.transform.forward = (endPosition - this.transform.position).normalized;
    }

    public void Rotate(Vector3 endPosition) {
        _startTime = Time.time;
        _rotating = true;
        _moving = false;
        _startRotation = this.transform.localEulerAngles;
        _destPosition = endPosition;
        _destRotation = new Vector3(_rigidbody.rotation.eulerAngles.x, _rigidbody.rotation.eulerAngles.y + Vector3.Angle(this.transform.forward, (endPosition - this.transform.position).normalized), _rigidbody.rotation.eulerAngles.z);
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        if (_rotating) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 10);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.transform.position, this._destPosition);
        }
    }
#endif
}
