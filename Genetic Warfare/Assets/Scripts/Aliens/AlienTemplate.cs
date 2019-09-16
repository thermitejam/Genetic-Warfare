using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlienName", menuName = "GeneticWarfare/AlienType", order = 1)]
public class AlienTemplate : ScriptableObject
{
    public int BaseShopPrice;
    public AlienSpecies speciesEnum;
    // Creation parameters
    public Vector2 defenceMinMax, attackSpeedMinMax, movementSpeedMinMax, damageMinMax, healthMinMax, rangeMinMax;

}
