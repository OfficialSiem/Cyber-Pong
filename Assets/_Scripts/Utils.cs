
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToTwoDWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }

    public static Ray ScreenToThreeDWorld(Camera camera, Vector3 position)
    {
        Ray ray = camera.ScreenPointToRay(position);
        return ray;
    }
}
