using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] int levelToLoad;
    private SceneLoader loader;

    private void Awake()
    {
        loader = GetComponent<SceneLoader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        loader.LoadScene(levelToLoad);
    }
}
