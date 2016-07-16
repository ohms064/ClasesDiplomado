using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    public float openTime = 2.0f;
    public float closeTime = 5.0f;
    public AudioClip doorAudio;

    private AudioSource _doorSource;
    private Animator _animator;
    private MeshRenderer _renderer;

	// Use this for initialization
	void Start () {
        _animator = this.GetComponent<Animator>();
        StartCoroutine("OpenClose");
        _renderer = this.GetComponent<MeshRenderer>();
        _doorSource = GetComponent<AudioSource>();
	}

    IEnumerator OpenClose() {
        while (true) {
            _animator.SetBool("Open", false);
            yield return new WaitForSeconds(closeTime);
            _animator.SetBool("Open", true);
            yield return new WaitForSeconds(openTime);
        }
    }

    public void OpenDoor() {
        _renderer.material.color = Color.red;
    }

    public void CloseDoor() {
        _renderer.material.color = Color.blue;
    }

    public void PlaySound() {
        StartCoroutine("Sound");
    }

    IEnumerator Sound() {
        _doorSource.PlayOneShot(doorAudio);
        yield return new WaitForSeconds(0.5f);
        _doorSource.Stop();
    }
}
