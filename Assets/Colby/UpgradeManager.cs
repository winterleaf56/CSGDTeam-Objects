using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private Player player;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When the upgrade button is pressed, the float value from the button is passed to the Upgrade function
    // and a new health entity is created for the player.
    public void Upgrade(float newMaxHealth) {
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
