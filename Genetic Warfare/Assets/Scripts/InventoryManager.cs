using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject itemParent;
    private InvSlotScript[] invSlots;
    private int emptySlotIndex;

    public Sprite[] alienIcons;

    private int slotClicked;
    private int alienClicked;

    private IDManager idManager;

    void Awake()
    {
        // Store all the inventory slots in the invSlots array
        invSlots = itemParent.GetComponentsInChildren<InvSlotScript>();

         // Wipe all the previously saved data for the inventory, since it we load it from LoadAndSave at the start of every scene
        for (int i = 0; i < invSlots.Length; i++)
        {
            PlayerPrefs.DeleteKey("slot" + invSlots[i]);
        }

        // Get the ID Manager for later use
        idManager = GameObject.Find("DataManager").GetComponent<IDManager>();
    }

    public void AddToInventory(int alienID)
    {
        if (idManager.GetInventorySlot(alienID) == -1) // If the alien doesn't have a pre-defined slot
        {
            // Get an empty slot index
            for (int i = 0; i < invSlots.Length; i++)
            {
                if (invSlots[i].IsEmpty())
                {
                    emptySlotIndex = i;
                    break;
                }
            }

            // Set the inventory slot of the alien to the empty one we just found
            idManager.SetInventorySlot(alienID, emptySlotIndex);
        } else { 
            
            // If the alien already has a slot then set the empty slot index to that slot
            emptySlotIndex = idManager.GetInventorySlot(alienID);           
        }

        // Set slot value to a string of the aliens stats. Slot4 = "1/0/1/etc.." so we can retrieve anywhere     
        PlayerPrefs.SetString("slot" + emptySlotIndex.ToString(), alienID.ToString());

        // Set the image of the slot to be identical to the alien
        itemParent.transform.GetChild(emptySlotIndex).GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
        itemParent.transform.GetChild(emptySlotIndex).GetChild(0).GetChild(0).GetComponent<Image>().sprite = alienIcons[idManager.GetAlienType(alienID)];

        if (idManager.GetPosition(alienID) == 2) // If the alien is on the floor make it's button un-interactable
        {
            itemParent.transform.GetChild(emptySlotIndex).GetChild(0).GetComponent<Button>().interactable = false;
        }

        // Set the inventory slot we're inserting into to be not empty
        invSlots[emptySlotIndex].MakeNotEmpty();
    }

    public void SlotClicked(int slot)
    {
        if (!invSlots[slot].IsEmpty())
        {
            GameObject.Find("AlienInfoPanel").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            GameObject.Find("AlienInfoPanel").GetComponent<CreateStatBox>().CreatePanel(int.Parse(PlayerPrefs.GetString("slot"+slot.ToString()))); // Create panel from slot name
            alienClicked = int.Parse(PlayerPrefs.GetString("slot" + slot.ToString()));
            slotClicked = slot;
        }
    }

    public void AlienOnFloor(bool inInventory, int slotID) // When an alien is moved to the floor we update it's inventory icon so we know it's been placed
    {
        itemParent.transform.GetChild(slotID).GetChild(0).GetComponent<Button>().interactable = inInventory;      
    }

    public void KillButtonClicked() // This function uses the slot clicked function's alienClicked variable to tell it which one to delete
    {
        itemParent.transform.GetChild(slotClicked).GetChild(0).GetChild(0).GetComponent<Image>().enabled = false; // Use the slot integer to wipe the slot clear
        itemParent.transform.GetChild(slotClicked).GetChild(0).GetChild(0).GetComponent<Image>().sprite = null;
        invSlots[slotClicked].MakeEmpty(); // Make the slot unable to be clicked
        GameObject.Find("AlienInfoPanel").GetComponent<CreateStatBox>().ClosePanel(); // Close the info panel
        GameObject.Find("DataManager").GetComponent<LoadAndSave>().DeleteAlien(alienClicked); // Then we send it to LoadAndSave to be wiped from the data structures
    }
}
