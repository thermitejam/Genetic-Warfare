using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public static InventoryScript inventory;

    void Start()
    {
        inventory = this;    
    }

}
