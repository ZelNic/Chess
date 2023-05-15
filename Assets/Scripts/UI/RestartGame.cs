using System;
using UnityEngine;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private Button _buttonRestart;
    public static Action onRestartGame;
    
    private void OnEnable()
    {
        Setting.onActivatedSettingsGame += ActiveButtonRestart;
    }

    private void ActiveButtonRestart()
    {
        if (_buttonRestart.gameObject.activeInHierarchy == true)
            _buttonRestart.gameObject.SetActive(false);
        else
            _buttonRestart.gameObject.SetActive(true);
    }

    public void Restart()
    {
       onRestartGame?.Invoke();
    }
}
