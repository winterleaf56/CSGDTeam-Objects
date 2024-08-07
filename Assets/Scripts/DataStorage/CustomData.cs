[System.Serializable]
public class CustomData {
    public string name;
    public Address address;
    public Book[] books;
}

[System.Serializable]
public class Address {
    public int unit;
    public string road;
    public string city;
}

[System.Serializable]
public class Book {
    public string bookName;
    public string author;
    public bool isDigital;
    public int yearPublished;
}