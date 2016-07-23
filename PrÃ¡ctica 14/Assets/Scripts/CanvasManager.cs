using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasManager : MonoBehaviour {
    public Image[] lifePoints;
    public Text gamePoints;
    public Text highScore;
    public static Image[] life;
    public static Text score;

    // Use this for initialization
    void Start () {
        life = lifePoints;
        score = gamePoints;
        if (PlayerPrefs.HasKey("HighScore"))
            highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        else {
            highScore.text = "0";
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }


}
