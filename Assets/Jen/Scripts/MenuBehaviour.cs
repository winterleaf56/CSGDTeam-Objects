using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreTxt;

    private void Start() {
        highScoreTxt.SetText("High Score: "+ PlayerPrefs.GetInt("HighScore"));
    }
}
