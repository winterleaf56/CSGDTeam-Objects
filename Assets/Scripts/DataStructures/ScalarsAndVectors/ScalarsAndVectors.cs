using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalarsAndVectors : MonoBehaviour
{
    public Transform obj;

    public Vector2 position;
    public Vector3 direction;

    public float scalar;
    public float rotVal;

    // Start is called before the first frame update
    void Start()
    {
        obj.position = position * scalar;
    }

    // Update is called once per frame
    void Update()
    {
        obj.rotation = Quaternion.Euler(direction);
        
        obj.rotation = Quaternion.Euler(0, 0 , rotVal);
    }
}
