using DG.Tweening;
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
        if (GetClickedObject() == null) return;
        var clickedObject = GetClickedObject();
        var playerMovement = clickedObject.GetComponent<PlayerMovement>();
        playerMovement.moving = true;
        playerMovement.transform.GetChild(0).DOScale(Vector3.one * 2f, .1f);
        
        foreach (var movableObject in _movableObjects)
        {
            if (movableObject == playerMovement) continue;
            movableObject.moving = false;
            movableObject.transform.GetChild(0).DOScale(Vector3.one, .1f);
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
