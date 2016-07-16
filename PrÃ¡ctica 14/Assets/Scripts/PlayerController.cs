using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprRenderer;
    private float movement;

    [Range(0.0f, 1.0f)]public float speed;

	// Use this for initialization
	void Start () {
        _sprRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        movement = Input.GetAxis("Horizontal") * speed;
        _animator.SetFloat("Speed", Mathf.Abs(movement));
        _sprRenderer.flipX = movement < 0 || (movement == 0 && _sprRenderer.flipX);
        this.transform.position += new Vector3(movement, 0.0f, 0.0f);
	}

    void OnTriggerEnter2D(Collider2D coll) {
        Destroy(coll.gameObject);
    }
}
