using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public GameObject shopCanvas;

    public void OpenMenu() {
        bool isActive = shopCanvas.activeSelf;
        shopCanvas.SetActive(!isActive);
    }
}
