using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float maxDistance;
    public bool moving = true;

    [SerializeField] private LayerMask ignoreLayer;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        InputManager.BasicInputs.Movement.performed += MovementPerformed;
    }

    public void CheckPlatformBelow()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, Mathf.Infinity, ignoreLayer))
            StartCoroutine(RestartScene());
    }
    
    private void MovementPerformed(InputAction.CallbackContext obj)
    {
        if (!moving) return;
        RotatePlayer();
        CheckForPlatform();
    }

    private void RotatePlayer()
    {
        var move = InputManager.BasicInputs.Movement.ReadValue<Vector2>();

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
        if (!Physics.Raycast(_transform.position, _transform.forward, out var hit, maxDistance, ~ignoreLayer)) return;

        var hitPos = hit.transform.position;
        var pos = new Vector3(hitPos.x, _transform.position.y, hitPos.z);

        _transform.DOMove(pos, .1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Toxic")) return;
        if (other.gameObject.GetComponent<PlayerMovement>())
            StartCoroutine(RestartScene());
    }

    private static IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
