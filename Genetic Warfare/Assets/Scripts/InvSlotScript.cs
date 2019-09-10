using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvSlotScript : MonoBehaviour
{
    private bool empty = true;

    public bool IsEmpty()
    {
        return empty;
    }

    public void MakeNotEmpty()
    {
        empty = false;
    }

    public void MakeEmpty()
    {
        empty = true;
    }
}
