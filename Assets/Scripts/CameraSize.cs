using UnityEngine;

public class CameraSize : MonoBehaviour
{
    private void Awake()
    {
        int coefficient = 5;
        float x = Screen.width;
        float y = Screen.height;
        float proportion = y / x;
        Camera.main.orthographicSize = proportion * coefficient;
    }
}