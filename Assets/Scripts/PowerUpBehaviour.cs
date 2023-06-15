using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour {

    private float moveSpeed = 2f;
    private void Update() {

        transform.position += Vector3.down * Time.deltaTime * moveSpeed;
        if(transform.position.y < -3.2f) {
            Destroy(gameObject);
        }
    }

}
