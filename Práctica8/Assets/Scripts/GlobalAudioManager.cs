using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class GlobalAudioManager : MonoBehaviour {

    [SerializeField] private AudioMixer _globalMixer;
    [HideInInspector] public static AudioMixer globalMixer;

    void Awake() {
        globalMixer = _globalMixer;
    }

    public void SetGlobalVolume(float volume) {
        print("It Works! " + volume);
        globalMixer.SetFloat("masterVolume", volume);
    }

    public void SetFXVolume(float volume) {
        print("It Works! " + volume);
        globalMixer.SetFloat("soundFXVolume", volume);
    }

    public void SetMusicVolume(float volume) {
        print("It Works! " + volume);
        globalMixer.SetFloat("musicVolume", volume);
    }
}
