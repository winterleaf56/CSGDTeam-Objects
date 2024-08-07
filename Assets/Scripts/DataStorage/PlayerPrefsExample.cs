using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPrefsExample : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TMP_Text txtDisplay;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData() {
        PlayerPrefs.SetString("SAVED_DATA", input.text);
    }

    public void LoadData() {
        if (PlayerPrefs.HasKey("SAVED_DATA")) {
            txtDisplay.SetText(PlayerPrefs.GetString("SAVED_DATA"));
        } else {
            txtDisplay.text = "No saved data";
        }
    }
}
