using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stats : MonoBehaviour
{
    [SerializeField]
    private float score;

    [SerializeField]
    protected float shopPrice;



    [SerializeField]
    private float defence;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float health;
    [SerializeField]
    private float range;
    
    public Vector2 defenceMinMax;
    public Vector2 attackSpeedMinMax;
    public Vector2 movementSpeedMinMax;
    public Vector2 damageMinMax;
    public Vector2 healthMinMax;
    public Vector2 rangeMinMax;
    

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
        score = (GetDefence() - defenceMinMax.x) / (defenceMinMax.y - defenceMinMax.x);
        score += (GetAttackSpeed() - attackSpeedMinMax.x) / (attackSpeedMinMax.y - attackSpeedMinMax.x);
        score += (GetMovementSpeed() - movementSpeedMinMax.x) / (movementSpeedMinMax.y - movementSpeedMinMax.x);
        score += (GetDamage() - damageMinMax.x) / (damageMinMax.y - damageMinMax.x);
        score += (GetHealth() - healthMinMax.x) / (healthMinMax.y - healthMinMax.x);
        score += (GetRange() - rangeMinMax.x) / (rangeMinMax.y - rangeMinMax.x);
        shopPrice = Mathf.Round(shopPrice * (1 + score));
        score /= 6;
        

    }

}
