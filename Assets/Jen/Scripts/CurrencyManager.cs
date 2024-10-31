using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : MonoBehaviour
{   
    private int currentCurrency; 

    public UnityEvent OnCurrencyUpdated;

    void Start() {
        OnCurrencyUpdated?.Invoke();
        GameManager.GetInstance().onGameStart += OnGameStart;
    }

    public void IncreaseCurrency(int value) {
        currentCurrency += value;
        OnCurrencyUpdated?.Invoke();
    }

    public void DeductCurrency(int value) {
        Debug.Log("Deducting currency: " + value);
        currentCurrency -= value;
        OnCurrencyUpdated?.Invoke();
    }

    public void OnGameStart() {
        currentCurrency = 0;
        OnCurrencyUpdated?.Invoke();
    }

    public int GetCurrency() {
        return currentCurrency;
    }
}
