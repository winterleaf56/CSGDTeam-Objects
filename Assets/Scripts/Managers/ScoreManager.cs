using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public UnityEvent OnScoreUpdated;
    public UnityEvent OnHighScoreUpdated;

    private int score, highScore;

    private void Start() {
        highScore = PlayerPrefs.GetInt("HighScore");
        OnHighScoreUpdated?.Invoke();
        GameManager.GetInstance().onGameStart += OnGameStart;
    }

    public int GetScore() {
        return score;
    }

    public int GetHighScore() {
        return highScore;
    }

    public void IncrementScore() {
        score++;
        OnScoreUpdated?.Invoke();

        if (score > highScore) {
            highScore = score;
            OnHighScoreUpdated?.Invoke();
        }
    }

    public void SetHighScore() {
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void OnGameStart() {
        score = 0;
        OnScoreUpdated?.Invoke();
    }
}
