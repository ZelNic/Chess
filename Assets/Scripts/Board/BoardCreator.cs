using System;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    public static Action<int, int> onSendSize;
    public static Func<Tile[,]> onSendArrayTile;
    [SerializeField] private Tile _tile;
    [SerializeField] private int _sizeByX;
    [SerializeField] private int _sizeByY;
    [SerializeField] private Transform _cameraTransform;
    private Tile[,] _arrayTile;
    public int SizeByX { get { return _sizeByX; } }
    public int SizeByY { get { return _sizeByY; } }

    private void Awake()
    {
        CreateBoard();
    }
    private void Start()
    {        
        onSendSize?.Invoke(SizeByX, SizeByY);
        CreaterAllChessPiece.onCreateAllChessPiece?.Invoke();
        _cameraTransform.position = new Vector3(_sizeByX / 2 - .5f, _sizeByY / 2 - .5f, -10);
    }
    private void OnEnable()
    {
        onSendArrayTile += SendArrayTile;
    }

    private Tile[,] SendArrayTile()
    {
        return _arrayTile;
    }


    public void CreateBoard()
    {
        _arrayTile = new Tile[_sizeByX, _sizeByY];
        for (int x = 0; x < _sizeByX; x++)
            for (int y = 0; y < _sizeByY; y++)
                _arrayTile[x, y] = CreateSingleTile(x, y);
    }
    public Tile CreateSingleTile(int x, int y)
    {
        Tile tempt = Instantiate(_tile, new Vector3(x, y), Quaternion.identity, transform);
        tempt.name = $"Y:{y} X:{x}";
        bool isOffSet = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
        tempt.ChangeColor(isOffSet);
        return tempt;
    }

}
