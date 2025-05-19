using UnityEngine;

public class KeyItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            inventory.CollectKey();
            Destroy(gameObject); // Remove key from scene
        }
    }
}
