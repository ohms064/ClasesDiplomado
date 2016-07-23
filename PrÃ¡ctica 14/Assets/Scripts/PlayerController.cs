using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprRenderer;
    private float movement;
    private bool isJumping = false;
    private bool isEnding = false;

    [Range(0.0f, 1.0f)]public float speed;
    public GameObject damageFX;
    public int health = 3;
    public float jumpForce = 1.0f;
    public float endGameTransition = 2.0f;
    public float pushedBackForce = 2.0f;
    public int items;
    [HideInInspector] public int points = 0;

	// Use this for initialization
	void Start () {
        _sprRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Damage") || _animator.GetBool("KO")) return;

        movement = Input.GetAxis("Horizontal") * speed;
        if (isJumping) movement *= 0.75f;
        _animator.SetFloat("Speed", Mathf.Abs(movement));
        _sprRenderer.flipX = movement < 0 || (movement == 0 && _sprRenderer.flipX);
        this.transform.position += new Vector3(movement, 0.0f, 0.0f);

        movement = Input.GetAxis("Vertical");
        if(movement > 0.0f && !isJumping) {            
            _animator.SetTrigger("Jump");
        }
	}

    public void Jump() {
        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.transform.CompareTag("Banana")) {
            coll.transform.position = new Vector2(Random.Range(-11.0f, 10.0f), Random.Range(-3.0f, 1.0f));
            points++;
            CanvasManager.score.text = points.ToString();
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.transform.CompareTag("Platform") && isJumping) {
            isJumping = false;
            _animator.SetTrigger("Land");
            return;
        }
        if (coll.transform.CompareTag("Barrel") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Damage")) {
            Destroy(coll.gameObject);
            health--;
            if(health >= 0) Destroy(CanvasManager.life[health]);
            if(isJumping)
                _animator.SetTrigger("DamageJump");
            else
                _animator.SetTrigger("Damage");
            if (health <= 0) {
                _animator.SetBool("KO", true);
            }
            Destroy(Instantiate(damageFX, coll.contacts[0].point, Quaternion.identity), 0.5f);
        }
    }

    public void ResetGame() {
        if (!isEnding) {
            isEnding = true;
            Invoke("ReloadGame", endGameTransition);
            if(PlayerPrefs.HasKey("HighScore") && PlayerPrefs.GetInt("HighScore") < points)
                PlayerPrefs.SetInt("HighScore", points);
        }
    }

    void ReloadGame() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void PushedBack() {
        int invert = _sprRenderer.flipX ? 1 : -1;
        _rigidbody.AddForce((invert * this.transform.right + this.transform.up) * pushedBackForce, ForceMode2D.Impulse);
    }
}
