using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreedController : MonoBehaviour
{
    [SerializeField] private Sprite[] alienIcons;
    [SerializeField] private GameObject Alien1Slot, Alien2Slot, Alien3Slot;
    private int id1, id2, id3;
    private bool slot1Occupied, slot2Occupied;

    private IDManager idManager;
    private GameObject itemParent;
    private CreateStatBox statPanel;
    private InventoryManager invent;

    [SerializeField] private GameObject[] panelArray;
    public int clickedSlotSave;
    private int IdToDelete;

    [SerializeField] private GameObject[] alienArray;
    private string babyToMake;
    private GameObject babyInstance;
    private int babyType;

    private void Start()
    {
        idManager = GameObject.Find("DataManager").GetComponent<IDManager>();
        itemParent = GameObject.Find("ItemsParent");
        statPanel = GameObject.Find("AlienInfoPanel").GetComponent<CreateStatBox>();
        invent = GameObject.Find("Inventory").GetComponent<InventoryManager>();

        GameObject.Find("Inventory").GetComponent<OpenCloseInv>().OpenClose(); // Open inventory
        
    }

    public void AddToBreedingSlot(int alienID)
    {
        if (!slot1Occupied || !slot2Occupied)
        {
            idManager.SetPosition(alienID, 3);
            itemParent.transform.GetChild(idManager.GetInventorySlot(alienID)).GetChild(0).GetComponent<Button>().interactable = false; // Make the aliens inventory slot un-interactable            
            if (!slot1Occupied)
            {
                id1 = alienID; // Set the first id slot variable to the alien ID
                slot1Occupied = true; // Set the first slot variable to be occupied
                Alien1Slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true; // Enable the image
                Alien1Slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = alienIcons[idManager.GetAlienType(alienID)]; // Set the sprite based on the alien type
            }
            else if (!slot2Occupied)
            {
                id2 = alienID;
                slot2Occupied = true;
                Alien2Slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                Alien2Slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = alienIcons[idManager.GetAlienType(alienID)];
            }
            statPanel.ClosePanel();
        }       
    }

    public void RemoveFromBreedingSlot(int alienID)
    {
        if (id1 == alienID || id2 == alienID)
        {
            idManager.SetPosition(alienID, 1);
            itemParent.transform.GetChild(idManager.GetInventorySlot(alienID)).GetChild(0).GetComponent<Button>().interactable = true;
            if (id1 == alienID)
            {
                id1 = 0;
                slot1Occupied = false;
                Alien1Slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false; // Enable the image            
            }
            else if (id2 == alienID)
            {
                id2 = 0;
                slot2Occupied = false;
                Alien2Slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false; // Enable the image
            }
            statPanel.ClosePanel();
        }

        if (id3 == alienID)
        {
            idManager.SetPosition(alienID, 1);
            GameObject.Find("Alien3Panel").GetComponent<Animator>().SetBool("babyFade", false);
            itemParent.transform.GetChild(idManager.GetInventorySlot(id3)).GetChild(0).GetComponent<Button>().interactable = true;
            statPanel.ClosePanel();
        }
    }

    public void CreatePanelWhenClicked(int clickedSlot)
    {
        statPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        clickedSlotSave = clickedSlot; // So we can use the clicked slot when deleting below

        if (clickedSlot == 1) {
           statPanel.CreatePanel(id1);
        }
        else if (clickedSlot == 2)
        {
            statPanel.CreatePanel(id2);
        } else if (clickedSlot == 3)
        {
            statPanel.CreatePanel(id3);
        }
    }

    public void KillButtonClicked()
    {
        if (clickedSlotSave == 1)
        {
            slot1Occupied = false;
            IdToDelete = id1;
        }
        else if (clickedSlotSave == 2)
        {
            slot1Occupied = false;
            IdToDelete = id2;
        } else if (clickedSlotSave == 3)
        {
            IdToDelete = id3;
            GameObject.Find("Alien3Panel").GetComponent<Animator>().SetBool("babyFade", false);
        }

         // Handles removing from inventory
        itemParent.transform.GetChild(idManager.GetInventorySlot(IdToDelete)).GetChild(0).GetChild(0).GetComponent<Image>().enabled = false; // Use the slot integer to wipe the slot clear
        itemParent.transform.GetChild(idManager.GetInventorySlot(IdToDelete)).GetChild(0).GetChild(0).GetComponent<Image>().sprite = null;
        itemParent.transform.GetChild(idManager.GetInventorySlot(IdToDelete)).GetChild(0).GetComponent<Button>().interactable = true;
        invent.invSlots[idManager.GetInventorySlot(IdToDelete)].MakeEmpty(); // Make the slot unable to be clicked
            
        statPanel.ClosePanel(); // Close the info panel        
        GameObject.Find("DataManager").GetComponent<LoadAndSave>().DeleteAlien(IdToDelete); // Then we send it to LoadAndSave to be wiped from the data structures

        clickedSlotSave = 0;
    }

    public void Breed()
    {
        statPanel.ClosePanel();
        GameObject.Find("Inventory").GetComponent<OpenCloseInv>().OpenClose();

        // Here would be all the logic for which parents make which babies
        babyToMake = "Qayrat";

        switch(babyToMake)
        {
            case "Jaotop":
                babyInstance = Instantiate(alienArray[0]);
                babyType = 0;
                break;
            case "Qayrat":
                babyInstance = Instantiate(alienArray[1]);
                babyType = 1;
                break;
        }

        // set the min and max
        babyInstance.GetComponent<AlienBase>().SetDefenceMinMax(Mathf.Min(idManager.GetDefence(id1), idManager.GetDefence(id2)) * 0.9f,
            Mathf.Max(idManager.GetDefence(id1), idManager.GetDefence(id2)) * 1.1f);

        babyInstance.GetComponent<AlienBase>().SetAttackSpeedMinMax(Mathf.Min(idManager.GetAttackSpeed(id1), idManager.GetAttackSpeed(id2)) * 0.9f,
            Mathf.Max(idManager.GetAttackSpeed(id1), idManager.GetAttackSpeed(id2)) * 1.1f);

        babyInstance.GetComponent<AlienBase>().SetMovementSpeedMinMax(Mathf.Min(idManager.GetMovementSpeed(id1), idManager.GetMovementSpeed(id2)) * 0.9f,
            Mathf.Max(idManager.GetMovementSpeed(id1), idManager.GetMovementSpeed(id2)) * 1.1f);

        babyInstance.GetComponent<AlienBase>().SetDamageMinMax(Mathf.Min(idManager.GetDamage(id1), idManager.GetDamage(id2)) * 0.9f,
            Mathf.Max(idManager.GetDamage(id1), idManager.GetDamage(id2)) * 1.1f);

        babyInstance.GetComponent<AlienBase>().SetHealthMinMax(Mathf.Min(idManager.GetHealth(id1), idManager.GetHealth(id2)) * 0.9f,
            Mathf.Max(idManager.GetHealth(id1), idManager.GetHealth(id2)) * 1.1f);

        babyInstance.GetComponent<AlienBase>().SetRangeMinMax(Mathf.Min(idManager.GetRange(id1), idManager.GetRange(id2)) * 0.9f,
            Mathf.Max(idManager.GetRange(id1), idManager.GetRange(id2)) * 1.1f);

        babyInstance.GetComponent<AlienBase>().GenerateBase();
        babyInstance.GetComponent<AlienBase>().GenerateScore();
        babyInstance.GetComponent<AlienBase>().GenerateID(babyType,1);
        id3 = babyInstance.GetComponent<AlienBase>().GetID();
        invent.AddToInventory(id3);
        Destroy(babyInstance);

        GameObject.Find("Alien3Panel").GetComponent<Animator>().SetBool("babyFade", true);

        idManager.SetPosition(id3, 3);
        itemParent.transform.GetChild(idManager.GetInventorySlot(id3)).GetChild(0).GetComponent<Button>().interactable = false;
        Alien3Slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true; // Enable the image
        Alien3Slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = alienIcons[idManager.GetAlienType(id3)]; // Set the sprite based on the alien type

        RemoveFromBreedingSlot(id1);
        RemoveFromBreedingSlot(id2);
    }

}
