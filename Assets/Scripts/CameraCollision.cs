using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform cameraPivot; // Usually the player's head or upper body
    public float minDistance = 1f;
    public float maxDistance = 4f;
    public float smoothSpeed = 10f;
    public LayerMask collisionLayers = ~0;

    private float currentDistance;
    private Vector3 currentVelocity;

    void Start()
    {
        currentDistance = maxDistance;
    }

    void LateUpdate()
    {
        // Desired position based on max distance
        Vector3 direction = (transform.position - cameraPivot.position).normalized;
        Vector3 desiredCameraPos = cameraPivot.position + direction * maxDistance;

        RaycastHit hit;
        if (Physics.Linecast(cameraPivot.position, desiredCameraPos, out hit, collisionLayers))
        {
            currentDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            currentDistance = maxDistance;
        }

        // Smoothly move camera to new position
        Vector3 finalPos = cameraPivot.position + direction * currentDistance;
        transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref currentVelocity, 1f / smoothSpeed);

        // Optional: make camera look at the pivot
        transform.LookAt(cameraPivot);
    }
}
