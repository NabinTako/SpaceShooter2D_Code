using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private int enemySpeed;
    [SerializeField] private int enemyMaxHit = 2;
    [SerializeField]private float shootTimeInterval;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePointA;
    [SerializeField] private Transform firePointB;
    private Transform actualFirePoint;


    private float yAxisLimit = -3.5f;
    private float xAxisLimitPositive = 3.65f;
    private float xAxisLimitNegative = -3.65f;

    private float previousShootTime = 0f;

    private void Start() {
        actualFirePoint = firePointA;
    }

    private void Update() {
        transform.position += Vector3.down * Time.deltaTime * enemySpeed;

        if(transform.position.y < yAxisLimit) {

           transform.position = new Vector3( Random.Range(xAxisLimitNegative, xAxisLimitPositive),-transform.position.y,transform.position.z);
           
        }

        HandelShooting();
    }

    private void HandelShooting() {
        if(Time.time > previousShootTime + shootTimeInterval) {
            previousShootTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = actualFirePoint.position;
            actualFirePoint = (actualFirePoint == firePointA) ? firePointB : firePointA;
        }
    }

    public void OnHit() {
        enemyMaxHit -= 1;
        if(enemyMaxHit <= 0) {
            GameManager.instance.UpdateScore();
            Destroy(gameObject);
        }
    }
}
