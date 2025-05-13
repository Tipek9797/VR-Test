using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KeyController keyController = FindObjectOfType<KeyController>();
            if (keyController != null)
            {
                keyController.CanWinTrue();
            }

            Destroy(gameObject);
        }
    }
}
