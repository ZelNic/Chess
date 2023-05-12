using System;
using UnityEngine;

public class CameraOptions : MonoBehaviour
{
    public static Action onRotateCamera;
    private void OnEnable()
    {
        BoardCreator.onSendSize += CustomizeCamera;
        Distributor.onWasMadeMove += RotateCamera;
        RestartGame.onRestartGame += ResetAngleRotation;
    }
    private void CustomizeCamera(int SizeByX, int SizeByY)
    {
        float coefficient = 4.5f;
        int optimalPosZ = -10;
        float x = Screen.width;
        float y = Screen.height;
        float proportion = y / x;
        Camera.main.orthographicSize = proportion * coefficient;
        Camera.main.transform.position = new Vector3(SizeByX / 2 - 0.5f, SizeByY / 2 - 0.5f, optimalPosZ);
    }
    private void RotateCamera()
    {
        Camera.main.transform.Rotate(new Vector3(0, 0, 180));
        onRotateCamera.Invoke();
    }
    private void ResetAngleRotation()
    {
        Camera.main.transform.rotation = new Quaternion(0,0,0,1);
    }
}