using System;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public static Action onRestartGame;
    public void RestartScene()
    {
       Distributor.onDestroyPieces?.Invoke();
       CreaterAllChessPiece.onCreateAllChessPiece?.Invoke();
       onRestartGame?.Invoke();
    }
}
