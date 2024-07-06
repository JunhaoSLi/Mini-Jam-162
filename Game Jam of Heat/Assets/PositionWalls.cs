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

        // Calculate positions
        float halfRoomWidth = roomWidth / 2;
        float halfRoomHeight = roomHeight / 2;

        // Position walls
        leftWall.transform.position = new Vector3(-halfRoomWidth + wallThickness / 2, 0, 0);
        rightWall.transform.position = new Vector3(halfRoomWidth - wallThickness / 2, 0, 0);
        topWall.transform.position = new Vector3(0, (halfRoomHeight - wallThickness / 2) - 0.6f, 0);    
        bottomWall.transform.position = new Vector3(0, -halfRoomHeight + wallThickness / 2, 0);

        // Set wall sizes
        leftWall.transform.localScale = new Vector3(wallThickness, roomHeight, 1);
        rightWall.transform.localScale = new Vector3(wallThickness, roomHeight, 1);
        topWall.transform.localScale = new Vector3(roomWidth, wallThickness + 1.2f, 1);
        bottomWall.transform.localScale = new Vector3(roomWidth, wallThickness, 1);
    }
}
