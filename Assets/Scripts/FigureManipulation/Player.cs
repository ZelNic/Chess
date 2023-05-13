using System;
using UnityEngine;
public class Player : MonoBehaviour
{
    public static Action<ChessPiece, bool> onChoose—hessPiece;
    [SerializeField] private GameObject _touchpointPiece;
    private ChessPiece _selectedPiece;
    private RaycastHit2D _rayHit;
    private GameObject _rayHitGO;
    private int _countClick = 0;
    private bool _isStepWhite = true;
    private bool _isGameOver = false;

    public bool IsStepWhite { get { return _isStepWhite; } private set { _isStepWhite = value; } }
    public bool IsGameOver { get { return _isGameOver; } private set { _isGameOver = value; } }
    private void OnEnable()
    {
        Distributor.onDestroyPieces += SetIsStepWhite;
        Distributor.onWasMadeMove += PlayerSwitch;
        Judge.onStopGame += SetIsGameOver;
        RestartGame.onRestartGame += SetIsNoGameOver;
        RestartGame.onRestartGame += SetIsStepWhite;
    }
    private void Reset()
    {
        IsStepWhite = true;
        IsGameOver = false;
        _touchpointPiece.gameObject.SetActive(false);
    }
    private void SetIsStepWhite() => IsStepWhite = true;
    private void SetIsNoGameOver() => IsGameOver = false;
    private void PlayerSwitch() => IsStepWhite = IsStepWhite == true ? false : true;
    private void SetIsGameOver() => IsGameOver = true;
    private void Update() => SelectObject();
    private void SelectObject()
    {
        if (Input.GetMouseButtonDown(0) && IsGameOver == false)
        {
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _rayHit = Physics2D.Raycast(cursorPosition, Vector2.zero);
            if (_rayHit.collider != null)
                _rayHitGO = _rayHit.collider.gameObject;

            Tile.onSetDefautColor?.Invoke();
            _touchpointPiece.gameObject.SetActive(false);
            if (_rayHit.collider != null && _rayHitGO.gameObject.GetComponent<ChessPiece>() != null && _countClick == 0)
                if ((IsStepWhite == true && _rayHitGO.GetComponent<ChessPiece>().team == 0) || (IsStepWhite == false && _rayHitGO.GetComponent<ChessPiece>().team == 1))
                {
                    _touchpointPiece.gameObject.SetActive(true);
                    _selectedPiece = _rayHitGO.GetComponent<ChessPiece>();
                    _touchpointPiece.transform.position = _rayHitGO.transform.position;
                    _countClick++;
                    onChoose—hessPiece?.Invoke(_selectedPiece, true);
                }

            if (_countClick > 0 && _rayHitGO.gameObject.GetComponent<Tile>() != null)
            {
                _countClick = 0;
                _touchpointPiece.transform.position = _rayHitGO.transform.position;
                Distributor.onChangePositionPiece?.Invoke(_selectedPiece, (int)_rayHitGO.transform.position.x, (int)_rayHitGO.transform.position.y);
            }

            if (_countClick > 0 && _rayHitGO.gameObject.GetComponent<ChessPiece>() != null)
            {
                if (_selectedPiece.team != _rayHitGO.GetComponent<ChessPiece>().team)
                {
                    _countClick = 0;
                    Distributor.onChangePositionPiece?.Invoke(_selectedPiece, (int)_rayHitGO.transform.position.x, (int)_rayHitGO.transform.position.y);
                }

                if (_selectedPiece.team == _rayHitGO.GetComponent<ChessPiece>().team)
                {
                    _touchpointPiece.gameObject.SetActive(true);
                    _touchpointPiece.transform.position = _rayHitGO.transform.position;
                    _selectedPiece = _rayHitGO.GetComponent<ChessPiece>();
                    onChoose—hessPiece?.Invoke(_selectedPiece, true);
                }
            }
        }
    }
}
