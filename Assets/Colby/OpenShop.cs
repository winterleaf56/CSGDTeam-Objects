using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class OpenShop : MonoBehaviour
{
    public GameObject shopCanvas;

    private bool paused = false;

    Player player;

    public void OpenMenu() {
        player = GameObject.Find("Player(Clone)").GetComponent<Player>();

        bool isActive = shopCanvas.activeSelf;
        shopCanvas.SetActive(!isActive);
        if (!paused) {
            ToggleScripts(false);
            Time.timeScale = 0;
            paused = true;
        } else {
            ToggleScripts(true);
            Time.timeScale = 1;
            paused = false;
        }
    }

    private void ToggleScripts(bool choice) {
        player.GetComponent<PlayerInput>().enabled = choice;
    }
}
