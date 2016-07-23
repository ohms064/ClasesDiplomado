using UnityEngine;
using System.Collections;

public class BarrelSpawner : MonoBehaviour {

    public GameObject barrelPrefab;
    public float waitTime = 2.0f;
    public float destroyTime = 5.0f;
    public Vector2 offset;
    public float attackInterval = 2.0f;

    private Vector3 offset3;
    private Animator _animator;

	// Use this for initialization
	void Start () {
        offset3 = new Vector3(offset.x, offset.y, 0.0f);
        _animator = GetComponent<Animator>();
        InvokeRepeating("Launch", 1.0f, attackInterval);
    }

    void Launch() {
        _animator.SetTrigger("Launch");
    }

    private void Spawn() {
        Destroy(Instantiate(barrelPrefab, this.transform.position + offset3, Quaternion.identity), destroyTime);
    }

}
