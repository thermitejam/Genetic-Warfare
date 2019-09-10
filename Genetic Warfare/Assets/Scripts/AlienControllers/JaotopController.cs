using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaotopController : AlienBase
{
    public void Create()
    {
        SetDefenceMinMax(0.2f, 0.6f);
        SetAttackSpeedMinMax(0, 0.3f);
        SetMovementSpeedMinMax(0.4f, 0.7f);
        SetDamageMinMax(0.1f, 0.3f);
        SetHealthMinMax(0.2f, 0.7f);
        SetRangeMinMax(0.1f, 0.3f);
        GenerateBase();       
        GenerateScore();
        GenerateID(0, 0);
    }
}
