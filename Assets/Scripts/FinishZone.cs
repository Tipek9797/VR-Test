using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishZone : MonoBehaviour
{
    private KeyController keyController;

    private void OnTriggerEnter(Collider other)
    {
        if (keyController == null)
        {
            keyController = FindObjectOfType<KeyController>();
        }

        bool win = keyController != null && keyController.CanWin();
        if (other.CompareTag("Player") && win)
        {
            TriggerWin();
        }
    }

    void TriggerWin()
    {
        SceneManager.LoadScene("WinScene");
    }
}
