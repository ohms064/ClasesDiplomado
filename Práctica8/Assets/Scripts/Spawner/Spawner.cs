using UnityEngine;
using System.Collections;
using System;

public abstract class Spawner : MonoBehaviour {
    [SerializeField] [Range( 1, 10 )] public int maxObjects = 1;
    [HideInInspector] public bool spawning;

    public abstract IEnumerator SpawnObject();
}
