using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private Player player;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private CurrencyManager currencyManager;

    [SerializeField] private GameObject[] healthLockedPanel;
    [SerializeField] private GameObject[] healthPurchasedPanel;

    [SerializeField] private GameObject[] regenLockedPanel;
    [SerializeField] private GameObject[] regenPurchasedPanel;

    private float currentRegenRate, currentMaxHealth;

    private void Start() {
        GameManager.GetInstance().onGameOver += GameOver;
    }

    public void CheckRegenUpdate(float newRegenRate) {
        int balance = currencyManager.GetCurrency();

        switch (newRegenRate) {
            case 0.75f:
                if (balance >= 50) UpgradeRegen(0.75f, 25, 0, false);
                break;
            case 1f:
                if (balance >= 100) UpgradeRegen(1f, 75, 1, true);
                break;
            default:
                break;
        }
    }

    public void CheckHealthUpgrade(float newMaxHealth) {
        int balance = currencyManager.GetCurrency();
        switch (newMaxHealth) {
            case 125:
                if (balance >= 15) UpgradeHealth(125, 15, 0, false);
                break;
            case 150:
                if (balance >= 40) UpgradeHealth(150, 40, 1, false);
                break;
            case 200:
                if (balance >= 100) UpgradeHealth(200, 80, 2, true);
                break;
            default:
                break;
        }
    }

    // When the upgrade button is pressed, the float value from the button is passed to the Upgrade function
    // and a new health entity is created for the player.
    // newMaxHealth is the new max health
    // price is the cost
    // panel is the index of the purchase and locked panels
    // lastUpgrade avoids error for no remaining locked panels
    private void UpgradeHealth(float newMaxHealth, int price, int panel, bool lastUpgrade) {
        player = GameManager.GetInstance().GetPlayer();

        currentRegenRate = player.health.GetRegenRate();
        player.health = new Health(newMaxHealth, currentRegenRate, player.health.GetHealth());

        // Subtracts the cost from player balance
        currencyManager.DeductCurrency(price);

        // UI Manager is then notified of the health upgrade
        uiManager.HealthUpgrade();

        healthPurchasedPanel[panel].SetActive(true);

        if (!lastUpgrade) {
            healthLockedPanel[panel].SetActive(false);
        }

        Debug.Log("Button pressed and new health is: " + newMaxHealth);
    }

    private void UpgradeRegen(float newRegenRate, int price, int panel, bool lastUpgrade) {
        player = GameManager.GetInstance().GetPlayer();

        currentMaxHealth = player.health.GetMaxHealth();
        player.health = new Health(currentMaxHealth, newRegenRate, player.health.GetHealth());

        currencyManager.DeductCurrency(price);

        uiManager.HealthUpgrade();

        regenPurchasedPanel[panel].SetActive(true);

        if (!lastUpgrade) {
            regenLockedPanel[panel].SetActive(false);
        }
    }

    public void GameOver() {
        foreach (GameObject panel in healthPurchasedPanel) {
            panel.SetActive(false);
        }

        foreach (GameObject panel in regenPurchasedPanel) {
            panel.SetActive(false);
        }

        for (int i = 0; i < healthLockedPanel.Length; i++) {
            healthLockedPanel[i].SetActive(true);
        }

        for (int i = 0; i < regenLockedPanel.Length; i++) {
            regenLockedPanel[i].SetActive(true);
        }
    }
}
