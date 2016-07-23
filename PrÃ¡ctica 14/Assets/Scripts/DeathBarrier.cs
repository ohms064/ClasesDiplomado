using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathBarrier : MonoBehaviour {

    public Vector2 startPos;

	void OnTriggerEnter2D(Collider2D coll) {
        if (coll.transform.CompareTag("Player")){
            SceneManager.LoadScene(0);
        }
    }
}
