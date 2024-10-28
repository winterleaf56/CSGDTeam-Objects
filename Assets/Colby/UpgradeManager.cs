using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private Player player;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private GameObject[] lockedPanel;
    [SerializeField] private GameObject[] purchasedPanel;

    //[SerializeField] private float price;
    //[SerializeField] private float newMaxHealth;

    /*public void CheckUpgrade() {
        if (scoreManager.GetScore() >= Mathf.Floor(newMaxHealth / 10)) {
            if (purchasedPanel != null) {
                purchasedPanel.SetActive(true);
            }
            if (lockedPanel != null) {
                lockedPanel.SetActive(false);
            }
            Upgrade();
        }
    }*/

    public void CheckUpgrade(float newMaxHealth) {
        //float price = Mathf.Floor(newMaxHealth / 10);
        float balance = scoreManager.GetScore();
        switch (newMaxHealth) {
            case 125:
                if (balance >= 15) {
                    purchasedPanel[0].SetActive(true);
                    lockedPanel[0].SetActive(false);
                    Upgrade(125);
                }
                    
                break;
            case 150:
                if (balance >= 25) {
                    purchasedPanel[1].SetActive(true);
                    lockedPanel[1].SetActive(false);
                    Upgrade(150);
                }
                break;
            case 175:
                if (balance >= 50) {
                    purchasedPanel[2].SetActive(true);
                    lockedPanel[2].SetActive(false);
                    Upgrade(200);
                }
                break;
            case 200:
                if (balance >= 75) {
                    purchasedPanel[3].SetActive(true);
                    Upgrade(250);
                }
                break;
            default:
                break;
        }
    }

    // When the upgrade button is pressed, the float value from the button is passed to the Upgrade function
    // and a new health entity is created for the player.
    private void Upgrade(float newMaxHealth) {
        player = GameManager.GetInstance().GetPlayer();
        player.health = new Health(newMaxHealth, 0.5f, 50);
        Debug.Log("Button pressed and new health is: " + newMaxHealth);

        // UI Manager is then notified of the health upgrade
        uiManager.HealthUpgrade();

    }

    /*// Probably a bad implementation but hey, it works
    public void showPanel(GameObject panel) {
        panel.SetActive(true);
    }*/
}
