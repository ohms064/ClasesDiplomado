using UnityEngine;
using System.Collections;

public class TouchTest : MonoBehaviour {

    private Touch touch;
    private Vector2 touchPos;
    void OnGUI() {
            for(int i = 0; i < Input.touchCount; i++) {
                touch = Input.GetTouch(i);
                switch (touch.phase) {
                    case TouchPhase.Moved:
                        GUI.color = Color.yellow;
                        break;
                    case TouchPhase.Stationary:
                        GUI.color = Color.red;
                        break;
                }
            touchPos = new Vector2(touch.position.x, Screen.height - touch.position.y);
            GUI.Label(new Rect(touch.position.x, touch.position.y, 150, 100), "P: " + touchPos);
            }
            
    }

    void Update() {
        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            print(touch.position);
        }
    }
}
