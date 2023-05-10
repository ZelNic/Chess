using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _touchpointPiece;
    private ChessPieceTemplate _selectedPiece;
    private RaycastHit2D _rayHit;
    private GameObject _rayHitGO;
    private int _countClick = 0;
    private bool _isStepWhite = true;
    private bool _isGameOver = false;
    public bool IsGameOver
    {
        get { return _isGameOver; }
        private set
        {
            _isGameOver = value;
        }
    }
    private void OnEnable()
    {
        Distributor.onDestroyPieces += SetIsWhiteTrue;
        Distributor.onWasMadeMove += PlayerSwitch;
        Judge.onStopGame += SelectionSwitch;
    }
    private void SetIsWhiteTrue() => _isStepWhite = true;
    private void PlayerSwitch() => _isStepWhite = _isStepWhite == true ? true : true;
    private void SelectionSwitch() => IsGameOver = IsGameOver == false ? true : false;
    private void Update() => SelectObject();
    private void SelectObject()
    {        
        if (Input.GetMouseButtonDown(0) && _isGameOver == false)
        {
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _rayHit = Physics2D.Raycast(cursorPosition, Vector2.zero);
            if (_rayHit.collider != null)
                _rayHitGO = _rayHit.collider.gameObject;

            Tile.onSetDefautColor?.Invoke();
            _touchpointPiece.gameObject.SetActive(false);
            if (_rayHit.collider != null && _rayHitGO.gameObject.GetComponent<ChessPieceTemplate>() != null && _countClick == 0)
                if ((_isStepWhite == true && _rayHitGO.GetComponent<ChessPieceTemplate>().team == 0) || (_isStepWhite == false && _rayHitGO.GetComponent<ChessPieceTemplate>().team == 1))
                {
                    _touchpointPiece.gameObject.SetActive(true);
                    _selectedPiece = _rayHitGO.GetComponent<ChessPieceTemplate>();
                    _touchpointPiece.transform.position = _rayHitGO.transform.position;
                    _countClick++;
                    Distributor.onChoose—hessPiece?.Invoke(_selectedPiece);
                }
            
            if (_countClick > 0 && _rayHitGO.gameObject.GetComponent<Tile>() != null)
            {
                _countClick = 0;
                _touchpointPiece.transform.position = _rayHitGO.transform.position;
                Distributor.onChangePositionPiece?.Invoke(_selectedPiece, (int)_rayHitGO.transform.position.x, (int)_rayHitGO.transform.position.y);
            }

            if (_countClick > 0 && _rayHitGO.gameObject.GetComponent<ChessPieceTemplate>() != null)
            {
                if (_selectedPiece.team != _rayHitGO.GetComponent<ChessPieceTemplate>().team)
                {
                    _countClick = 0;
                    Distributor.onChangePositionPiece?.Invoke(_selectedPiece, (int)_rayHitGO.transform.position.x, (int)_rayHitGO.transform.position.y);
                }

                if (_selectedPiece.team == _rayHitGO.GetComponent<ChessPieceTemplate>().team)
                {
                    _touchpointPiece.gameObject.SetActive(true);
                    _touchpointPiece.transform.position = _rayHitGO.transform.position;
                    _selectedPiece = _rayHitGO.GetComponent<ChessPieceTemplate>();
                    Distributor.onChoose—hessPiece?.Invoke(_selectedPiece);
                }
            }
        }
    }
}
