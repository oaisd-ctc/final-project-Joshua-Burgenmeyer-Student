using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("RoomInfo")]
    [ShowOnly] public int RoomIndex;
    [ShowOnly] public bool pauseUpdate = false;

    [Header("GameObjects")]
    public GameObject tilemap;
    public GameObject dynamicGameObject;

    [Header("Respawn Points")]
    public RespawnPoint respawnPointEnter;
    public RespawnPoint respawnPointExit;

    [Serializable]
    public struct RespawnPoint
    {
        public GameObject point;
        public bool isActive;
    }

    public void ToggleRespawn(bool state)
    {
        if (state)
        {
            respawnPointEnter.isActive = true;
            respawnPointExit.isActive = false;

            respawnPointEnter.point.SetActive(respawnPointEnter.isActive);
            respawnPointExit.point.SetActive(respawnPointExit.isActive);
        }
        else
        {
            respawnPointEnter.isActive = false;
            respawnPointExit.isActive= true;

            respawnPointEnter.point.SetActive(respawnPointEnter.isActive);
            respawnPointExit.point.SetActive(respawnPointExit.isActive);
        }
    }
}
