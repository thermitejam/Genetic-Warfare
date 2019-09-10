using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QayratController: AlienBase
{
    public void Create()
    {
        SetDefenceMinMax(0.1f, 0.5f);
        SetAttackSpeedMinMax(0, 0.2f);
        SetMovementSpeedMinMax(0.4f, 0.6f);
        SetDamageMinMax(0.1f, 0.5f);
        SetHealthMinMax(0.2f, 0.6f);
        SetRangeMinMax(0.2f, 0.7f);
        GenerateBase();
        GenerateScore();
        GenerateID(1,0);
    }
}
