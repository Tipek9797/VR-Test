using UnityEngine;

public class KeyController : MonoBehaviour
{
    private bool canWin = false;

    public void CanWinTrue()
    {
        canWin = true; 
    }

    public void CanWinFalse()
    {
        canWin = false;
    }

    public bool CanWin()
    {
        return canWin;
    }
}
