using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    public GameObject player;

    [SerializeField] private List<Room> rooms = new();

    [HideInInspector] public int PlayerCurrentRoom = 0;
    [HideInInspector] public int PlayerLastRoom = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Duplicate RoomManager found. Removing " + name);
            Destroy(this);
            return;
        }

        Debug.Log("RoomManager Initialized");
        Instance = this;

        CollectRooms();
    }

    private void CollectRooms()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if (child.TryGetComponent<Room>(out Room room))
            {
                rooms.Add(room);
                room.RoomIndex = i;
                if (room.dynamicGameObject.CompareTag("DisableOnStart"))
                {
                    DisableRoom(room.RoomIndex);
                }
            }
            else
            {
                Debug.LogError("No Room Component Found on " + child.name);
            }
        }
    }

    public void EnableRoom(int roomIndex)
    {
        Room room = rooms[roomIndex];
        room.dynamicGameObject.SetActive(true);
        room.tilemap.SetActive(true);
        room.pauseUpdate = false;

        print("RoomManager Enabled: " + room.gameObject.name);
    }

    public void DisableRoom(int roomIndex)
    {
        Room room = rooms[roomIndex];
        room.dynamicGameObject.SetActive(false);
        room.tilemap.SetActive(false);
        room.pauseUpdate = true;

        print("RoomManager Disabled: " + room.gameObject.name);
    }

    public void ChangePlayerCurrentRoom(int currentRoom)
    {
        PlayerLastRoom = PlayerCurrentRoom;
        PlayerCurrentRoom = currentRoom;
    }

    public Vector3 ReturnCurrentEnterRespawnPointPosition()
    {
        Room room = rooms[PlayerCurrentRoom];
        return room.respawnPointEnter.point.transform.position;
    }

    public Vector3 ReturnCurrentExitRespawnPointPosition()
    {
        Room room = rooms[PlayerCurrentRoom];
        return room.respawnPointExit.point.transform.position;
    }

    public bool ReturnEnterRespawnState(int roomIndex)
    {
        Room room = rooms[roomIndex];
        return room.respawnPointEnter.isActive;
    }

    public bool ReturnExitRespawnState(int roomIndex)
    {
        Room room = rooms[roomIndex];
        return room.respawnPointExit.isActive;
    }

    public void ToggleEnterRoomRespawn(int roomIndex, bool state)
    {
        Room room = rooms[roomIndex];
        room.ToggleRespawn(state);
    }
}
