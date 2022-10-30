using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static PlayerInputs.BasicInputsActions BasicInputs;
    
    private PlayerInputs _playerInputs;

    private void Awake()
    {
        _playerInputs = new PlayerInputs();
        BasicInputs = _playerInputs.BasicInputs;
    }

    private void OnEnable()
    {
        _playerInputs.Enable();
    }

    private void OnDisable()
    {
        _playerInputs.Disable();
    }
}
