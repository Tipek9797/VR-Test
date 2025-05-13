using UnityEngine;

public class KeyController : MonoBehaviour
{
    private FinishZone finishZone;

    private void Start() {
        finishZone = FindObjectOfType<FinishZone>();
    }

    public void CanWinTrue()
    {
        finishZone.SetCanWinTrue();
        Debug.Log("canwintrue in keyController");
        Destroy(transform.parent.gameObject);

    }
}
