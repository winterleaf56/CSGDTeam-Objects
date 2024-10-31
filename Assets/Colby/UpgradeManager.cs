using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private Player player;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private CurrencyManager currencyManager;

    [SerializeField] private GameObject[] lockedPanel;
    [SerializeField] private GameObject[] purchasedPanel;

    public void CheckUpgrade(float newMaxHealth) {
        int balance = currencyManager.GetCurrency();
        switch (newMaxHealth) {
            case 125:
                if (balance >= 15) Upgrade(125, 15, 0, false);
                break;
            case 150:
                if (balance >= 40) Upgrade(150, 40, 1, true);
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
    private void Upgrade(float newMaxHealth, int price, int panel, bool lastUpgrade) {
        player = GameManager.GetInstance().GetPlayer();
        player.health = new Health(newMaxHealth, 0.5f, 50);

        currencyManager.DeductCurrency(price);

        // UI Manager is then notified of the health upgrade
        uiManager.HealthUpgrade();

        purchasedPanel[panel].SetActive(true);

        if (!lastUpgrade) {
            lockedPanel[panel].SetActive(false);
        }

        Debug.Log("Button pressed and new health is: " + newMaxHealth);
    }
}
