using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    private float horizontalInput;
    private float verticalInput;
    private float powerUpTakenTime;
    [SerializeField]private float powerUpInterval;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int playerMaxHit = 3;
    [SerializeField] private Transform firePointA;
    [SerializeField] private Transform firePointB;
    [SerializeField] private CheckPowerUps PowerUpChecker;
    private Transform actualFirePoint;

    [SerializeField] private bool doubleShootBoost;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject Shield;

    // input axis names
    private const string horizontalAxis = "Horizontal";
    private const string verticalAxis = "Vertical";
    private const float xAxisPositiveLimit = 4.2f;
    private const float xAxisNegativeLimit = -4.2f;
    private const float yAxisNegativeLimit = -2.7f;
    private const float yAxisPositiveLimit = 1.0f;

    private void Start() {
        actualFirePoint = firePointA;

        PowerUpChecker.OnDowbleShootPowerUP += PowerUpChecker_OnDowbleShootPowerUP;
        PowerUpChecker.OnSpeedUpPowerUP += PowerUpChecker_OnSpeedUpPowerUP;
        PowerUpChecker.OnEnableShieldPowerUP += PowerUpChecker_OnEnableShieldPowerUP;
    }

    private void PowerUpChecker_OnEnableShieldPowerUP(object sender, EventArgs e) {
        Shield.SetActive(true);
        powerUpTakenTime = Time.time;
    }

    private void PowerUpChecker_OnSpeedUpPowerUP(object sender, EventArgs e) {
        moveSpeed = 6;
        powerUpTakenTime = Time.time;
    }

    private void PowerUpChecker_OnDowbleShootPowerUP(object sender, System.EventArgs e) {
        doubleShootBoost = true;
        powerUpTakenTime = Time.time; 
    }

    private void Update() {
        HandelInput();
        HandelPowerUp(powerUpTakenTime);
    }

    private void HandelPowerUp(float powerUpTime) {
        if(Time.time > powerUpTime+powerUpInterval) {
            doubleShootBoost = false;
            moveSpeed = 4;
        }

    }

    private void HandelInput() {
        horizontalInput = Input.GetAxis(horizontalAxis);
        verticalInput = Input.GetAxis(verticalAxis);

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (doubleShootBoost) {
                Instantiate(bulletPrefab).transform.position = firePointA.position;
                Instantiate(bulletPrefab).transform.position = firePointB.position;
                return;
            }

            Instantiate(bulletPrefab).transform.position = actualFirePoint.position;
            actualFirePoint= (actualFirePoint == firePointA)?firePointB : firePointA;
        }

        if(transform.position.y > yAxisNegativeLimit && transform.position.y < yAxisPositiveLimit) {

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
        playerMaxHit -= 1;
        if (playerMaxHit <= 0) {
            Destroy(gameObject);
        }
    }
}
