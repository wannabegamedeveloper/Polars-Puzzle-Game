using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    [SerializeField] private MovableType type;
    [SerializeField] private GameObject platform;
    [SerializeField] private LayerMask ignorePlatform;
    [SerializeField] private LayerMask ignoreSpawnLoc;
    [SerializeField] private PlayerMovement thisPlayer;
    [SerializeField] private PlayerMovement player;

    private bool _triggeredOnce;
    private Transform _transform;

    private enum MovableType
    {
        Player, Toxic
    }
    
    private void Start()
    {
        _transform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Platform Mesh")) return;
        if (_triggeredOnce)
        {
            CameraMovement.AlignWithActiveMovable(transform);
            if (type == MovableType.Player)
                GeneratePlatform();
            else if (type == MovableType.Toxic)
                RemovePlatform();
        }

        _triggeredOnce = true;
    }

    private void RemovePlatform()
    {
        if (Physics.Raycast(_transform.position + _transform.forward * .2f, _transform.forward, out var hit,
                thisPlayer.maxDistance, ~ignoreSpawnLoc))
        {
            Destroy(hit.transform.gameObject);
            if (player == null) return;
            player.CheckPlatformBelow();
        }
    }

    private void GeneratePlatform()
    {
        if (!Physics.Raycast(_transform.position, _transform.forward, out var hit, thisPlayer.maxDistance,
                ~ignorePlatform)) return;
        if (Physics.Raycast(_transform.position + _transform.forward * .2f, _transform.forward,
                thisPlayer.maxDistance, ~ignoreSpawnLoc)) return;

        var platformInstance = Instantiate(platform, hit.transform.position,
            Quaternion.Inverse(hit.transform.rotation));

        platformInstance.transform.GetChild(0).GetChild(0).transform.rotation =
            Quaternion.Euler(0f, platformInstance.transform.rotation.y * -1f, 0f);
    }

}
