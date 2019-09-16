using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI ins;
    private void Awake()
    {
        ins = this;
    }


    [SerializeField] InventorySlotUI inventorySlotPrefab;
    [SerializeField] GameObject inventoryGrid;
    [SerializeField] List<InventorySlotUI> inventorySlots = new List<InventorySlotUI>();

    void ClearInventorySlots()
    {
        Debug.Log("CLEARING OLD INVENTORY");

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            Destroy(inventorySlots[i].gameObject);
            inventorySlots.RemoveAt(i);
        }
    }

    public void GenerateInventory()
    {
        ClearInventorySlots();

        Debug.Log("GENERATING NEW INVENTORY");

        for (int i = 0; i < Inventory.ins.getAlienCount(); i++)
        {
            InventorySlotUI tmp = Instantiate(inventorySlotPrefab, inventoryGrid.transform);
            inventorySlots.Add(tmp);
            tmp.UpdateInfo(Inventory.ins.getAlien(i).id);
        }
    }
}
