using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootstrapTest : MonoBehaviour
{
    void Start()
    {
        if (Bootstrap.Instance.Test())
            print("Bootstrap Test Success: " + Bootstrap.Instance.name + " Active");
        else
            Debug.LogError("Bootstrap Test Failed: Bootstrap Not Found");
    }
}