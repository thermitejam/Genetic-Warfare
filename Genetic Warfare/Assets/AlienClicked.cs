using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienClicked : MonoBehaviour
{
    void OnMouseDown() // When the aliens clicked move the panel to the middle of the screen and create a panel using this objects game component
    {
        GameObject.Find("AlienInfoPanel").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        GameObject.Find("AlienInfoPanel").GetComponent<CreateStatBox>().CreatePanel(gameObject.GetComponent<JaotopController>());
    }
}
