using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishZone : MonoBehaviour
{
    private bool canWin = false;

    public void SetCanWinTrue()
    {
        canWin = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("can win ? : " +canWin);
        Debug.Log("who are you ? : "+ other.name);
        if (other.CompareTag("Player") && canWin)
        {
            TriggerWin();
        }
    }

    void TriggerWin()
    {
        SceneManager.LoadScene("WinScene");
    }
}
