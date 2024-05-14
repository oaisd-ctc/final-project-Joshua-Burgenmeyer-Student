using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        GameManager.Instance.SceneToLoadNext = scene;
        if (Bootstrap.Instance.isPaused)
            GameManager.Instance.TogglePauseMenu();

        print("Starting Loading Screen...");
        SceneManager.LoadScene("LoadingScreen");
    }
}