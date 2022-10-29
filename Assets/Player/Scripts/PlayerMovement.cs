using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask ignoreLayer;
     
    private PlayerInputs _playerInputs;
    private Transform _transform;
    
    private void Awake()
    {
        _playerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        _playerInputs.Enable();
    }

    private void OnDisable()
    {
        _playerInputs.Disable();
    }

    private void Start()
    {
        _transform = transform;
        _playerInputs.PlayerMovement.Movement.performed += MovementPerformed;
    }

    private void MovementPerformed(InputAction.CallbackContext obj)
    {
        RotatePlayer();
        CheckForPlatform();
    }

    private void RotatePlayer()
    {
        var move = _playerInputs.PlayerMovement.Movement.ReadValue<Vector2>();
        
        if (move.x != 0f)
            _transform.rotation = Quaternion.Euler(0f, 90f * move.x, 0f);
        else
            _transform.rotation = move.y switch
            {
                > 0f => Quaternion.Euler(Vector3.zero),
                < 0f => Quaternion.Euler(0f, 180f, 0f),
                _ => _transform.rotation
            };
    }

    private void CheckForPlatform()
    {
        if (!Physics.Raycast(_transform.position, _transform.forward, out var hit, Mathf.Infinity, ~ignoreLayer)) return;
        
        var hitPos = hit.transform.position;
        var pos = new Vector3(hitPos.x, _transform.position.y, hitPos.z);
        
        _transform.DOMove(pos, .1f);
    }
}
