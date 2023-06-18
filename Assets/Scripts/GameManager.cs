using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text gameOverScoreText;
    [SerializeField] private Text gameOverBestScoreText;
    private const string bestScorePlayerPrefName = "BestScore";
    public static GameManager instance;
    private int score;
    private bool co_opMode;
    private int deathMessageCounter; 
    // to differenciate between two players death and sigle player death
    [SerializeField] GameObject Audiocontroler;
    [SerializeField] GameObject HomeBackGround;
    [SerializeField] GameObject ExplosionGameObject;
    [SerializeField] AudioSource ExplosionAudio;
    [SerializeField] GameObject enemySpawner;
    [SerializeField] GameObject powerUpSpawner;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject homeCanvas;
    [SerializeField] GameObject gameOverCanvas;

    private void Awake() {
        if (instance != null) {
            Destroy(instance);
        }
        instance = this.gameObject.GetComponent<GameManager>();
        score = 0;
        inGameCanvas.SetActive(false);
    }

    public void UpdateScore() {
        score += 10;
        scoreText.text = "Score:        " + score;
    }

    public void StartSinglePlayerGame() {
        GameComponentsActivate();
        co_opMode = false;
        Instantiate(player1).transform.position = new Vector3(0, -2.7f, 0);
    }
    public void StartDoublePlayerGame() {
        GameComponentsActivate();
        co_opMode = true;
        Instantiate(player1).transform.position = new Vector3(2.7f, -2.7f, 0);
        Instantiate(player2).transform.position = new Vector3(-2.7f, -2.7f, 0);
    }

    private void GameComponentsActivate() {
        enemySpawner.SetActive(true);
        powerUpSpawner.SetActive(true);
        inGameCanvas.SetActive(true);
        homeCanvas.SetActive(false);
        HomeBackGround.SetActive(false);
        scoreText.text = "Score:        " + score;
        bestScoreText.text = "Best Score:       " + PlayerPrefs.GetInt(bestScorePlayerPrefName,0);
    }
    public void GameOver() {
        int bestScore = PlayerPrefs.GetInt(bestScorePlayerPrefName, 0);
        if (co_opMode) {
        deathMessageCounter += 1;
            if(deathMessageCounter >= 2) {
                enemySpawner.SetActive(false);
                powerUpSpawner.SetActive(false);
                PlayerPrefs.SetInt(bestScorePlayerPrefName, score);
            }
            return;
        }
        const string enemyGameObjectTag = "Enemy";
        GameObject[] enemyGameObjectsArray =
        GameObject.FindGameObjectsWithTag(enemyGameObjectTag);
        foreach (GameObject enemyGameObject in enemyGameObjectsArray)
        {
            Destroy(enemyGameObject);
        }
        enemySpawner.SetActive(false);
        powerUpSpawner.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameOverScoreText.text = "Current score:    " + score;
        if(score > bestScore) {
        PlayerPrefs.SetInt(bestScorePlayerPrefName, score);
        gameOverBestScoreText.text = "Previous Best:    " + bestScore;
            score = 0;
            return;
        }
        score = 0;
        gameOverBestScoreText.text = "Best Score:       " + bestScore;
    }

    public void ShowAudioSlider() {
        Audiocontroler.SetActive(!Audiocontroler.activeInHierarchy);
    }

    public void OnPlayer_EnemyDestruction(Vector3 position) {
        float maxTime = 0.7f;
        GameObject explosionVisual = Instantiate(ExplosionGameObject);
        ExplosionAudio.Play();
        explosionVisual.transform.position = position;
        Destroy(explosionVisual, maxTime);
    }

    public void OnQuidGamePressed() {
        Application.Quit();
    }
}
