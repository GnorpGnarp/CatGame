using UnityEngine;
using System.Collections;

public class DoorUnlock : MonoBehaviour
{
    public GameObject promptUI; // Assign your prompt Canvas here in the Inspector
    public float rotationSpeed = 90f;

    private bool isOpen = false;
    private bool isOpening = false;
    private bool playerInRange = false;
    private Transform playerTransform;

    private void Update()
    {
        if (playerInRange && !isOpen && !isOpening)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerInventory inventory = playerTransform.GetComponent<PlayerInventory>();
                if (inventory != null && inventory.hasKey)
                {
                    StartCoroutine(OpenDoor());
                    promptUI.SetActive(false); // hide prompt when door starts opening
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOpen || isOpening) return;

        if (other.GetComponent<PlayerInventory>())
        {
            playerInRange = true;
            playerTransform = other.transform;
            promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInventory>())
        {
            playerInRange = false;
            promptUI.SetActive(false);
        }
    }

    private IEnumerator OpenDoor()
    {
        isOpening = true;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, -90, 0);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.rotation = targetRotation;
        isOpen = true;
        isOpening = false;
        Debug.Log("Door opened with key!");
    }
}
