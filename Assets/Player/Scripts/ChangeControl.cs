using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeControl : MonoBehaviour
{
    [SerializeField] private LayerMask ignoreLayers;

    private PlayerMovement[] _movableObjects;
    
    private void Start()
    {
        _movableObjects = FindObjectsOfType<PlayerMovement>();
        InputManager.BasicInputs.ChangePlayer.performed += ChangePlayer;
    }

    private void ChangePlayer(InputAction.CallbackContext obj)
    {
        var clickedObject = GetClickedObject();
        var playerMovement = clickedObject.GetComponent<PlayerMovement>();
        playerMovement.moving = true;
        
        foreach (var movableObject in _movableObjects)
        {
            if (movableObject != playerMovement)
                movableObject.moving = false;
        }
    }

    private Transform GetClickedObject()
    {
        if (Camera.main == null) return null;
        
        var pos = Mouse.current.position.ReadValue();
        var ray = Camera.main.ScreenPointToRay(pos);

        bool isHit = Physics.Raycast(ray, out var hit, Mathf.Infinity, ~ignoreLayers);
        return isHit ? hit.transform : null;
    }
}
