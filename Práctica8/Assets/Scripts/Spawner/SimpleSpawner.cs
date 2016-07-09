using UnityEngine;
using System.Collections;

/// <summary>
/// Script que generá instancias de spawnObject que deberá ser el prefab de un 
/// carro con los componentes CarAIControl y TestDirectionAI.
/// <para>
/// El objeto se instancía con la rotación y posición del SimpleSpawner
/// en escena por lo que el SimpleSpawner debe estar ubicado correctamente.
/// </para>
/// </summary>
public class SimpleSpawner : Spawner {    
    [Range(3.0f, 10.0f)] public float instantiateEverySeconds = 3.0f;

    public GameObject spawnObject;
    public SimpleSpawnType spawnerBegin = SimpleSpawnType.WAIT;

    private Transform _nextWaypoint;
    private int _numObj;
    private GameObject newObject;

    void Start() {
        StartCoroutine("SpawnObject");
    }

    /// <summary>
    /// Crea una instancia de spawnObject mientas numObj sea menor a maxObjects.
    /// </summary>
    public override IEnumerator SpawnObject() {
        yield return new WaitForSeconds( instantiateEverySeconds * (int)spawnerBegin );
        while ( _numObj < maxObjects ) {
            _numObj++;
            newObject = (GameObject) Instantiate( spawnObject, this.transform.position, this.transform.rotation );
            Manager.enemigos.Add(newObject.GetComponent<EnemyController>());
            yield return new WaitForSeconds( instantiateEverySeconds );
        }
        spawning = false;
    }

    //-----------------------------GIZMOS---------------------------------------------

#if UNITY_EDITOR
    

    void OnDrawGizmos() {
        Gizmos.DrawIcon(transform.position + Vector3.up, "SimpleSpawner.png");
    }
#endif
}
