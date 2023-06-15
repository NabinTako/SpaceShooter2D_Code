using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

    [SerializeField] GameObject[] PowerUps;

    private float xAxisLimitPositive = 3.65f;
    private float xAxisLimitNegative = -3.65f;
    private float previousSpawnTime;
    [SerializeField] private float PowerUpSpawnInterval;

    private void Update() {

        if (Time.time > previousSpawnTime + PowerUpSpawnInterval) {
            GameObject powerUp = Instantiate(PowerUps[Random.Range(0,3)]);
            powerUp.transform.position = new Vector3(Random.Range(xAxisLimitNegative, xAxisLimitPositive), transform.position.y, transform.position.z);
            previousSpawnTime = Time.time;
        }

    }

}
