using System;
using UnityEngine;
using UnityEngine.UI;

public class OutcomeGame : MonoBehaviour
{
    public static Action<int> onShowWhoWins;
    public static Action onStopGame;
    [SerializeField] private Button _white;
    [SerializeField] private Button _black;
    [SerializeField] private Button _buttonRestart;

    private void OnEnable()
    {
        onShowWhoWins += ShowWhoWins;
        RestartGame.onRestartGame += DeactiveButton;
    } 

    private void ShowWhoWins(int whoWins)
    {
        onStopGame?.Invoke();
        if (whoWins == 0)
            _white.gameObject.SetActive(true);
        else
            _black.gameObject.SetActive(true);
        _buttonRestart.gameObject.SetActive(true);
    }

    public void DeactiveButton()
    {
        _white.gameObject.SetActive(false);
        _black.gameObject.SetActive(false);
        _buttonRestart.gameObject.SetActive(false);
    }
}
