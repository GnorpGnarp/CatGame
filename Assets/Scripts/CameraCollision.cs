using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform cameraPivot; // The point the camera orbits around (usually the player)
    public float minDistance = 1f;  // How close the camera can get
    public float maxDistance = 4f;  // How far the camera sits normally
    public float smoothSpeed = 10f;
    public LayerMask collisionLayers;

    private float currentDistance;
    private Vector3 desiredCameraPos;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        currentDistance = maxDistance;
    }

    void LateUpdate()
    {
        desiredCameraPos = cameraPivot.position - transform.forward * maxDistance;

        RaycastHit hit;
        if (Physics.Linecast(cameraPivot.position, desiredCameraPos, out hit, collisionLayers))
        {
            currentDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            currentDistance = maxDistance;
        }

        // Move camera to adjusted position
        transform.position = Vector3.Lerp(transform.position, cameraPivot.position - transform.forward * currentDistance, Time.deltaTime * smoothSpeed);
    }
}
