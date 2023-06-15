using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] GameObject EnemyPrefab;

    private float xAxisLimitPositive = 3.65f;
    private float xAxisLimitNegative = -3.65f;
    private float previousSpawnTime;
    [SerializeField]private float EnemySpawnInterval;

    private void Update() {

        if(Time.time > previousSpawnTime + EnemySpawnInterval) {
            GameObject enemy = Instantiate(EnemyPrefab);
            enemy.transform.position = new Vector3(Random.Range(xAxisLimitNegative, xAxisLimitPositive),transform.position.y,transform.position.z);
            previousSpawnTime = Time.time;
        }
        
    }
}
