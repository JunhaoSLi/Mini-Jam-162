using UnityEngine;

public class PositionWalls : MonoBehaviour
{
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject topWall;
    public GameObject bottomWall;

    void Start()
    {
        float roomWidth = 16f; // Width of the room
        float roomHeight = 9f; // Height of the room
        float wallThickness = 0.3f;
        // Get the position of the Room object this script is attached to
        Vector3 roomPosition = transform.position;

        // Calculate positions
        float halfRoomWidth = roomWidth / 2;
        float halfRoomHeight = roomHeight / 2;

        // Position walls
        leftWall.transform.position = new Vector3(roomPosition.x - halfRoomWidth + wallThickness / 2, roomPosition.y, roomPosition.z);
        rightWall.transform.position = new Vector3(roomPosition.x + halfRoomWidth - wallThickness / 2, roomPosition.y, roomPosition.z);
        topWall.transform.position = new Vector3(roomPosition.x, roomPosition.y + halfRoomHeight - wallThickness / 2 - 0.6f, roomPosition.z);
        bottomWall.transform.position = new Vector3(roomPosition.x, roomPosition.y - halfRoomHeight + wallThickness / 2, roomPosition.z);

        // Set wall sizes
        leftWall.transform.localScale = new Vector3(wallThickness, roomHeight, 1);
        rightWall.transform.localScale = new Vector3(wallThickness, roomHeight, 1);
        topWall.transform.localScale = new Vector3(roomWidth, wallThickness + 1.2f, 1);
        bottomWall.transform.localScale = new Vector3(roomWidth, wallThickness, 1);
    }
}
