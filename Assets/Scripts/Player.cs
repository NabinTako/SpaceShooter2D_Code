using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    protected float horizontalInput;
    protected float verticalInput;
    protected float powerUpTakenTime;
    protected int playerHitCount;
    [SerializeField] protected int playerMaxHit = 3;
    [SerializeField] protected GameObject[] DamagedThrusters;
    [SerializeField] protected float powerUpInterval;
    [SerializeField] protected int moveSpeed;

    [SerializeField] protected AudioSource laserAudio;
    [SerializeField] protected Transform firePointA;
    [SerializeField] protected Transform firePointB;
    [SerializeField] protected CheckPowerUps PowerUpChecker;

    protected Transform actualFirePoint;
    protected bool doubleShootBoost;
    
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected GameObject Shield;

    // input axis names
    protected const string horizontalAxis = "Horizontal";
    protected const string verticalAxis = "Vertical";
    protected const float xAxisPositiveLimit = 4.2f;
    protected const float xAxisNegativeLimit = -4.2f;
    protected const float yAxisNegativeLimit = -2.7f;
    protected const float yAxisPositiveLimit = 1.0f;

    protected virtual void Start() {
        actualFirePoint = firePointA;
        playerHitCount = 0;
        PowerUpChecker.OnDowbleShootPowerUP += PowerUpChecker_OnDowbleShootPowerUP;
        PowerUpChecker.OnSpeedUpPowerUP += PowerUpChecker_OnSpeedUpPowerUP;
        PowerUpChecker.OnEnableShieldPowerUP += PowerUpChecker_OnEnableShieldPowerUP;
    }

    protected void PowerUpChecker_OnEnableShieldPowerUP(object sender, EventArgs e) {
        Shield.SetActive(true);
        powerUpTakenTime = Time.time;
    }

    protected void PowerUpChecker_OnSpeedUpPowerUP(object sender, EventArgs e) {
        moveSpeed = 6;
        powerUpTakenTime = Time.time;
    }

    protected void PowerUpChecker_OnDowbleShootPowerUP(object sender, System.EventArgs e) {
        doubleShootBoost = true;
        powerUpTakenTime = Time.time; 
    }

    protected virtual void Update() {
    }

    protected virtual void HandelPowerUp(float powerUpTime) {
        if(Time.time > powerUpTime+powerUpInterval) {
            doubleShootBoost = false;
            moveSpeed = 4;
        }

    }

   protected virtual void HandelInput() {

        if (transform.position.y > yAxisNegativeLimit && transform.position.y < yAxisPositiveLimit) {

            transform.position += new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * moveSpeed;
        }else if (transform.position.y < yAxisNegativeLimit) {
            transform.position += new Vector3(horizontalInput, 
                (verticalInput > 0)? 1 : 0 , 0) * Time.deltaTime * moveSpeed;
        } else {
            transform.position += new Vector3(horizontalInput,
               (verticalInput < 0) ? -1 : 0, 0) * Time.deltaTime * moveSpeed;
        }

        if (transform.position.x > xAxisPositiveLimit || transform.position.x < xAxisNegativeLimit) {
            HandelPosition();
        }
    }
    private void HandelPosition() {

        transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
    }

    public void OnHit() {
        if (Shield.activeInHierarchy) {
            return;
        }
        if(playerHitCount < DamagedThrusters.Length) {
        DamagedThrusters[playerHitCount].SetActive(true);
        }
        playerHitCount += 1;
        if (playerHitCount >= playerMaxHit) {
            GameManager.instance.OnPlayer_EnemyDestruction(transform.position);
            GameManager.instance.GameOver();
            Destroy(gameObject);
            return;
        }
    }
}
