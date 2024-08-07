using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CustomDataExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CustomData customData = new CustomData();

        customData.name = "Miko";
        
        customData.address = new Address();
        customData.address.unit = 24;
        customData.address.road = "Main Street";
        customData.address.city = "Vancouver";

        customData.books = new Book[2];
        customData.books[0] = new Book();
        customData.books[0].bookName = "The Reign";
        customData.books[0].author = "Mark Racy";
        customData.books[0].isDigital = true;
        customData.books[0].yearPublished = 2019;

        customData.books[1] = new Book();
        customData.books[1].bookName = "The Rain";
        customData.books[1].author = "Mark Racy";
        customData.books[1].isDigital = true;
        customData.books[1].yearPublished = 2021;

        // Data Serialization
        string data = JsonUtility.ToJson(customData);

        Debug.Log($"JSON data = {data}");

        string filePath = Path.Combine(Application.dataPath, "JSONFolder/customJSONFile.json");
        File.WriteAllText(filePath, data);

        // Data Deserialization
        string json = File.ReadAllText(filePath);

        CustomData deserializedCustomData = JsonUtility.FromJson<CustomData>(json);

        string name = deserializedCustomData.name;
        string firstBookName = deserializedCustomData.books[0].bookName;
        Debug.Log($"Name = {name}, First Book Name = {firstBookName}");
        string secondBookName = deserializedCustomData.books[1].bookName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
