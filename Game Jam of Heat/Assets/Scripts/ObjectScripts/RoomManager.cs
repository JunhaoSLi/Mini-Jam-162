using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject roomCamera;
    [SerializeField] private int maxRooms = 20;
    [SerializeField] private int minRooms = 15;


    int roomWidth = 16; // Width of the room
    int roomHeight = 9; // Height of the room
    // Grid of Rooms to be generated
    [SerializeField] int gridX = 10;
    [SerializeField] int gridY = 10;

    private List<GameObject> roomObjects = new List<GameObject>();
    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();
    private int[,] roomGrid;
    private int roomCount;
    private bool generationComplete = false;
    private GameObject startingRoom;

    private void Start()
    {
        roomGrid = new int[gridX, gridY];
        roomQueue = new Queue<Vector2Int>();

        Vector2Int initialRoomIndex = new Vector2Int(gridX / 2 , gridY / 2); // Start room generation from center of Grid
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private void Update()
    {
        if(roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue(); // Get the prev room's position in the room grid
            int x = roomIndex.x;
            int y = roomIndex.y;

            TryGenerateRoom(new Vector2Int(x - 1, y));
            TryGenerateRoom(new Vector2Int(x + 1, y));
            TryGenerateRoom(new Vector2Int(x, y - 1));
            TryGenerateRoom(new Vector2Int(x, y + 1));
        } 
        else if (!generationComplete)
        {
            Debug.Log($"Generation Complete, {roomCount} rooms created");
            generationComplete = true;
            startingRoom.GetComponent<Room>().Activate();
        } 
        else if (roomCount < minRooms)
        {
            Debug.Log($"Regenerating Rooms, {roomCount} was less than {minRooms}");
            RegenerateRooms();
        }
    }

    /**
     * Starts the generation of rooms from the grid index passed
     */
    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        // Queues up rooms to be created
        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;

        // Creates first room in given index
        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
        startingRoom = initialRoom;
    }

    /** 
     * Attemps to create a new room. Returns True if we manage to create a new Room, False if we do not create a new room.
     * Creates the new room at a random X or Y above the initial room.
     */
    private bool TryGenerateRoom(Vector2Int roomIndex)
    {

        int x = roomIndex.x;
        int y = roomIndex.y;

        // If we have too many rooms, don't generate more
        if (roomCount >= maxRooms)
        {
            return false;
        }

        // Randomly generate or don't generate a room. Don't generate if we loop back to center
        if (Random.value < 0.5f && roomIndex != Vector2Int.zero)
        {
            return false;
        }

        // If we have a room already in that grid, don't generate
        if (isRoomTaken(roomIndex))
        {
            return false; 
        }

        // Rooms will not cluster often
        if (CountAdjacentRooms(roomIndex) > 1)
        {
            return false;
        }
        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;

        var nextRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        nextRoom.GetComponent<Room>().RoomIndex = roomIndex;
        nextRoom.name = $"Room-{roomCount}";
        roomObjects.Add(nextRoom);

        OpenDoors(nextRoom, x, y);

        return true;
    }

    /**
     * Clears all rooms and try again if generation went poorly
     */
    private void RegenerateRooms()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridX, gridY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridX / 2, gridY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private bool isRoomTaken(Vector2Int roomIndex)
    {
        return roomGrid[roomIndex.x, roomIndex.y] == 1;
    }

    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        // If we have a neighbor, count it
        if (x > 0 && roomGrid[x - 1, y] != 0) { count++; } // left neighbor
        if (x < gridX-1 && roomGrid[x + 1, y] != 0) { count++; } // right neighbor
        if (y > 0 && roomGrid[x, y - 1] != 0) { count++; } // down neighbior
        if (y < gridY-1 && roomGrid[x, y + 1] != 0) { count++; } // up neighbor

        return count;
    }

    void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();

        // Neighbors
        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

        // Determine which doors to open based on the direction
        if (x > 0 && roomGrid[x - 1, y] != 0)
        {
            // Left Neighbor
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);
        }
        if (x < gridX-1 && roomGrid[x + 1, y] != 0)
        {
            // Right Neighbor
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);
        }
        if (y > 0 && roomGrid[x, y - 1] != 0)
        {
            // Below Neighbor
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }
        if (y < gridY - 1 && roomGrid[x, y + 1] != 0)
        {
            // Top Neighbor
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
        }
    }

    Room GetRoomScriptAt(Vector2Int index)
    {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null)
        {
            return roomObject.GetComponent<Room>();
        }
        return null;
    }

    /** 
     * Calculates the position of the Room based on the Grid Index given
     */
    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        return new Vector3(roomWidth * (gridIndex.x - gridX / 2) , roomHeight * (gridIndex.y - gridY / 2));
    }

    /** 
     * For debugging the room grid
     */
    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }
}
