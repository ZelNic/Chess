using System;
using UnityEngine;
using UnityEngine.UI;

public class RotateCamera : MonoBehaviour
{
    public static Action onEnabledRotate;
    [SerializeField] private Button _buttonRotate;
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _pressSprite;
    private Image _imageButton;
    private bool _isPressed = false;

    private void OnEnable()
    {
        Setting.onActivatedSettingsGame += SwitchActiveButton;
    }

    private void Start()
    {
        _imageButton = _buttonRotate.GetComponent<Image>();
    }
    private void SwitchActiveButton()
    {
        if (_buttonRotate.gameObject.activeInHierarchy == true)

            _buttonRotate.gameObject.SetActive(false);

        else
            _buttonRotate.gameObject.SetActive(true);
    }
    public void ActivateRotateCamera()
    {
        if (_isPressed == false)
        {
            _imageButton.sprite = _pressSprite;
            _isPressed = true;
        }
        else
        {
            _imageButton.sprite = _normalSprite;
            _isPressed = false;
        }
        onEnabledRotate?.Invoke();
    }

}
