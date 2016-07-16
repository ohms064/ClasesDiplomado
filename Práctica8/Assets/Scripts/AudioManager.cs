using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioClip[] soundsFX;

    private AudioSource _audioSource;

	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            _audioSource.PlayOneShot(soundsFX[0]);
        }
        if (Input.GetMouseButtonDown(1)) {
            _audioSource.PlayOneShot(soundsFX[1]);
        }
    }
}
