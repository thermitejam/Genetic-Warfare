using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBase : MonoBehaviour
{
    [SerializeField] // STATS
    private float score, shopPrice = 250, defence, attackSpeed, movementSpeed, damage, health, range;

    [System.NonSerialized] // Seperate scores for each score, that way we can use the values in the stat box
    public float defenceScore, attackSpeedScore, movementSpeedScore, damageScore, healthScore, rangeScore;

    [SerializeField] // CREATION PARAMETERS these are default values, eventually you wont need default values because youll create it in the enemy Start() this is just for testing
    public Vector2 defenceMinMax = new Vector2(10,50), attackSpeedMinMax = new Vector2(0, 2), movementSpeedMinMax = new Vector2(0.4f, 5), damageMinMax = new Vector2(5, 17), 
        healthMinMax = new Vector2(200, 600), rangeMinMax = new Vector2(0.5f, 2);

    // GET STAT FUNCTIONS
    public float GetDefence()
    {
        return defence;
    }
    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
    public float GetMovementSpeed()
    {
        return movementSpeed;
    }
    public float GetDamage()
    {
        return damage;
    }
    public float GetHealth()
    {
        return health;
    }
    public float GetRange()
    {
        return range;
    }
    public float GetScore()
    {
        return score;
    }
    public float GetShopPrice ()
    {
        return shopPrice;
    }
    
    // SET STAT FUNCTIONS
    public void SetDefence(float Defence)
    {
        defence = Defence;
    }
    public void SetAttackSpeed(float AttackSpeed)
    {
        attackSpeed = AttackSpeed;
    }
    public void SetMovementSpeed(float MovementSpeed)
    {
        movementSpeed = MovementSpeed;
    }
    public void SetDamage(float Damage)
    {
        damage = Damage;
    }
    public void SetHealth(float Health)
    {
        health = Health;
    }
    public void SetRange(float Range)
    {
        range = Range;
    }

    // SET CREATION PARAMETERS FUNCTIONS
    public void SetDefenceMinMax(float x,float y)
    {
        defenceMinMax = new Vector2(x, y);
    }
    public void SetAttackSpeedMinMax(float x, float y)
    {
        attackSpeedMinMax = new Vector2(x, y);
    }
    public void SetMovementSpeedMinMax(float x, float y)
    {
        movementSpeedMinMax = new Vector2(x, y);
    }
    public void SetDamageMinMax(float x, float y)
    {
        damageMinMax = new Vector2(x, y);
    }
    public void SetHealthMinMax(float x, float y)
    {
        healthMinMax = new Vector2(x, y);
    }
    public void SetRangeMinMax(float x, float y)
    {
        rangeMinMax = new Vector2(x, y);
    }

    public void GenerateBase()
    {       
        defence = Mathf.Round(Random.Range(defenceMinMax.x, defenceMinMax.y) * 100) / 100;
        attackSpeed = Mathf.Round(Random.Range(attackSpeedMinMax.x, attackSpeedMinMax.y) * 100) / 100;
        movementSpeed = Mathf.Round(Random.Range(movementSpeedMinMax.x, movementSpeedMinMax.y) * 100) / 100;
        damage = Mathf.Round(Random.Range(damageMinMax.x, damageMinMax.y) * 100) / 100;
        health = Mathf.Round(Random.Range(healthMinMax.x, healthMinMax.y) * 100) / 100;  
        range = Mathf.Round(Random.Range(rangeMinMax.x, rangeMinMax.y) * 100)/100;        
    }

    public void ScoreStats()
    {
        defenceScore = (GetDefence() - defenceMinMax.x) / (defenceMinMax.y - defenceMinMax.x);
        attackSpeedScore = (GetAttackSpeed() - attackSpeedMinMax.x) / (attackSpeedMinMax.y - attackSpeedMinMax.x);
        movementSpeedScore = (GetMovementSpeed() - movementSpeedMinMax.x) / (movementSpeedMinMax.y - movementSpeedMinMax.x);
        damageScore = (GetDamage() - damageMinMax.x) / (damageMinMax.y - damageMinMax.x);
        healthScore = (GetHealth() - healthMinMax.x) / (healthMinMax.y - healthMinMax.x);
        rangeScore = (GetRange() - rangeMinMax.x) / (rangeMinMax.y - rangeMinMax.x);
        score = defenceScore + attackSpeedScore + movementSpeedScore + damageScore + healthScore + rangeScore;
        shopPrice = Mathf.Round(250 * (1 + score)); // changed shopPrice to 250, because shopPrice is 0 by default
        score /= 6;
    }
}
