using UnityEngine;

public class CameraOptions : MonoBehaviour
{
    private void OnEnable() => BoardCreator.onSendSize += TuneCamera;
    private void TuneCamera(int SizeByX, int SizeByY)
    {
        int coefficient = 5;
        int optimalPosZ = -10;
        float x = Screen.width;
        float y = Screen.height;
        float proportion = y / x;
        Camera.main.orthographicSize = proportion * coefficient;
        Camera.main.transform.position = new Vector3(SizeByX / 2 - 0.5f, SizeByY / 2 - 0.5f, optimalPosZ);
    }
}