using UnityEngine;

public class SetRainPosToUI : MonoBehaviour
{
    private void Start()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos += new Vector3(0, 20, 9);
        transform.position = cameraPos;
    }
}
