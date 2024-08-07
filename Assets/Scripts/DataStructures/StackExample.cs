using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackExample : MonoBehaviour
{
    public GameObject objectPrefab;
    public Stack<GameObject> objStack = new Stack<GameObject>();

    GameObject tempOBJ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Push object to the top of the stack
        if (Input.GetKeyDown(KeyCode.A)) {
            tempOBJ = Instantiate(objectPrefab, transform);
            tempOBJ.transform.position = new Vector2(0, objStack.Count);

            tempOBJ.name = "STACKED-" + objStack.Count;
            tempOBJ.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

            objStack.Push(tempOBJ);
            Debug.Log("Pushed " + tempOBJ.name + " to the stack");
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            var removedObj = objStack.Pop();
            Destroy(removedObj);
            Debug.Log("Popped from the stack: " + removedObj.name);
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            Debug.Log("Object at the top of the stack: " + objStack.Peek().name);
        }
        
    }
}
