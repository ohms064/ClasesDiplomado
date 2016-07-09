using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public static GameObject player;
    public static PlayerController playerController;
    public static List<EnemyController> enemigos;
    public static Spawner[] spawners;

    void Awake () {
        spawners = GameObject.FindObjectsOfType<Spawner>();
        Manager.enemigos = new List<EnemyController>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        player = playerController.gameObject;
	}

    public static void Stop() {
        foreach(Spawner spawner in spawners) {
            spawner.StopAllCoroutines();
        }
        foreach(EnemyController enemigo in enemigos) {
            enemigo.Stop();
        }
    }

    public static void Begin() {
        playerController.enabled = true;
        foreach (Spawner spawner in spawners) {
            spawner.enabled = true;
        }
    }

    public static void Reset() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
