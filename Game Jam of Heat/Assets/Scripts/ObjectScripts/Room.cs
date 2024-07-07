using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject westDoor;
    [SerializeField] GameObject eastDoor;
    [SerializeField] GameObject northDoor;
    [SerializeField] GameObject southDoor;
    [SerializeField] GameObject roomCamera;
    [SerializeField] RoomEnemiesManager roomEnemiesManager;

    public Vector2Int RoomIndex { get; set; }

    private void Start()
    {
        roomCamera.SetActive(false);
        westDoor.SetActive(false);
        eastDoor.SetActive(false);
        northDoor.SetActive(false);
        southDoor.SetActive(false);
    }

    public void OpenDoor(Vector2Int direction)
    {
        if (direction == Vector2Int.left)
        {
            westDoor.SetActive(true);
        }
        if (direction == Vector2Int.right)
        {
            eastDoor.SetActive(true);
        }
        if (direction == Vector2Int.up)
        {
            northDoor.SetActive(true);
        }
        if (direction == Vector2Int.down)
        {
            southDoor.SetActive(true);
        }
    }

    public void Activate()
    {
        roomCamera.SetActive(true);
        roomEnemiesManager.ActivateEnemies();
    }

    public void Deactivate()
    {
        roomCamera.SetActive(false);
        roomEnemiesManager.DeactivateEnemies();
    }
}
