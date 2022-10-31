using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private static Transform _camTransform;

    private static Vector3 _pos;
    
    private void Awake()
    {
        _camTransform = transform;
    }

    public static void AlignWithActiveMovable(Transform movable)
    {
        
    }
}
