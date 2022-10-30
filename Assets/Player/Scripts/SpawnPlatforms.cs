using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private LayerMask ignorePlatform;
    [SerializeField] private LayerMask ignoreSpawnLoc;
    [SerializeField] private PlayerMovement playerMovement;

    private bool _triggeredOnce;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Platform Mesh")) return;
        if (_triggeredOnce)
            GeneratePlatform();
        _triggeredOnce = true;
    }

    private void GeneratePlatform()
    {
        if (!Physics.Raycast(_transform.position, _transform.forward, out var hit, playerMovement.maxDistance,
                ~ignorePlatform)) return;
        if (Physics.Raycast(_transform.position + _transform.forward * .2f, _transform.forward,
                playerMovement.maxDistance, ~ignoreSpawnLoc)) return;
        
        var platformInstance = Instantiate(platform, hit.transform.position,
            Quaternion.Inverse(hit.transform.rotation));

        platformInstance.transform.GetChild(0).GetChild(0).transform.rotation =
            Quaternion.Euler(0f, platformInstance.transform.rotation.y * -1f, 0f);
    }

}
