using UnityEngine;

public class RotationControllerChess : MonoBehaviour
{
    private ChessPiece[,] _mapCP;
    private void OnEnable()
    {
        Distributor.onStartDistribution += GetMapChessPiece;
        CameraOptions.onRotateCamera += TurnPieces;
        RestartGame.onRestartGame += ResetRotation;
    }
    private void GetMapChessPiece(ChessPiece[,] mapCP) => _mapCP = mapCP;
    private void TurnPieces()
    {
        for (int i = 0; i < _mapCP.GetLength(0); i++)
            for (int j = 0; j < _mapCP.GetLength(1); j++)
                if (_mapCP[i, j] != null)
                    _mapCP[i, j].transform.Rotate(new Vector3(0, 0, 180));
    }
    private void ResetRotation()
    {
        for (int i = 0; i < _mapCP.GetLength(0); i++)
            for (int j = 0; j < _mapCP.GetLength(1); j++)
                if (_mapCP[i, j] != null)
                    _mapCP[i, j].transform.rotation = new Quaternion(0, 0, 0, 1);
    }
}
