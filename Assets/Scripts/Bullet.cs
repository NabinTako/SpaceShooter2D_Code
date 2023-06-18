using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private enum BulletDir {
        up,
        down
    }

    private const string enemyTag = "Enemy";
    private const string playerTag = "Player";
    private bool ignorePlayer;
    [SerializeField]private BulletDir dir;
    private Vector3 bulletDirection;
    [SerializeField] private int bulletSpeed;

    private void Start() {

        switch (dir) {
            case BulletDir.up:
                bulletDirection = Vector3.up;
                ignorePlayer = true;
                break;
            case BulletDir.down:
                bulletDirection = Vector3.down;
                ignorePlayer = false;
                break;
        }

        Destroy(gameObject,2.5f);
    }

    private void Update() {
        transform.position += bulletDirection * Time.deltaTime * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(ignorePlayer == true && (other.tag == enemyTag)) {
            if (other.TryGetComponent<Enemy>(out Enemy enemyScript)) {
                enemyScript.OnHit();
                Destroy(this.gameObject);
            }
        } else if (ignorePlayer == false && (other.tag == playerTag)) {
            if (other.TryGetComponent<Player>(out Player playerScript)) {
                playerScript.OnHit();
                Destroy(this.gameObject);

            }
        }
    }
}