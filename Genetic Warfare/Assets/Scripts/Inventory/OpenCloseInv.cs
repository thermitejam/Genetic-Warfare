using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseInv : MonoBehaviour
{
    public void OpenClose()
    {
        gameObject.GetComponent<Animator>().SetBool("open", !gameObject.GetComponent<Animator>().GetBool("open"));        
    }
}
