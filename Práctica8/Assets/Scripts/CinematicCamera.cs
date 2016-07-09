using UnityEngine;
using System.Collections;

public class CinematicCamera : MonoBehaviour {

    private ThirdPersonCamera _cam;
    private Animator _animator;

    // Use this for initialization
    void Start() {
        _cam = this.GetComponent<ThirdPersonCamera>();
        _animator = this.GetComponent<Animator>();
        _cam.enabled = false;
        Manager.playerController.enabled = false;
    }

    public void BeginThirdPersonCamera() {
        _animator.enabled = false;
        _cam.enabled = true;
        Manager.Begin();

    }
}
