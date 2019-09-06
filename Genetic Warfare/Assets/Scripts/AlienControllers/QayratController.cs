using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QayratController: AlienBase
{
    [SerializeField] // ID
    private int alienID;

    private void Start() // for now it's fine that it loads everything on start, in future it should be a Create() function so that whenever it's made it doesn't regenerate its stats
    {
        SetDefenceMinMax(0.1f, 0.5f);
        SetAttackSpeedMinMax(0, 0.2f);
        SetMovementSpeedMinMax(0.4f, 0.6f);
        SetDamageMinMax(0.1f, 0.5f);
        SetHealthMinMax(0.2f, 0.6f);
        SetRangeMinMax(0.2f, 0.7f);
        GenerateBase();
        ScoreStats();

        alienID = PlayerPrefs.GetInt("alienID") + 1; // Give the alien a unique ID. It will never not be unique as it is always 1 more than the last
        PlayerPrefs.SetInt("alienID", PlayerPrefs.GetInt("alienID") + 1); // If this fucks up we can use random number from 9999999, and compare it against the id list to make sure its not identical

        // Set alien data using it's ID in a long string
        // First number is it's alien type,, 0 = JAOTOP, 1 = QAYRAT, will update as more are added
        // Second number is it's position, 0 = nowhere, 1 = inventory, 2 = placed on home floor.. by default it is nowhere, which accounts for shop and opponent aliens, when you buy, position = 1
        // whenever it's position is updated you just update the string and the other scripts will take care of it
        PlayerPrefs.SetString(alienID.ToString(), 1.ToString() + "/" + 0.ToString() + "/" + GetScore().ToString() + "/" + GetShopPrice().ToString() + "/" + GetDefence().ToString() + "/" + GetAttackSpeed().ToString() + "/"
            + GetMovementSpeed().ToString() + "/" + GetDamage().ToString() + "/" + GetHealth().ToString() + "/" + GetRange().ToString());

        GameObject.Find("DataManager").GetComponent<LoadAndSave>().Add(alienID);
        GameObject.Find("DataManager").GetComponent<LoadAndSave>().Remove(alienID); // Currently removes itsself
    }

    void OnMouseDown() // When the aliens clicked move the panel to the middle of the screen and create a panel using the alien ID
    {
        GameObject.Find("AlienInfoPanel").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        GameObject.Find("AlienInfoPanel").GetComponent<CreateStatBox>().CreatePanel(alienID);
    }
}
