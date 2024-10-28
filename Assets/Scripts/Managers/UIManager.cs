using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private TMP_Text txtHealth;
    [SerializeField] private TMP_Text txtScore, txtHighScore;

    [Header("Menu")]
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject gameOverLabel;
    [SerializeField] private TMP_Text txtMenuHighScore;

    private Player player;
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Awake()
    {
        scoreManager = GameManager.GetInstance().scoreManager;

        GameManager.GetInstance().onGameStart += GameStarted;
        GameManager.GetInstance().onGameOver += GameOver;
    }

    public void UpdateHealth(float currentHealth) {
        txtHealth.SetText(currentHealth.ToString());
    }

    public void UpdateScore(int currentScore) {
        txtScore.SetText(scoreManager.GetScore().ToString());
    }

    public void UpdateHighScore() {
        txtHighScore.SetText(scoreManager.GetHighScore().ToString());
        txtMenuHighScore.SetText($"High Score: {scoreManager.GetHighScore()}");
    }

    public void GameStarted() {
        player = GameManager.GetInstance().GetPlayer();
        player.health.OnHealthUpdate += UpdateHealth;

        menuCanvas.SetActive(false);
    }

    // When a health upgrade is purchased, the UpdateHealth action is unsubscribed from,
    // the player is then fetched, and then a new subscription is made
    public void HealthUpgrade() {
        player.health.OnHealthUpdate -= UpdateHealth;
        player = GameManager.GetInstance().GetPlayer();
        player.health.OnHealthUpdate += UpdateHealth;
    }

    public void GameOver() {
        Debug.Log("Game Over");
        gameOverLabel.SetActive(true);
        menuCanvas.SetActive(true);

        // reset upgrades
    }
}
