using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyScriptableData", menuName = "CircuitStream/MyScriptableData", order = 1)]
public class ScriptableObjectsExample : ScriptableObject
{
    public string objectName;
    public int score;
    public Vector2 startPos;
}
