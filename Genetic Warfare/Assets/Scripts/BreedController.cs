using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedController : MonoBehaviour
{
    /*[SerializeField] private GameObject Jaotop;
    [SerializeField] private GameObject Qayrat;
    private AlienBase babyScript;
    private GameObject baby;
    private string babyToMake;

    private void Start()
    {
        Breed(GameObject.Find("Qayrat"), GameObject.Find("Qayrat2"));
    }

    public void Breed (GameObject Alien1, GameObject Alien2)
    {                   
        babyToMake = "Jaotop";
        switch (babyToMake)
        {
            case "Qayrat":
                {
                    baby = Instantiate(Qayrat);
                    babyScript = baby.GetComponent<QayratController>();
                }
                break;
            case "Jaotop":
                {
                    baby = Instantiate(Jaotop);
                    babyScript = baby.GetComponent<JaotopController>();
                }
                break;
            default:
                Debug.LogError("BREEDING ERROR: babyToMake did not match an alien's name");
                break;
        }

        babyScript.SetDefenceMinMax(Mathf.Min(Alien1.GetComponent<AlienBase>().GetDefence(), Alien2.GetComponent<AlienBase>().GetDefence()) * 0.9f, 
            Mathf.Max(Alien1.GetComponent<AlienBase>().GetDefence(), Alien2.GetComponent<AlienBase>().GetDefence()) *1.1f);

        babyScript.SetAttackSpeedMinMax(Mathf.Min(Alien1.GetComponent<AlienBase>().GetAttackSpeed(), Alien2.GetComponent<AlienBase>().GetAttackSpeed()) * 0.9f,
            Mathf.Max(Alien1.GetComponent<AlienBase>().GetAttackSpeed(), Alien2.GetComponent<AlienBase>().GetAttackSpeed()) * 1.1f);

        babyScript.SetMovementSpeedMinMax(Mathf.Min(Alien1.GetComponent<AlienBase>().GetMovementSpeed(), Alien2.GetComponent<AlienBase>().GetMovementSpeed()) * 0.9f,
            Mathf.Max(Alien1.GetComponent<AlienBase>().GetMovementSpeed(), Alien2.GetComponent<AlienBase>().GetMovementSpeed()) * 1.1f);

        babyScript.SetDamageMinMax(Mathf.Min(Alien1.GetComponent<AlienBase>().GetDamage(), Alien2.GetComponent<AlienBase>().GetDamage()) * 0.9f,
            Mathf.Max(Alien1.GetComponent<AlienBase>().GetDamage(), Alien2.GetComponent<AlienBase>().GetDamage()) * 1.1f);

        babyScript.SetHealthMinMax(Mathf.Min(Alien1.GetComponent<AlienBase>().GetHealth(), Alien2.GetComponent<AlienBase>().GetHealth()) * 0.9f,
            Mathf.Max(Alien1.GetComponent<AlienBase>().GetHealth(), Alien2.GetComponent<AlienBase>().GetHealth()) * 1.1f);

        babyScript.SetRangeMinMax(Mathf.Min(Alien1.GetComponent<AlienBase>().GetRange(), Alien2.GetComponent<AlienBase>().GetRange()) * 0.9f,
            Mathf.Max(Alien1.GetComponent<AlienBase>().GetRange(), Alien2.GetComponent<AlienBase>().GetRange()) * 1.1f);     

        babyScript.GenerateBase();
        babyScript.ScoreStats();
    }*/
}
