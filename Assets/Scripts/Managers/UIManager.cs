using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private TMP_Text txtHealth;
    [SerializeField] private TMP_Text txtScore, txtHighScore;
    [SerializeField] private Image speedTimer, shieldTimer, rapidFireTimer;
    [SerializeField] private GameObject[] nukeDisplay;

    [Header("Menu")]
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject gameOverLabel;
    [SerializeField] private TMP_Text txtMenuHighScore;

    private Player player;
    private ScoreManager scoreManager;
    private Camera cam;

    // Start is called before the first frame update
    void Awake()
    {
        scoreManager = GameManager.GetInstance().scoreManager;

        cam = Camera.main;

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
        player.OnTimerUpdate += UpdateTimer;
        player.OnNukeUpdate += UpdateNukeDisplay;
        
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

    private void UpdateSpeedTimer(float timerTime, float duration){  
        speedTimer.transform.position = cam.WorldToScreenPoint(player.transform.position) + new Vector3(75,50,0);
        speedTimer.fillAmount = 1 - timerTime / duration;
    }

    private void UpdateShieldTimer(float timerTime, float duration){  
        shieldTimer.transform.position = cam.WorldToScreenPoint(player.transform.position) + new Vector3(75,0,0);
        shieldTimer.fillAmount = 1 - timerTime / duration;
    }

    private void UpdateRapidFireTimer(float timerTime, float duration){  
        rapidFireTimer.transform.position = cam.WorldToScreenPoint(player.transform.position) + new Vector3(10,0,0);
        rapidFireTimer.fillAmount = 1 - timerTime / duration;
    }

    public void UpdateTimer(float timerTime, float duration, int type) {
        if (type==0) {
            UpdateSpeedTimer(timerTime, duration);
        }
        else if (type==1) {
            UpdateShieldTimer(timerTime, duration);
        }
        else if (type==2) {
            UpdateRapidFireTimer(timerTime, duration);
        }
    }

    public void UpdateNukeDisplay(int nukeCount, bool addedNuke){
        nukeDisplay[nukeCount].SetActive(addedNuke);
    }
}
