using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector] public int SceneToLoadNext;

    private int currentScene;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Duplicate GameManager found. Removing " + name);
            Destroy(this);
            return;
        }

        Debug.Log("GameManager Initialized");
        Instance = this;

        DontDestroyOnLoad(this);
    }

    public void TogglePauseMenu()
    {
        if (UIManager.Instance.canvas.gameObject.activeSelf)
        {
            Bootstrap.Instance.isPaused = false;
            Time.timeScale = 1;
            UIManager.Instance.canvas.gameObject.SetActive(false);
        }
        else
        {
            Bootstrap.Instance.isPaused = true;
            Time.timeScale = 0;
            UIManager.Instance.canvas.gameObject.SetActive(true);
        }
    }

    public void UpdateCamera()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        print("Updating Camera: Set to " + Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(currentScene)) + "(" + currentScene + ")");
        if (currentScene < 3)
        {
            //Camera.main.backgroundColor = Color.black;
            Camera.main.backgroundColor = new Color(0.075f, 0.008f, 0.031f);
        }
        else
        {
            //Camera.main.backgroundColor = new Color(0.1921569f, 0.3019608f, 0.4745098f);
            Camera.main.backgroundColor = new Color(0.122f, 0.02f, 0.063f);
        }
    }

    public void LoadLevel(Text text)
    {
        print("Loading Scene: " + Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(SceneToLoadNext)) + "(" + SceneToLoadNext + ")");
        StartCoroutine(LoadNext(text));
    }

    private IEnumerator LoadNext(Text text)
    {
        //yield return new WaitForSeconds(.5f);
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(SceneToLoadNext);

        while (!loadLevel.isDone)
        {
            text.text = (int)(loadLevel.progress * 100) + "%";
            yield return null;
        }
        //transition.SetTrigger();

        OnSceneLoaded();
    }

    public void OnSceneLoaded()
    {
        print("Scene Loaded");

        //UIManager.Instance.LoadSceneUI(SceneToLoadNext);

        UpdateCamera();
        //Bootstrap.Instance.Rooms.Clear(); //Clear all held rooms

        if (currentScene > 2)
        {
            RoomManager.Instance.PlayerCurrentRoom = 0;
            GameObject[] disableOnStartObejcts = GameObject.FindGameObjectsWithTag("DisableOnStart");

            foreach (GameObject obj in disableOnStartObejcts)
            {
                obj.SetActive(false);
                print("GameManager disabled: " + obj.name + "(" + obj.transform.parent.name + ")");
            }

            UIManager.Instance.FindPlayerMovementComponent();
        }
    }
}
