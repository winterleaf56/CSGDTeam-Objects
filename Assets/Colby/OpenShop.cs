using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public GameObject shopCanvas;

    private bool paused = false;

    public void OpenMenu() {
        bool isActive = shopCanvas.activeSelf;
        shopCanvas.SetActive(!isActive);
        if (!paused) {
            Time.timeScale = 0;
            paused = true;
        } else {
            Time.timeScale = 1;
            paused = false;
        }
    }
}
