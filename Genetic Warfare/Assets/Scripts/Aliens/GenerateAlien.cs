using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAlien : MonoBehaviour
{
    // Start is called before the first frame update

    

    public void BreedAlien(alienEntry alien1,alienEntry alien2)
    {
        
    }

    public void createAlien(AlienSpecies speciesEnum)
    {
        createAlien(AlienTemplateLoader.ins.loadAlien(speciesEnum));
    }
    public void createAlien(AlienTemplate species)
    {
        alienEntry newAlien = new alienEntry();
        
        newAlien.species = species.speciesEnum;
        newAlien.Defence = Mathf.Round(100 * Random.Range(species.defenceMinMax.x, species.defenceMinMax.y)) / 100;
        newAlien.Health = Mathf.Round(100 * Random.Range(species.healthMinMax.x,species.healthMinMax.y)) / 100;
        newAlien.Range = Mathf.Round(100 * Random.Range(species.rangeMinMax.x,species.rangeMinMax.y)) / 100;
        newAlien.AttackSpeed = Mathf.Round(100 * Random.Range(species.attackSpeedMinMax.x, species.attackSpeedMinMax.y)) / 100;
        newAlien.MovementSpeed = Mathf.Round(100 * Random.Range(species.movementSpeedMinMax.x,species.movementSpeedMinMax.y)) / 100;
        newAlien.Damage = Mathf.Round(100 * Random.Range(species.damageMinMax.x,species.damageMinMax.y)) / 100;
        newAlien.score = ScoreAlien(newAlien);
        newAlien.ShopPrice = CalculateShopPrice(newAlien);
        newAlien.id = GenerateNewID(newAlien);
        Inventory.ins.addAlienToInventory(newAlien);
        
    }
    public float ScoreAlien(alienEntry alien)
    {
        
        AlienTemplate template = new AlienTemplate();
        template = AlienTemplateLoader.ins.loadAlien(alien.species);
        
        float score = (alien.Defence - template.defenceMinMax.x) / (template.defenceMinMax.y - template.defenceMinMax.x);
        score += (alien.Health = template.healthMinMax.x) / (template.healthMinMax.y - template.healthMinMax.x);
        score += (alien.Range - template.healthMinMax.x) / (template.healthMinMax.y - template.healthMinMax.x);
        score += (alien.MovementSpeed - template.movementSpeedMinMax.x) / (template.movementSpeedMinMax.y - template.movementSpeedMinMax.x);
        score += (alien.Damage - template.damageMinMax.x) / (template.damageMinMax.y - template.damageMinMax.x);

        alien.score = score;

        return score / 6;
    }
    public int CalculateShopPrice(alienEntry alien)
    {
        int shopPrice = Mathf.RoundToInt((alien.score + 1) * AlienTemplateLoader.ins.loadAlien(alien.species).BaseShopPrice);

        alien.ShopPrice = shopPrice;
        return shopPrice;
    }

    public string GenerateNewID(alienEntry alien)
    {
        string newAlienID = Mathf.Round(Random.Range(0, 10000000)).ToString();
        while (Inventory.ins.doesAlienExsist(newAlienID))
        {
            newAlienID = Mathf.Round(Random.Range(0, 10000000)).ToString();
        }
        alien.id = newAlienID;
        return newAlienID;

    }



}
