using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public GameObject explosion;
    public GameObject holeDecal;

    void OnCollisionEnter(Collision col) {
        Destroy(Instantiate(explosion, col.contacts[0].point, Quaternion.identity), 2.0f);
        GameObject decal = (GameObject) Instantiate(holeDecal, col.contacts[0].point + col.contacts[0].normal * 0.02f, Quaternion.identity);
        print(col.contacts[0].normal);
        decal.transform.forward = -col.contacts[0].normal;
        decal.transform.parent = col.transform;
        Destroy(this.gameObject);
    }

}
