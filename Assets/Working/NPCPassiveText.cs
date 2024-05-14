using UnityEngine;

public class NPCPassiveText : MonoBehaviour
{
    [SerializeField] private GameObject passiveDialogueObejct;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
            passiveDialogueObejct.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
            passiveDialogueObejct.SetActive(false);
    }
}
