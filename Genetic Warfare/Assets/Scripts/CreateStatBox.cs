using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateStatBox : MonoBehaviour
{
    [SerializeField] private Slider defenceBar, attackBar, movementBar, damageBar, healthBar, rangeBar;
    [SerializeField] private Image alienImage;
    [SerializeField] private Sprite[] alienImages;
    [SerializeField] private Button moveBtn, killBtn;
    private int tempAlienID, alienPosition;
    private GameObject clickedAlien;

    public void CreatePanel(int alienID)
    {
        defenceBar.value = float.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[5])/2; // 5 is the index of the defence, if the string format changes then this must be updated to reflect that
        attackBar.value = float.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[6])/2; // We do /2 as the stats have to have a cap. if they had no cap then there would be no realistic
        movementBar.value = float.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[7])/2; // way of implementing the slider bars. 2 is enough room to grow your enemy 
        damageBar.value = float.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[8])/2; // will have to make sure it doesn't go above 2 in the breeding code
        healthBar.value = float.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[9])/2;
        rangeBar.value = float.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[10])/2;
        alienImage.sprite = alienImages[int.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[1])]; // Change the sprite and color

        alienPosition = int.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[2]);

        switch (alienPosition)
        {
            case 0:
                moveBtn.GetComponentInChildren<Text>().text = "Not Owned";
                killBtn.interactable = false; // If we don't own the alien we can't kill it
                break;
            case 1:
                moveBtn.GetComponentInChildren<Text>().text = "Inv->Floor";
                killBtn.interactable = true;
                break;
            case 2:
                moveBtn.GetComponentInChildren<Text>().text = "Floor->Inv";
                killBtn.interactable = false; // As a precaution you have to move the alien to your inventory if you want to kill it
                break;              
        }
        tempAlienID = alienID; // saved so we know which alien we need to move in MoveButtonClicked()
    }

    public void ClosePanel() // When we close just move it off screen
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, 0);
    }

    public void MoveButtonClicked()
    {
        if (alienPosition == 1) {
            GameObject.Find("DataManager").GetComponent<LoadAndSave>().AddAlienToFloor(tempAlienID);
            ClosePanel();
        } else if (alienPosition == 2)
        {            
            GameObject.Find("DataManager").GetComponent<LoadAndSave>().RemoveFromFloor(clickedAlien);
            ClosePanel();
        }       
    }

    public void FeedAlien(GameObject alien) // this is so we can delete the alien based on what button is pressed
    {
        clickedAlien = alien; 
    }
}

