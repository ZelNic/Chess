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
        
    }

    public void Restart()
    {
       onRestartGame?.Invoke();
    }
}
