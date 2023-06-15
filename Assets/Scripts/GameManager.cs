using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField] private Text scoreText;
    public static GameManager instance;
    private int score;
    [SerializeField] GameObject enemySpawner;
    [SerializeField] GameObject powerUpSpawner;
    [SerializeField] GameObject player;
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject homeCanvas;
    [SerializeField] GameObject controlCanvas;

    private void Awake() {
        if (instance != null) {
            Destroy(instance);
        }
        instance = this.gameObject.GetComponent<GameManager>();
        score = 0;
        inGameCanvas.SetActive(false);
        //controlCanvas.SetActive(false);
    }

    public void UpdateScore() {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void StartGame() {
        enemySpawner.SetActive(true);
        powerUpSpawner.SetActive(true);
        inGameCanvas.SetActive(true);
        homeCanvas.SetActive(false);
        scoreText.text = "Score: " + score;
        Instantiate(player).transform.position = new Vector3(0, -2.7f, 0);
    }

    public void GameOver() {
        enemySpawner.SetActive(false);
        powerUpSpawner.SetActive(false);
    }

    public void ShowPlayerControls() {
        homeCanvas.SetActive(false);
        controlCanvas.SetActive(true);
    }
    public void HidePlayerControls() {
        controlCanvas.SetActive(false);
        homeCanvas.SetActive(true);
    }
}
