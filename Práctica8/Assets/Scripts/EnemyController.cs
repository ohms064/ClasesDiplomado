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

    private Vector3 _randDestination, _startPosition;
    private Transform _player;
    private Animator _enemyAnimator;
    private NavMeshAgent _agent;
    private EnemyState _currentState;

	// Use this for initialization
	void Start () {
        _enemyAnimator = this.GetComponent<Animator>();
        _agent = this.GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
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

    IEnumerator SayHi() {
        StopCoroutine("SwitchStates");
        StopCoroutine("Patrol");
        _agent.enabled = false;
        _enemyAnimator.SetFloat("Speed", 0.0f);
        _enemyAnimator.SetTrigger("Wave");
        _enemyAnimator.SetTrigger("Shoot");
        
        while (_enemyAnimator.GetCurrentAnimatorStateInfo(1).IsName("Wave")) yield return new WaitForFixedUpdate();
    }

    IEnumerator SwitchStates() {
        while (true) {
            yield return new WaitForEndOfFrame();
            if (Vector3.Distance(this.transform.position, _player.position) < chaseDistance) {
                if (_currentState != EnemyState.SHOOTING) {
                    _currentState = EnemyState.CHASING;
                }
            }
            else if (_currentState != EnemyState.PATROLING) {
                _currentState = EnemyState.BEGIN_PATROL;
            }

            switch (_currentState) {
                case EnemyState.CHASING:
                    _agent.SetDestination(_player.position);
                    if (_agent.remainingDistance < distanceThreshold) {
                        if (!_enemyAnimator.GetCurrentAnimatorStateInfo(1).IsName("Wave")) _currentState = EnemyState.SHOOTING;
                    }
                    break;
                case EnemyState.BEGIN_PATROL:
                    _currentState = EnemyState.PATROLING;
                    StartCoroutine("Patrol");
                    break;
                case EnemyState.PATROLING:
                    break;
                case EnemyState.SHOOTING:
                    StartCoroutine("SayHi");
                    break;

            }
            _enemyAnimator.SetFloat("Speed", _agent.velocity.magnitude);
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        switch (_currentState) {
            case EnemyState.CHASING:
                Gizmos.color = Color.red;
                Gizmos.DrawLine(this.transform.position, _player.position);
                Gizmos.DrawWireSphere(this.transform.position, distanceThreshold);
                break;
            case EnemyState.BEGIN_PATROL: case EnemyState.PATROLING:
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(this.transform.position, _randDestination);
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(this.transform.position, chaseDistance);
                break;
        }
        
        

    }
#endif
}
