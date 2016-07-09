using UnityEngine;
using System.Collections;

enum EnemyState {
    BEGIN_PATROL,
    CHASING,
    PATROLING,
    SHOOTING
}

public class EnemyController : MonoBehaviour {
    public Vector2 areaPatrol;
    public float patrolUpdate;
    public float changeStateTime;
    public float distanceThreshold;
    public float chaseDistance;
    public float lifePoints = 10.0f;
    public RectTransform lifeBar;

    private float _maxLifePoints;
    private Vector3 _randDestination, _startPosition;
    private Animator _enemyAnimator;
    private NavMeshAgent _agent;
    private EnemyState _currentState;

    private bool iKilledYou = false;

	// Use this for initialization
	void Start () {
        _maxLifePoints = lifePoints;
        _enemyAnimator = this.GetComponent<Animator>();
        _agent = this.GetComponent<NavMeshAgent>();
        _startPosition = this.transform.position;
        _currentState = EnemyState.BEGIN_PATROL;
        StartCoroutine("SwitchStates");
    }

    IEnumerator Patrol() {
        _randDestination = _startPosition + new Vector3(Random.Range(-areaPatrol.x, areaPatrol.x),
                                                        0.0f,
                                                        Random.Range(-areaPatrol.y, areaPatrol.y));
        _agent.SetDestination(_randDestination);
        yield return new WaitForSeconds(patrolUpdate);
        if(_currentState == EnemyState.PATROLING) StartCoroutine("Patrol");
    }

    IEnumerator Shoot() {
        StopCoroutine("SwitchStates");
        iKilledYou = true;
        StopCoroutine("Patrol");
        _agent.enabled = false;
        _enemyAnimator.SetFloat("Speed", 0.0f);
        _enemyAnimator.SetTrigger("Shoot");
        transform.LookAt(Manager.player.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position + this.transform.up * this.transform.localScale.y * 0.5f, this.transform.forward, out hit)) {
            if(hit.transform == Manager.player.transform) {
                Manager.playerController.Die();
            }
        }
        while (_enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shoot")) yield return new WaitForFixedUpdate();
         
        _enemyAnimator.SetTrigger("Wave");
    }

    IEnumerator SwitchStates() {
        while (true) {
            yield return new WaitForFixedUpdate();
            if (Vector3.Distance(this.transform.position, Manager.player.transform.position) < chaseDistance) {
                if (_currentState != EnemyState.SHOOTING) {
                    _currentState = EnemyState.CHASING;
                    _agent.SetDestination(Manager.player.transform.position);
                }
            }
            else if (_currentState != EnemyState.PATROLING) {
                _currentState = EnemyState.BEGIN_PATROL;
            }

            while(_agent.pathPending) yield return new WaitForFixedUpdate();

            switch (_currentState) {
                case EnemyState.CHASING:
                    if (Vector3.Distance(this.transform.position, Manager.player.transform.position) < distanceThreshold) {
                        _currentState = EnemyState.SHOOTING;
                    }
                    _agent.SetDestination(Manager.player.transform.position);
                    break;
                case EnemyState.BEGIN_PATROL:
                    _currentState = EnemyState.PATROLING;
                    StartCoroutine("Patrol");
                    break;
                case EnemyState.PATROLING:
                    break;
                case EnemyState.SHOOTING:
                    StartCoroutine("Shoot");
                    break;

            }
            _enemyAnimator.SetFloat("Speed", _agent.velocity.magnitude);
        }
    }

    public void Stop() {
        if(_agent.isOnNavMesh) _agent.Stop();
        StopAllCoroutines();
    }

    public void Damage(float damage) {
        lifePoints -= damage;
        lifeBar.localScale = new Vector3( lifePoints / _maxLifePoints, 1.0f, 1.0f);
        if(lifePoints <= 0) {
            lifeBar.localScale = new Vector3(0.0f, 1.0f, 1.0f);
            Die();
        }
    }

    public void Die() {
        Stop();
        GetComponent<Collider>().enabled = false;
        _enemyAnimator.SetBool("Death", true);
        _enemyAnimator.SetFloat("Direction", 0);
        _enemyAnimator.SetFloat("Speed", 0);
        Destroy(this.gameObject, 10.0f);
    }
#if UNITY_EDITOR
    void OnDrawGizmos() {
        switch (_currentState) {
            case EnemyState.CHASING:
                Gizmos.color = Color.red;
                Gizmos.DrawLine(this.transform.position, Manager.player.transform.position);
                Gizmos.DrawWireSphere(this.transform.position, distanceThreshold);
                break;
            case EnemyState.BEGIN_PATROL: case EnemyState.PATROLING:
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(this.transform.position, _randDestination);
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(this.transform.position, chaseDistance);
                break;
        }
        if (iKilledYou) {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(this.transform.position, Vector3.one);
        }
    }
#endif
}
