using UnityEngine;

public class YarnCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        YarnManager manager = FindObjectOfType<YarnManager>();
        if (other.CompareTag("Player") && manager != null)
        {
            manager.CollectYarn();
            Destroy(gameObject);
        }
    }
}
