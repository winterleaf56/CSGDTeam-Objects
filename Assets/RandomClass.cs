using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Static classes cannot inherit or be instantiated
public static class RandomClass
{
    // All fields and methods must be static
    static int myNumber;

    public static void MyMethod() {

    }

    public static int NumberMethod() {
        return myNumber;
    }
}
