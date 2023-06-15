using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour {

    [SerializeField] private int ShieldHealth = 2;

    private const string enemyTag = "Enemy";
    private const string bulletgameObjectTag = "EnemyBullet";
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == enemyTag || other.gameObject.tag == bulletgameObjectTag) {
            ShieldHealth -= 1;
            Destroy(other.gameObject);
            if(ShieldHealth <= 0) {
                ShieldHealth = 2;
                gameObject.SetActive(false);
            }
        }
    }
}
