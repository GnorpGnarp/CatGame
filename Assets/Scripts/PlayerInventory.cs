using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasKey = false;

    public void CollectKey()
    {
        hasKey = true;
        Debug.Log("Key collected!");
    }
}
