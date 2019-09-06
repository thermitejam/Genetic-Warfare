using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateStatBox : MonoBehaviour
{
    [SerializeField] private Slider defenceBar, attackBar, movementBar, damageBar, healthBar, rangeBar;
    [SerializeField] private Image alienImage;
    [SerializeField] private Sprite[] alienImages;

    public void CreatePanel(int alienID)
    {
        defenceBar.value = float.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[4])/2; // 4 is the index of the defence, if the string format changes then this must be updated to reflect that
        attackBar.value = float.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[5])/2; // We do /2 as the stats have to have a cap. if they had no cap then there would be no realistic
        movementBar.value = float.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[6])/2; // way of implementing the slider bars. 2 is enough room to grow your enemy 
        damageBar.value = float.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[7])/2;
        healthBar.value = float.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[8])/2;
        rangeBar.value = float.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[9])/2;

        alienImage.sprite = alienImages[int.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[0])]; // Change the sprite and color
    }

    public void ClosePanel() // When we close just move it off screen
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, 0);
    }
}

