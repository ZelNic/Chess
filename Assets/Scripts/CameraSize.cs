using UnityEngine;

public class CameraSize : MonoBehaviour
{
    private void Awake()
    {
        float x = Screen.width;
        float y = Screen.height;
        float proportion = y / x;
        Camera.main.orthographicSize = proportion * 5f;
    }
}