using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedController : MonoBehaviour
{
    [SerializeField] private GameObject Qayrat;
    [SerializeField] private GameObject Jaotop;
    private Stats babyScript;
    private GameObject baby;
    private string babyToMake;

    private void Start()
    {
        Breed(GameObject.Find("Qayrat"), GameObject.Find("Qayrat2"));
    }

    public void Breed (GameObject Alien1, GameObject Alien2)
    {                   
        babyToMake = "Jaop";
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

        babyScript.SetDefenceMinMax(Mathf.Min(Alien1.GetComponent<Stats>().GetDefence(), Alien2.GetComponent<Stats>().GetDefence()) * 0.9f, 
            Mathf.Max(Alien1.GetComponent<Stats>().GetDefence(), Alien2.GetComponent<Stats>().GetDefence()) *1.1f);

        babyScript.SetAttackSpeedMinMax(Mathf.Min(Alien1.GetComponent<Stats>().GetAttackSpeed(), Alien2.GetComponent<Stats>().GetAttackSpeed()) * 0.9f,
            Mathf.Max(Alien1.GetComponent<Stats>().GetAttackSpeed(), Alien2.GetComponent<Stats>().GetAttackSpeed()) * 1.1f);

        babyScript.SetMovementSpeedMinMax(Mathf.Min(Alien1.GetComponent<Stats>().GetMovementSpeed(), Alien2.GetComponent<Stats>().GetMovementSpeed()) * 0.9f,
            Mathf.Max(Alien1.GetComponent<Stats>().GetMovementSpeed(), Alien2.GetComponent<Stats>().GetMovementSpeed()) * 1.1f);

        babyScript.SetDamageMinMax(Mathf.Min(Alien1.GetComponent<Stats>().GetDamage(), Alien2.GetComponent<Stats>().GetDamage()) * 0.9f,
            Mathf.Max(Alien1.GetComponent<Stats>().GetDamage(), Alien2.GetComponent<Stats>().GetDamage()) * 1.1f);

        babyScript.SetHealthMinMax(Mathf.Min(Alien1.GetComponent<Stats>().GetHealth(), Alien2.GetComponent<Stats>().GetHealth()) * 0.9f,
            Mathf.Max(Alien1.GetComponent<Stats>().GetHealth(), Alien2.GetComponent<Stats>().GetHealth()) * 1.1f);

        babyScript.SetRangeMinMax(Mathf.Min(Alien1.GetComponent<Stats>().GetRange(), Alien2.GetComponent<Stats>().GetRange()) * 0.9f,
            Mathf.Max(Alien1.GetComponent<Stats>().GetRange(), Alien2.GetComponent<Stats>().GetRange()) * 1.1f);     

        babyScript.GenerateBase();
        babyScript.ScoreStats();
    }
}
