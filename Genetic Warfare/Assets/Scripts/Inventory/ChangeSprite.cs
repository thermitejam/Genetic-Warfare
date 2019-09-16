using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    Image spriterenderer;
    Sprite Sprite1;
    [SerializeField] Sprite Sprite2;
    // Start is called before the first frame update
    void Start()
    {
        spriterenderer = gameObject.GetComponent<Image>();
        Sprite1 = spriterenderer.sprite;
    }

    // Update is called once per frame
    public void change()
    {
        if (spriterenderer.sprite == Sprite1)
        {
            spriterenderer.sprite = Sprite2;
        }
        else
        {
            spriterenderer.sprite = Sprite1;
        }
        
    }
}
