using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QayratController: AlienBase
{
    [SerializeField] // ID
    private int alienID;

    private void Start()
    {
        SetDefenceMinMax(10, 50);
        SetAttackSpeedMinMax(0, 2);
        SetMovementSpeedMinMax(0.4f, 5);
        SetDamageMinMax(5, 17);
        SetHealthMinMax(200, 600);
        SetRangeMinMax(0.5f, 2);
        GenerateBase();
        ScoreStats();

        alienID = PlayerPrefs.GetInt("alienID") + 1; // Give the alien a unique ID. It will never not be unique as it is always 1 more than the last
        PlayerPrefs.SetInt("alienID", PlayerPrefs.GetInt("alienID") + 1); // If this fucks up we can use random number from 9999999, and compare it against the id list to make sure its not identical

        // Set alien data using it's ID in a long string
        PlayerPrefs.SetString(alienID.ToString(), GetScore().ToString() + "/" + GetShopPrice().ToString() + "/" + GetDefence().ToString() + "/" + GetAttackSpeed().ToString() + "/"
            + GetMovementSpeed().ToString() + "/" + GetDamage().ToString() + "/" + GetHealth().ToString() + "/" + GetRange().ToString());

        GameObject.Find("DataManager").GetComponent<LoadAndSave>().Add(alienID);
        GameObject.Find("DataManager").GetComponent<LoadAndSave>().Remove(alienID); // Currently removes itsself
    }   
}
