using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListExamples : MonoBehaviour
{

    public GameObject objectPrefab;

    public List<GameObject> objectList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject tempOBJ;

        tempOBJ = Instantiate(objectPrefab, transform);
        tempOBJ.transform.position = new Vector2(0, 0);
        objectList.Add(tempOBJ);

        tempOBJ = Instantiate(objectPrefab, transform);
        tempOBJ.transform.position = new Vector2(1, 0);
        objectList.Add(tempOBJ);

        tempOBJ = Instantiate(objectPrefab, transform);
        tempOBJ.transform.position = new Vector2(2, 0);
        objectList.Add(tempOBJ);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            // ObjectList.Count is the number of elements in the list
            objectList[Random.Range(0, objectList.Count)].GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        }

        //Remove an object from the list at index 1
        if (Input.GetKeyDown(KeyCode.X)) {
            Destroy(objectList[1].gameObject);
            objectList.RemoveAt(1);
        }
    }
}
