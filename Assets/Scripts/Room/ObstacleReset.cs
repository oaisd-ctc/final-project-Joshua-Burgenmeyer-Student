using UnityEngine;

public class ResetObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            if (RoomManager.Instance.ReturnEnterRespawnState(RoomManager.Instance.PlayerCurrentRoom))
            {
                RoomManager.Instance.player.transform.position = RoomManager.Instance.ReturnCurrentEnterRespawnPointPosition();
            }
            else
            {
                RoomManager.Instance.player.transform.position = RoomManager.Instance.ReturnCurrentExitRespawnPointPosition();
            }
        }
    }
}