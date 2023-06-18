using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Player {


    protected override void  Start() {
        base.Start();
    }

    protected override void Update() {
        HandelInput();
        base.HandelPowerUp(powerUpTakenTime);
    }
    protected override void HandelInput() {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            horizontalInput = Input.GetAxis(horizontalAxis);
            verticalInput = Input.GetAxis(verticalAxis);
        } else {
            horizontalInput = 0;
            verticalInput = 0;
        }


        if (Input.GetKeyDown(KeyCode.Space)) {
            laserAudio.Play();
            if (doubleShootBoost) {
                Instantiate(bulletPrefab).transform.position = firePointA.position;
                Instantiate(bulletPrefab).transform.position = firePointB.position;
                return;
            }

            Instantiate(bulletPrefab).transform.position = actualFirePoint.position;
            actualFirePoint = (actualFirePoint == firePointA) ? firePointB : firePointA;
        }

        base.HandelInput();
    }

}
