
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenCoordinatesTo2DWorldCoordinates(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }

    public static Ray ScreenCoordinatesTo3DWorldCoordinates(Camera camera, Vector3 position)
    {
        Ray ray = camera.ScreenPointToRay(position);
        return ray;
    }
}
