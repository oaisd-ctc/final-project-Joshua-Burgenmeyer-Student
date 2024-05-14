using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Canvas canvas;

    [SerializeField] private Canvas MainMenuPrefab;
    [SerializeField] private Canvas LoadingScreenPrefab;
    [SerializeField] private Canvas GameplayPrefab;

    //[SerializeField] private GameManager gameManager;
    //[SerializeField] private MusicManager musicManager;

    private PlayerMovement movement;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Duplicate UIManager found. Removing " + name);
            Destroy(this);
            return;
        }

        Debug.Log("UIManager Initialized");
        Instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //LoadSceneUI(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        ScanForEscapeKey();
    }

    private void ScanForEscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CanPause())
            {
                GameManager.Instance.TogglePauseMenu();
            }
        }
    }

    public void FindPlayerMovementComponent()
    {
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private bool CanPause()
    {
        return SceneManager.GetActiveScene().buildIndex > 2 && movement != null && movement.LastOnGroundTime > 0 && !movement.IsJumping;
    }

    public void LoadSceneUI(int sceneID)
    {
        if (sceneID == 1)
        {
            //Main Menu
            Instantiate(MainMenuPrefab);
        }
        else if (sceneID == 2)
        {
            //Loading Screen
            Instantiate(LoadingScreenPrefab);
            Destroy(MainMenuPrefab);
            Destroy(GameplayPrefab);
        }
        else
        {
            //Gameplay
            Instantiate(GameplayPrefab);
        }
    }
}
