using System;
using UnityEngine;
using UnityEngine.UI;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private Button _buttonRotate;
    public static Action onEnabledRotate;

    private void OnEnable()
    {
        Setting.onActivatedSettingsGame += SwitchActiveButton;
    }

    private void SwitchActiveButton()
    {
        if (_buttonRotate.gameObject.activeInHierarchy == true)

            _buttonRotate.gameObject.SetActive(false);

        else
            _buttonRotate.gameObject.SetActive(true);
    }
    public void ActivateRotateCamera() => onEnabledRotate?.Invoke();
}
