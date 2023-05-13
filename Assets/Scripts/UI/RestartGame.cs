using System;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public static Action onRestartGame;
    public void Restart()
    {
       onRestartGame?.Invoke();
    }
}
