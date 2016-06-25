using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour {

    private Animator _playerAnimator;
    private CapsuleCollider _capsule;
    private float _startColliderHeight;

	// Use this for initialization
	void Start () {
        _capsule = this.GetComponent<CapsuleCollider>();
        _startColliderHeight = _capsule.height;
        _playerAnimator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        _playerAnimator.SetFloat("Direction", Input.GetAxis("Horizontal"));
        _playerAnimator.SetFloat("Speed", Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space)) {
            _playerAnimator.SetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            _playerAnimator.SetTrigger("Wave");
        }
        if (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
            _capsule.height = _playerAnimator.GetFloat("ColliderHeight");
        }
        else { _capsule.height = _startColliderHeight; }
	}

    IEnumerator JumpState() {
        while (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
            _capsule.height = _playerAnimator.GetFloat("ColliderHeight");
            yield return new WaitForFixedUpdate();
        }
        _capsule.height = _startColliderHeight;
    }
}
