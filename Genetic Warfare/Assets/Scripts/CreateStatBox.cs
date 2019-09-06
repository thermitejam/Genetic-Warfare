using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateStatBox : MonoBehaviour
{
    [SerializeField] private Slider defenceBar, attackBar, movementBar, damageBar, healthBar, rangeBar;
    [SerializeField] private Image alienImg;

    public void CreatePanel(AlienBase alien)
    {
        defenceBar.value = alien.defenceScore; // Set the value of each slider based on the stat score
        attackBar.value = alien.attackSpeedScore;
        movementBar.value = alien.movementSpeedScore;
        damageBar.value = alien.damageScore;
        healthBar.value = alien.healthScore;
        rangeBar.value = alien.rangeScore;

        alienImg.sprite = alien.gameObject.GetComponent<SpriteRenderer>().sprite; // Change the sprite and color
        alienImg.color = alien.gameObject.GetComponent<SpriteRenderer>().color; // Eventually we won't use color since the sprite will be the only thing we need
    }

    public void ClosePanel() // When we close just move it off screen
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, 0);
    }
}

