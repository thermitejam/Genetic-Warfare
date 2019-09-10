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

       /* ID Data Key:
       0: Alien ID
       1: Alien Type (0=Jaotop, 1=Qayrat, update when more are added)
       2: Position (0 = nowhere(just spawned), 1 = inventory, 2 = floor (anything on the floor is also in the inventory)
       3: Score
       4: Shop Price
       5: Defence
       6: Attack Speed
       7: Movement Speed
       8: Damage
       9: Health
       10: Range
       11: Inventory Slot (-1 by default because it has no slot)
        */
        PlayerPrefs.SetString(alienID.ToString(), alienID.ToString() + "/" + alienType.ToString() + "/" + 0.ToString() + "/" + score.ToString() + "/" + shopPrice.ToString() + "/" + defence.ToString() + "/" 
            + attackSpeed.ToString() + "/" + movementSpeed.ToString() + "/" + damage.ToString() + "/" + health.ToString() + "/" + range.ToString() + "/" + "-1");

        GameObject.Find("DataManager").GetComponent<LoadAndSave>().AddToActiveAliens(alienID); // Then add it to the list of active ID's
    }

    void OnMouseDown() // When clicked
    {
        GameObject.Find("AlienInfoPanel").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0); // Create a panel with the alien ID
        GameObject.Find("AlienInfoPanel").GetComponent<CreateStatBox>().CreatePanel(alienID);

        // If it's on the floor then we can feed it to our stat box, if it wasn't on the floor then we might not want to feed the game object
        if (GameObject.Find("DataManager").GetComponent<IDManager>().GetPosition(alienID) == 2) 
        {
            GameObject.Find("AlienInfoPanel").GetComponent<CreateStatBox>().FeedAlien(gameObject); // Give the stat box a reference incase the player wants to move(destroy) it
        }        
    }
}
