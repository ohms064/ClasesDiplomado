using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour {

    public ParticleSystem particles;
    public float damage = 5.0f;

    private Animator _playerAnimator;
    private CapsuleCollider _capsule;
    private float _startColliderHeight;

#if UNITY_ANDROID || UNITY_IOS
    private Vector3 accel;
    private Touch finger;
#endif


    // Use this for initialization
    void Start () {
        _capsule = this.GetComponent<CapsuleCollider>();
        _startColliderHeight = _capsule.height;
        _playerAnimator = this.GetComponent<Animator>();
        StartCoroutine("Move");
	}
	
	// Update is called once per frame
	IEnumerator Move () {
        while (true) {
#if UNITY_STANDALONE || UNITY_EDITOR
            _playerAnimator.SetFloat("Direction", Input.GetAxis("Horizontal"));
            _playerAnimator.SetFloat("Speed", Input.GetAxis("Vertical"));
            if (Input.GetKeyDown(KeyCode.Space)) {
                _playerAnimator.SetTrigger("Jump");
            }
            if (Input.GetKeyDown(KeyCode.H)) {
                _playerAnimator.SetTrigger("Wave");
            }
            if (Input.GetMouseButtonDown(0)) {
                _playerAnimator.SetTrigger("Shoot");
                RaycastHit hit;
                if(Physics.Raycast(this.transform.position + this.transform.up * this.transform.localScale.y, this.transform.forward, out hit)) {
                    print(hit.transform.name);
                    hit.transform.SendMessage("Damage", damage);
                }
                else {
                    print("Nothing happened");
                }
            }
            if (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
                _capsule.height = _playerAnimator.GetFloat("ColliderHeight");
            }
            else { _capsule.height = _startColliderHeight; }

# elif UNITY_IOS || UNITY_ANDROID
            accel = Input.acceleration;
            _playerAnimator.SetFloat("Direction", accel.x);
            for(int i = 0; i < Input.touchCount; i++) {
                finger = Input.GetTouch(i);
                if (finger.position.x > Screen.width * 0.5f) {
                    _playerAnimator.SetFloat("Speed", 1.0f);
                }
                else if(finger.phase == TouchPhase.Began){
                    _playerAnimator.SetTrigger("Jump");
                }
            }
            if(Input.touchCount == 0) {
                _playerAnimator.SetFloat("Speed", 0.0f);
            }
#endif
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator JumpState() {
        while (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
            _capsule.height = _playerAnimator.GetFloat("ColliderHeight");
            yield return new WaitForFixedUpdate();
        }
        _capsule.height = _startColliderHeight;
    }

    public void Die() {
        StopAllCoroutines();
        Manager.Stop();
        _playerAnimator.SetFloat("Direction", 0);
        _playerAnimator.SetFloat("Speed", 0);
        _playerAnimator.SetBool("Death", true);
    }
#if UNITY_EDITOR
    void OnDrawGizmos() {

        Gizmos.color = Physics.Raycast(this.transform.position + this.transform.up * this.transform.localScale.y, this.transform.forward) ? Color.green : Color.red;
        Gizmos.DrawRay(this.transform.position + this.transform.up * this.transform.localScale.y, this.transform.forward);
        
    }
#endif
}
