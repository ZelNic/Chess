using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingGame : MonoBehaviour
{
    public static Action onActivatedSettingsGame;
    [SerializeField] private Button _buttonSetting;
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _pressSprite;
    private Image _imageButton;
    private bool _isPressed = false;
    public void SwitchActiveSetting() => onActivatedSettingsGame?.Invoke();

    private void OnEnable() => onActivatedSettingsGame += SwitchSprite;

    private void Start()
    {
        _imageButton = _buttonSetting.GetComponent<Image>();
    }
    private void SwitchSprite()
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
    }
            


}
