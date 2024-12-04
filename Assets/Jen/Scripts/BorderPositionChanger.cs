using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderPositionChanger : MonoBehaviour
{   
    [SerializeField] private Transform borderL, borderR;

    private float screenWidth;

    void Start()
    {
        screenWidth = Screen.width;
        SetBorderEdges();
    }

    // Update is called once per frame
    void Update()
    {   
        if(screenWidth != Screen.width) SetBorderEdges();
    }

    private void SetBorderEdges() {
        float camPosZ = -Camera.main.transform.position.z;
        borderL.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height/2-borderL.position.y, camPosZ));
        borderR.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2-borderR.position.y, camPosZ));
        screenWidth = Screen.width;
    }
}
