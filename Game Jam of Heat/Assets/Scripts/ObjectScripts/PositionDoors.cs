using UnityEngine;

public class PositionDoors : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject topDoor;
    public GameObject bottomDoor;

    void Start()
    {
        float roomWidth = 16f; // Width of the room
        float roomHeight = 9f; // Height of the room
        float doorThickness = 0.3f;

        // Calculate positions
        float halfRoomWidth = roomWidth / 2;
        float halfRoomHeight = roomHeight / 2;
        Vector3 roomPosition = transform.position;

        // Position doors
        leftDoor.transform.position = new Vector3(roomPosition.x - halfRoomWidth + doorThickness / 2, roomPosition.y, roomPosition.z);
        rightDoor.transform.position = new Vector3(roomPosition.x + halfRoomWidth - doorThickness / 2, roomPosition.y, roomPosition.z);
        topDoor.transform.position = new Vector3(roomPosition.x, roomPosition.y + (halfRoomHeight - doorThickness / 2) - 0.6f, roomPosition.z);
        bottomDoor.transform.position = new Vector3(roomPosition.x, roomPosition.y - halfRoomHeight + doorThickness / 2, roomPosition.z);

        // Set door sizes
        leftDoor.transform.localScale = new Vector3(doorThickness, roomHeight / 6, 1);
        rightDoor.transform.localScale = new Vector3(doorThickness, roomHeight / 6, 1);
        topDoor.transform.localScale = new Vector3(roomWidth / 6, doorThickness + 1.2f, 1);
        bottomDoor.transform.localScale = new Vector3(roomWidth / 6, doorThickness, 1);
    }
}
