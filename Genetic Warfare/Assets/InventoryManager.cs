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


    private void Start()
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
        // search for empty slot
        for (int i = 0; i < invSlots.Length; i++)
        {
            if (invSlots[i].IsEmpty())
            {
                emptySlotIndex = i;
                break;
            }
        }

        // set slot value to a string of the aliens stats
        Debug.Log(PlayerPrefs.GetString(alienID.ToString()));
        PlayerPrefs.SetString("slot" + emptySlotIndex.ToString(), alienID.ToString());

        // set the image of the slot to be identical to the aliens
        alienSprite = alienIcons[int.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[0])];
        itemParent.transform.GetChild(emptySlotIndex).GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
        itemParent.transform.GetChild(emptySlotIndex).GetChild(0).GetChild(0).GetComponent<Image>().sprite = alienSprite;

        // make sure inventory slot is not empty
        invSlots[emptySlotIndex].MakeNotEmpty();

        // destroy original alien?
    }

    public void SlotClicked(int slot)
    {
        if (!invSlots[slot].IsEmpty())
        {
            GameObject.Find("AlienInfoPanel").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0); // set position to 0
            GameObject.Find("AlienInfoPanel").GetComponent<CreateStatBox>().CreatePanel(int.Parse(PlayerPrefs.GetString("slot"+slot.ToString()))); // Get the alien ID from the slot name
        }
    }
}
