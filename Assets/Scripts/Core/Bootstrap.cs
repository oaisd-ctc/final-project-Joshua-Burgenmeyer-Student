using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public static class PreformBootstrap
{
    const string sceneName = "Bootstrap";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
        {
            Scene scene = SceneManager.GetSceneAt(sceneIndex);

            if (scene.name == sceneName)
                return;
        }

        Debug.Log("Loading Bootstrap Scene...");

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}

public class Bootstrap : MonoBehaviour
{
    public static Bootstrap Instance { get; private set; }

    [HideInInspector] public bool isPaused;
    //[HideInInspector] public Transform currentRespawnPoint;
    //[HideInInspector] public List<Transform> respawnPointEnterObjects;
    //[HideInInspector] public List<Transform> respawnPointExitObjects;
    //[HideInInspector] public List<Room> Rooms;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Duplicate Bootstrap found. Removing " + name);
            Destroy(this);
            return;
        }

        Debug.Log("Bootstrap Initialized");
        Instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        GameManager.Instance.UpdateCamera();
    }

    public bool Test()
    {
        return true;
    }
}
