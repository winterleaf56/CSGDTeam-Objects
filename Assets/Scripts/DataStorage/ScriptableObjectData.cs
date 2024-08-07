using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectData : MonoBehaviour
{
    [SerializeField] ScriptableObjectsExample example;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Loaded a file with name: {example.objectName}, score: {example.score}, starting at: {example.startPos}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
