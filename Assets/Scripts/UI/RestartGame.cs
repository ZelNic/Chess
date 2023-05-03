using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public void RestartScene()
    {
       Distributor.onDestroyPieces?.Invoke();
       CreaterAllChessPiece.onCreateAllChessPiece?.Invoke();
    }
}
