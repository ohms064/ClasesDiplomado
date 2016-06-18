using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator _playerAnimator;

	// Use this for initialization
	void Start () {
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
	}
}
