using UnityEngine;
using UnityEngine.UI;

public class LoadingBarSpinner : MonoBehaviour
{
    [SerializeField] private Image image;

    private void Start()
    {
        InvokeRepeating(nameof(Spinner), 0, .25f);
    }

    private void Spinner()
    {
        image.transform.Rotate(0, 0, -45);
    }
}