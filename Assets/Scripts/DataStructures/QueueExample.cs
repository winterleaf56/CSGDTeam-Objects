using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueExample : MonoBehaviour
{
    public GameObject objPrefab;

    public Queue<GameObject> objQueue = new Queue<GameObject>();

    GameObject tempOBJ;
    Vector2 lastEnqueuePosition = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            tempOBJ = Instantiate(objPrefab, transform);
            tempOBJ.transform.position = new Vector2(lastEnqueuePosition.x + 1, 0);

            tempOBJ.name = "STACKED-" + objQueue.Count;
            tempOBJ.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

            objQueue.Enqueue(tempOBJ);
            lastEnqueuePosition = tempOBJ.transform.position;
            Debug.Log("Enqueued " + tempOBJ.name + " to the Queue");
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            var removedObj = objQueue.Dequeue();
            Destroy(removedObj);
            Debug.Log("Dequeued from the queue: " + removedObj.name);
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            Debug.Log("Object at the front of the Queue: " + objQueue.Peek().name);
        }
    }
}
