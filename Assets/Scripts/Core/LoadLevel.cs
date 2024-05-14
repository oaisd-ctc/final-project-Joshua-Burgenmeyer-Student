using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private Text text;

    private void Awake()
    {
        GameManager.Instance.UpdateCamera();
    }

    private void Start()
    {
        GameManager.Instance.LoadLevel(text);
    }
}