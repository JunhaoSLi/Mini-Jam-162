using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedCamera : MonoBehaviour
{
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = 9f / 2f; // Set orthographic size to 4.5 which is half the Height and by definition will fill a 16:9 aspect ratio
        // TODO: Position in Center of Room
    }
}