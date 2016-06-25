using UnityEngine;
using System.Collections;

public class EnemySpawning : MonoBehaviour {

    public Vector3 spawnLocation;
    public float m_SpawnTime = 2f;

    void Start()
    {
        StartCoroutine("WaitToSpawnNewEnemy");
    }

    IEnumerator WaitToSpawnNewEnemy()
    {
        yield return new WaitForSeconds(m_SpawnTime);
        Master.m_Instance.SpawnEnemy(spawnLocation);
        StartCoroutine("WaitToSpawnNewEnemy");
    }

}