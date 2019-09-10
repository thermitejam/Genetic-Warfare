using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBase : MonoBehaviour
{
    [SerializeField] // Id
    private int alienID, alienType, alienPosition, inventorySlot;

    [SerializeField] // Stats
    private float score, shopPrice = 250, defence, attackSpeed, movementSpeed, damage, health, range;
  
    [SerializeField] // Creation parameters
    public Vector2 defenceMinMax, attackSpeedMinMax, movementSpeedMinMax, damageMinMax, healthMinMax, rangeMinMax;

    [System.NonSerialized] // Seperate scores for each score, that way we can use the values in the stat box
    public float defenceScore, attackSpeedScore, movementSpeedScore, damageScore, healthScore, rangeScore;

    private string[] unpackedID;

    // Set/Get Alien ID
    public int GetID() { return alienID; }
    public void SetID(int id) { alienID = id; }

    // Set creation parameter functions
    public void SetDefenceMinMax(float x,float y) { defenceMinMax = new Vector2(x, y); }
    public void SetAttackSpeedMinMax(float x, float y) { attackSpeedMinMax = new Vector2(x, y); }
    public void SetMovementSpeedMinMax(float x, float y) { movementSpeedMinMax = new Vector2(x, y); }
    public void SetDamageMinMax(float x, float y) { damageMinMax = new Vector2(x, y); }
    public void SetHealthMinMax(float x, float y) { healthMinMax = new Vector2(x, y); }
    public void SetRangeMinMax(float x, float y) { rangeMinMax = new Vector2(x, y); }

    public void GenerateBase()
    {               
        defence = Mathf.Round(Random.Range(defenceMinMax.x, defenceMinMax.y) * 100) / 100;
        attackSpeed = Mathf.Round(Random.Range(attackSpeedMinMax.x, attackSpeedMinMax.y) * 100) / 100;
        movementSpeed= Mathf.Round(Random.Range(movementSpeedMinMax.x, movementSpeedMinMax.y) * 100) / 100;
        damage = Mathf.Round(Random.Range(damageMinMax.x, damageMinMax.y) * 100) / 100;
        health =  Mathf.Round(Random.Range(healthMinMax.x, healthMinMax.y) * 100) / 100;
        range = Mathf.Round(Random.Range(rangeMinMax.x, rangeMinMax.y) * 100)/100;        
    }

    public void GenerateScore()
    {
        defenceScore = (defence - defenceMinMax.x) / (defenceMinMax.y - defenceMinMax.x);
        attackSpeedScore = (attackSpeed - attackSpeedMinMax.x) / (attackSpeedMinMax.y - attackSpeedMinMax.x);
        movementSpeedScore = (movementSpeed - movementSpeedMinMax.x) / (movementSpeedMinMax.y - movementSpeedMinMax.x);
        damageScore = (damage - damageMinMax.x) / (damageMinMax.y - damageMinMax.x);
        healthScore = (health - healthMinMax.x) / (healthMinMax.y - healthMinMax.x);
        rangeScore = (range - rangeMinMax.x) / (rangeMinMax.y - rangeMinMax.x);
        score = defenceScore + attackSpeedScore + movementSpeedScore + damageScore + healthScore + rangeScore;
        shopPrice = Mathf.Round(250 * (1 + score));
        score /= 6;
        inventorySlot = -1;
    }

    public void GenerateID(int alienType, int position)
    {
        alienID = PlayerPrefs.GetInt("alienIDrandom") + 1; // Assign unique ID
        PlayerPrefs.SetInt("alienIDrandom", PlayerPrefs.GetInt("alienIDrandom") + 1); // Increase it by 1 for next time

        // An alien ID holds all the data about it. With the ID you can switch scenes, close the game etc. and maintain the data
        // First number is it's ID
        // Second number is it's alien type,, 0 = JAOTOP, 1 = QAYRAT, update as more are added
        // Third number is it's position, 0 = nowhere (i.e. in shop, fighting opponent), 1 = inventory, 2 = placed on home floor
        // Whenever any of the data changes you just update the ID string (by using playerprefs.setstring(id,...) so it can be used elsewhere
        // last number is the inventory slot, initially it is -1 so we know it's not in the inventory
        PlayerPrefs.SetString(alienID.ToString(), alienID.ToString() + "/" + alienType.ToString() + "/" + 0.ToString() + "/" + score.ToString() + "/" + shopPrice.ToString() + "/" + defence.ToString() + "/" 
            + attackSpeed.ToString() + "/" + movementSpeed.ToString() + "/" + damage.ToString() + "/" + health.ToString() + "/" + range.ToString() + "/" + "-1");

        GameObject.Find("DataManager").GetComponent<LoadAndSave>().Add(alienID); // Then add it to the list of active ID's
    }

     public void UpdateID(int valueIndex, int thingToAdd) // Swaps one thing for another in our ID string
    {
        unpackedID = PlayerPrefs.GetString(alienID.ToString()).Split('/');
        unpackedID[valueIndex] = thingToAdd.ToString();
        PlayerPrefs.SetString(alienID.ToString(), unpackedID[0] + "/" + unpackedID[1] + "/" + unpackedID[2] + "/" + unpackedID[3] + "/" + unpackedID[4] + "/" + unpackedID[5] + "/" + unpackedID[6] + "/"
        + unpackedID[7] + "/" + unpackedID[8] + "/" + unpackedID[9] + "/" + unpackedID[10] + "/" + unpackedID[11]);
    }

    void OnMouseDown() // When the aliens clicked move the panel to the middle of the screen and create a panel using the alien ID
    {
        GameObject.Find("AlienInfoPanel").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        GameObject.Find("AlienInfoPanel").GetComponent<CreateStatBox>().CreatePanel(alienID);
        if(int.Parse(PlayerPrefs.GetString(alienID.ToString()).Split('/')[2]) == 2) // if it's on the floor then we can go ahead and feed it to our stat box
        {
            GameObject.Find("AlienInfoPanel").GetComponent<CreateStatBox>().FeedAlien(gameObject); // give the stat box a reference incase the player wants to move(destroy) it
        }        
    }
}
