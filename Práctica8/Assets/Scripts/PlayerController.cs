using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour {

    public float damage = 5.0f;
    [HideInInspector]public bool isWalking = false;

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

	IEnumerator Move () {
        while (true) {
            _playerAnimator.SetFloat("Direction", Input.GetAxis("Horizontal"));
            _playerAnimator.SetFloat("Speed", Input.GetAxis("Vertical"));

            isWalking = _playerAnimator.GetFloat("Speed") > 0.2f;

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

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator JumpState() {
        //particles.emission.enabled = false;
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
