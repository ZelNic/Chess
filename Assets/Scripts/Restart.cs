using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartScene()
    {
       Distributor.onSendPiecesToScrap?.Invoke();
       CreaterAllChessPiece.onCreateAllChessPiece?.Invoke();
    }
}
