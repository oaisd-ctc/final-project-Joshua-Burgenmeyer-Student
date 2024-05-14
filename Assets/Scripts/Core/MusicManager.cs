using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Duplicate MusicManager found. Removing " + name);
            Destroy(this);
            return;
        }

        Debug.Log("MusicManager Initialized");
        Instance = this;

        DontDestroyOnLoad(this);
    }
}
