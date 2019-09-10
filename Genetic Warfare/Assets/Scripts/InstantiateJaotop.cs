using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateJaotop : MonoBehaviour
{
    [SerializeField] GameObject jao;
    [SerializeField] GameObject qay;
    public void SpawnJaotop()
    {
        if (Random.Range(0, 5) < 3)
        {
            GameObject JaoInstance = Instantiate(jao);
            JaoInstance.GetComponent<JaotopController>().Create();
            GameObject.Find("Inventory").GetComponent<InventoryManager>().AddToInventory(JaoInstance.GetComponent<JaotopController>().GetID());
            GameObject.Find("DataManager").GetComponent<IDManager>().SetPosition(JaoInstance.GetComponent<JaotopController>().GetID(), 1);
            Destroy(JaoInstance);
        } else
        {
            GameObject QayInstance = Instantiate(qay);
            QayInstance.GetComponent<QayratController>().Create();
            GameObject.Find("Inventory").GetComponent<InventoryManager>().AddToInventory(QayInstance.GetComponent<QayratController>().GetID());
            GameObject.Find("DataManager").GetComponent<IDManager>().SetPosition(QayInstance.GetComponent<QayratController>().GetID(), 1);
            Destroy(QayInstance);
        }
    }
}
