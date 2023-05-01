using UnityEngine;

public class Restart : MonoBehaviour
{
    public void RestartScene()
    {
       Distributor.onDestroyPieces?.Invoke();
       CreaterAllChessPiece.onCreateAllChessPiece?.Invoke();
    }
}
