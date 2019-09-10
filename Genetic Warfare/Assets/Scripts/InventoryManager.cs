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
    private Sprite alienSprite;

    private Color tempCol;

    private string[] unpackedID;

    private int slotClicked;
    private int alienClicked;

    void Awake()
    {
        // Store all the inventory slots in the invSlots array
        invSlots = itemParent.GetComponentsInChildren<InvSlotScript>();

         // Wipe all the previously saved data for the inventory, since it we load it from LoadAndSave at the start of every scene
        for (int i = 0; i < invSlots.Length; i++)
        {
            PlayerPrefs.DeleteKey("slot" + invSlots[i]);
        }
    }

    public void AddToInventory(int alienID)
    {
        if (int.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[11]) == -1) // If the alien doesn't have a pre-defined slot
        {
            // search for empty slot, don't need to save the slot emptyness as a playerpref because the inventory is wiped every scene change
            for (int i = 0; i < invSlots.Length; i++)
            {
                if (invSlots[i].IsEmpty())
                {
                    emptySlotIndex = i;
                    break;
                }
            }

            // Then we have to unpack the ID and set the aliens slot index to the newly selected one
            unpackedID = PlayerPrefs.GetString(alienID.ToString()).Split('/');
            unpackedID[11] = emptySlotIndex.ToString();
            PlayerPrefs.SetString(alienID.ToString(), unpackedID[0] + "/" + unpackedID[1] + "/" + unpackedID[2] + "/" + unpackedID[3] + "/" + unpackedID[4] + "/" + unpackedID[5] + "/" + unpackedID[6] + "/"
            + unpackedID[7] + "/" + unpackedID[8] + "/" + unpackedID[9] + "/" + unpackedID[10] + "/" + unpackedID[11]);
        } else // If it does have a pre-defined slot, set that to be the empty slot which we will insert into
        { // We don't need to worry about putting an alien in an already used spot because loading the scene is the only time the aliens will have a pre-defined slot
            emptySlotIndex = int.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[11]);           
        }

        // Set slot value to a string of the aliens stats, so now the stats of the alien can be accessed anywhere        
        PlayerPrefs.SetString("slot" + emptySlotIndex.ToString(), alienID.ToString());

        // Set the image of the slot to be identical to the aliens
        alienSprite = alienIcons[int.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[1])];
        itemParent.transform.GetChild(emptySlotIndex).GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
        itemParent.transform.GetChild(emptySlotIndex).GetChild(0).GetChild(0).GetComponent<Image>().sprite = alienSprite;

        if (int.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[2]) == 2) // If the alien is pre-defined to be on the floor
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
            GameObject.Find("AlienInfoPanel").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0); // set position to 0
            GameObject.Find("AlienInfoPanel").GetComponent<CreateStatBox>().CreatePanel(int.Parse(PlayerPrefs.GetString("slot"+slot.ToString()))); // Get the alien ID from the slot name
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
