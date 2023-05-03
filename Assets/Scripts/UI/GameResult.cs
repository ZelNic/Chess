using System;
using UnityEngine;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    public static Action<int> onShowWhoWins;
    [SerializeField] private Button _white;
    [SerializeField] private Button _black;

    private void OnEnable()
    {
        onShowWhoWins += ShowWhoWins;
    }

    private void ShowWhoWins(int whoWins)
    {
        if (whoWins == 0)
            _white.gameObject.SetActive(true);
        else
            _black.gameObject.SetActive(true);
    }

    public void DeactiveButton()
    {
        _white.gameObject.SetActive(false);
        _black.gameObject.SetActive(false);
    }



}
