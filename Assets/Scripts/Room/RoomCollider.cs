using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCollider : MonoBehaviour
{
    [SerializeField] private Room roomScript;
    [SerializeField] private GameObject virtualCam;

    private float timeSinceExit;
    private bool playerIsInsideRoom;

    private void Update()
    {
        if (roomScript.pauseUpdate)
            return;

        timeSinceExit += Time.deltaTime;

        if (CanDisableRoom())
        {
            RoomManager.Instance.DisableRoom(roomScript.RoomIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            IfCanEnableRoomEnableRoom();
            playerIsInsideRoom = true;

            RoomManager.Instance.ChangePlayerCurrentRoom(roomScript.RoomIndex);

            if (roomScript.RoomIndex > RoomManager.Instance.PlayerLastRoom)
            {
                RoomManager.Instance.ToggleEnterRoomRespawn(roomScript.RoomIndex, true);
            }
            else if (roomScript.RoomIndex <  RoomManager.Instance.PlayerLastRoom)
            {
                RoomManager.Instance.ToggleEnterRoomRespawn(roomScript.RoomIndex, false);
            }

            virtualCam.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            timeSinceExit = 0;
            playerIsInsideRoom = false;

            virtualCam.SetActive(false);
        }
    }
    
    private void IfCanEnableRoomEnableRoom()
    {
        if (!roomScript.dynamicGameObject.activeSelf)
            RoomManager.Instance.EnableRoom(roomScript.RoomIndex);
    }

    private bool CanDisableRoom()
    {
        return timeSinceExit > 3 && !playerIsInsideRoom;
    }
}
