using System;
using UnityEngine;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    public static Action onRestartGame;
    [SerializeField] private Button _buttonRestart;
    private void OnEnable() => SettingGame.onActivatedSettingsGame += ActiveButtonRestart;
    private void ActiveButtonRestart()
    {
        if (_buttonRestart.gameObject.activeInHierarchy == true)
            _buttonRestart.gameObject.SetActive(false);
        else
            _buttonRestart.gameObject.SetActive(true);
    }
    public void Restart() => onRestartGame?.Invoke();
   
}
