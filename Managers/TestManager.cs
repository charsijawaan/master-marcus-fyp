using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : Singleton<TestManager>
{

    public List<string> testStrings = new List<string>();

    private void Awake()
    {
        testStrings.Add("1 string");
        testStrings.Add("2 string");
    }

    public List<string> GetTestStrings()
    {
        return testStrings;
    }

}
