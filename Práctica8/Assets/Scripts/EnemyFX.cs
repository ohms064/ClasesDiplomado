using UnityEngine;
using System.Collections;

public class EnemyFX : MonoBehaviour {
    public ParticleSystem particles;

    public void EmitParticles() {
        particles.transform.parent = null;
        particles.Play();
        Destroy(particles, 15.0f);
    }
}
