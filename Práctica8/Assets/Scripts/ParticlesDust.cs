using UnityEngine;
using System.Collections;

public class ParticlesDust : MonoBehaviour {

    public ParticleSystem particles;
	
	// Update is called once per frame
	void Update () {
        if (Manager.playerController.isWalking && !particles.IsAlive()) {
            particles.Emit(5);
        }
	}
}
