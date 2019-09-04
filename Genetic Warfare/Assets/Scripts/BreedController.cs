using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedController : MonoBehaviour
{
    // here you would instantiate all the possible aliens
    [SerializeField] private GameObject QayRat;


    private GameObject baby;

    private string babyToMake;
    private void Start()
    {
        Breed(GameObject.Find("Qayrat"), GameObject.Find("Qayrat2"));
    }

    public void Breed (GameObject Alien1, GameObject Alien2)
    {
        // here you would decide based on alien1.name and alien2.name which alien the baby would be
        // then do a switch statement using the alien name as the parameter and have a copy of the following two lines in each case to determine the alien

        baby = Instantiate(QayRat);
        QayratController babyScript = baby.GetComponent<QayratController>();
      
        Debug.Log("Alien1 defence: " + Alien1.GetComponent<Stats>().GetDefence());
        Debug.Log("Alien2 defence: " + Alien2.GetComponent<Stats>().GetDefence());


        // DEFENCE
        if (Alien1.GetComponent<Stats>().GetDefence() < Alien2.GetComponent<Stats>().GetDefence())
        {           
           babyScript.SetDefenceMinMax(Alien1.GetComponent<Stats>().GetDefence()*0.9f, Alien2.GetComponent<Stats>().GetDefence()*1.1f);
        } else
        {
            babyScript.SetDefenceMinMax(Alien2.GetComponent<Stats>().GetDefence() * 0.9f, Alien1.GetComponent<Stats>().GetDefence() * 1.1f);
        }


        // ATTACK SPEED
        if (Alien1.GetComponent<Stats>().GetAttackSpeed() < Alien2.GetComponent<Stats>().GetAttackSpeed())
        {
            babyScript.SetAttackSpeedMinMax(Alien1.GetComponent<Stats>().GetAttackSpeed() * 0.9f, Alien2.GetComponent<Stats>().GetAttackSpeed() * 1.1f);
        }
        else
        {
            babyScript.SetAttackSpeedMinMax(Alien2.GetComponent<Stats>().GetAttackSpeed() * 0.9f, Alien1.GetComponent<Stats>().GetAttackSpeed() * 1.1f);
        }


        // MOVEMENT SPEED
        if (Alien1.GetComponent<Stats>().GetMovementSpeed() < Alien2.GetComponent<Stats>().GetMovementSpeed())
        {
            babyScript.SetMovementSpeedMinMax(Alien1.GetComponent<Stats>().GetMovementSpeed() * 0.9f, Alien2.GetComponent<Stats>().GetMovementSpeed() * 1.1f);
        }
        else
        {
            babyScript.SetMovementSpeedMinMax(Alien2.GetComponent<Stats>().GetMovementSpeed() * 0.9f, Alien1.GetComponent<Stats>().GetMovementSpeed() * 1.1f);
        }


        // DAMAGE
        if (Alien1.GetComponent<Stats>().GetDamage() < Alien2.GetComponent<Stats>().GetDamage())
        {
            babyScript.SetDamageMinMax(Alien1.GetComponent<Stats>().GetDamage() * 0.9f, Alien2.GetComponent<Stats>().GetDamage() * 1.1f);
        }
        else
        {
            babyScript.SetDamageMinMax(Alien2.GetComponent<Stats>().GetDamage() * 0.9f, Alien1.GetComponent<Stats>().GetDamage() * 1.1f);
        }


        // HEALTH
        if (Alien1.GetComponent<Stats>().GetHealth() < Alien2.GetComponent<Stats>().GetHealth())
        {
            babyScript.SetHealthMinMax(Alien1.GetComponent<Stats>().GetHealth() * 0.9f, Alien2.GetComponent<Stats>().GetHealth() * 1.1f);
        }
        else
        {
            babyScript.SetHealthMinMax(Alien2.GetComponent<Stats>().GetHealth() * 0.9f, Alien1.GetComponent<Stats>().GetHealth() * 1.1f);
        }


        // RANGE
        if (Alien1.GetComponent<Stats>().GetRange() < Alien2.GetComponent<Stats>().GetRange())
        {
            babyScript.SetRangeMinMax(Alien1.GetComponent<Stats>().GetRange() * 0.9f, Alien2.GetComponent<Stats>().GetRange() * 1.1f);
        }
        else
        {
            babyScript.SetRangeMinMax(Alien2.GetComponent<Stats>().GetRange() * 0.9f, Alien1.GetComponent<Stats>().GetRange() * 1.1f);
        }

        babyScript.GenerateBase();
        babyScript.ScoreStats();
        Debug.Log("Final defence: " + babyScript.GetDefence());
    }
}
