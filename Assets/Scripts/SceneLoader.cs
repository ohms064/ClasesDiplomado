using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    private int scene1, scene2;
    void Awake() {
        Debug.Log("Awakening");
    }

	// Use this for initialization
	void Start () {
        //Application.LoadLevel("");
        DontDestroyOnLoad(this.gameObject);
        scene1 = -1;
        scene2 = -1;

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.UnloadScene(scene1);
            SceneManager.UnloadScene(scene2);
            scene1 = Random.Range(2, 6);
            scene2 = Random.Range(2, 6);
            SceneManager.LoadScene(scene1, LoadSceneMode.Additive);
            SceneManager.LoadScene(scene2, LoadSceneMode.Additive);
        }
    }

}
