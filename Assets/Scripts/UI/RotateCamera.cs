using UnityEngine;
using System;
using UnityEngine.UI;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private Button _buttonRotate;
    public static Action onEnabledRotate;

    private void OnEnable()
    {
        Setting.onActivatedSettingsGame += ActivateRotateCamera;
    }

    public void ActivateRotateCamera()
    {
       onEnabledRotate?.Invoke();
    }
}
