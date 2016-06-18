using UnityEngine;
using System.Collections;

public enum SelectorState {
    CAR_SELECTION,
    TERRAIN_SELECTION
}

/*
Práctica, el carro debe ir sobre el terreno y moverse de un punto a otro. El carro debe rotar hacía donde vaya.
*/

public class CarSelector : MonoBehaviour {

    public SelectorState currentState;

    private GameObject tmpCar;
    private Vector3 destPosition;
    private float velocity;
    private Ray mouseRay;
    private RaycastHit hit;
    // Use this for initialization
    void Start () {
        currentState = SelectorState.CAR_SELECTION;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out hit)) {
                switch (currentState) {
                    case SelectorState.CAR_SELECTION:
                        if (hit.transform.tag == "Car") {
                            tmpCar = hit.transform.gameObject;
                            currentState = SelectorState.TERRAIN_SELECTION;
                        }
                        else {
                            tmpCar = null;
                        }
                        break;
                    case SelectorState.TERRAIN_SELECTION:
                        if(hit.transform.tag != "Car") {
                            destPosition = hit.point;
                            tmpCar.SendMessage("Move", destPosition, SendMessageOptions.DontRequireReceiver);
                        }
                        currentState = SelectorState.CAR_SELECTION;
                        break;
                }
            }
        }
	}
}
