using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class TimerTest : MonoBehaviour
{
    public Image cooldown;
    public bool coolingDown;
    public float waitTime = 30.0f;

    // Update is called once per frame
    void Update()
    {
        if (coolingDown == true)
        {
            //Reduce fill amount over 30 seconds
            cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
    }

    public void AddTime() {
        waitTime += 5;
        cooldown.fillAmount += 1;
    }
}