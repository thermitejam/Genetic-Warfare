using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateStatBox : MonoBehaviour
{
    [SerializeField] private Slider defenceBar, attackSpeedBar, movementSpeedBar, damageBar, healthBar, rangeBar;
    [SerializeField] private Image alienImage;
    [SerializeField] private Sprite[] alienImages;
    [SerializeField] private Button moveBtn, killBtn;
    private int clickedAlienID, alienPosition;
    private GameObject clickedAlien;
    private IDManager idManager;

    private void Start()
    {
        idManager = GameObject.Find("DataManager").GetComponent<IDManager>();
    }

    public void CreatePanel(int alienID)
    {
        // Set the slider values; We divide by 2 as 2 is the cap at which no stat can surpass. This can be altered
        defenceBar.value = idManager.GetDefence(alienID)/2;
        attackSpeedBar.value = idManager.GetAttackSpeed(alienID) / 2;
        movementSpeedBar.value = idManager.GetMovementSpeed(alienID) / 2;
        damageBar.value = idManager.GetDamage(alienID) / 2;
        healthBar.value = idManager.GetHealth(alienID) / 2;
        rangeBar.value = idManager.GetRange(alienID) / 2;
        alienImage.sprite = alienImages[idManager.GetAlienType(alienID)]; // Change the alien icon

        alienPosition = idManager.GetPosition(alienID); // Set the alienPosition variable to the aliens position so we know which button we are using

        switch (alienPosition)
        {
            case 0: 
                moveBtn.GetComponentInChildren<Text>().text = "Not Owned";
                killBtn.interactable = false; // If we don't own the alien we can't kill it
                break;
            case 1:
                moveBtn.GetComponentInChildren<Text>().text = "Inv->Floor";
                killBtn.interactable = true; // If we own the alien and it's in our inventory we can kill it
                break;
            case 2:
                moveBtn.GetComponentInChildren<Text>().text = "Floor->Inv";
                killBtn.interactable = false; // As a precaution you have to move the alien to your inventory if you want to kill it
                break;              
        }

        clickedAlienID = alienID; // Saved so we know which alien we need to move in MoveButtonClicked()
    }

    public void ClosePanel() // When we close just move it off-screen
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, 0);
    }

    public void MoveButtonClicked()
    {
        if (alienPosition == 1) { // If the alien is in the inventory, then when we click the button we want to move it to the floor
            GameObject.Find("DataManager").GetComponent<LoadAndSave>().AddAlienToFloor(clickedAlienID);
            ClosePanel();
        } else if (alienPosition == 2) // If the alien is on the the floor then when we click the button want to remove it from the floor
        {            
            GameObject.Find("DataManager").GetComponent<LoadAndSave>().RemoveFromFloor(clickedAlien);
            ClosePanel();
        }       
    }

    public void FeedAlien(GameObject alien) // Called when player clicks an alien so we know which alien to delete 
    {
        clickedAlien = alien; 
    }
}

