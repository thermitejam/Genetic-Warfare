using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDManager : MonoBehaviour
{
    private string[] unpackedID;

    public int GetAlienType(int id) { return GetIDValueAsInt(id, 1); }
    public int GetPosition(int id) { return GetIDValueAsInt(id, 2); }
    public float GetScore(int id) { return GetIDValueAsInt(id, 3); }
    public float GetShopPrice(int id) { return GetIDValueAsInt(id, 4); }    
    public float GetDefence(int id) { return GetIDValueAsInt(id, 5); }
    public float GetAttackSpeed(int id) { return GetIDValueAsInt(id, 6); }
    public float GetMovementSpeed(int id) { return GetIDValueAsInt(id, 7); }
    public float GetDamage(int id) { return GetIDValueAsInt(id, 8); }
    public float GetHealth(int id) { return GetIDValueAsInt(id, 9); }
    public float GetRange(int id) { return GetIDValueAsInt(id, 10); }
    public int GetInventorySlot(int id) { return GetIDValueAsInt(id, 11); }

    public int GetIDValueAsInt(int id, int index)
    {
        return int.Parse(PlayerPrefs.GetString(id.ToString()).Split('/')[index]);
    }

    public float GetIDValueAsFloat(int id, int index)
    {
        return float.Parse(PlayerPrefs.GetString(id.ToString()).Split('/')[index]);
    }

    public void SetAlienType(int id, int type) { SetIDValueAsInt(id, 1, type); }
    public void SetPosition(int id, int position) { SetIDValueAsInt(id, 2, position); }
    public void SetScore(int id, float sc) { SetIDValueAsFloat(id, 3, sc); }
    public void SetShopPrice(int id, int sp) { SetIDValueAsInt(id, 4, sp); }
    public void SetDefence(int id, float df) { SetIDValueAsFloat(id, 5, df); }
    public void SetAttackSpeed(int id, float aspd) { SetIDValueAsFloat(id, 6, aspd); }
    public void SetMovementSpeed(int id, float mvspd) { SetIDValueAsFloat(id, 7, mvspd); }
    public void SetDamage(int id, float dmg) { SetIDValueAsFloat(id, 8, dmg); }
    public void SetHealth(int id, float hlth) { SetIDValueAsFloat(id, 9, hlth); }
    public void SetRange(int id, float rnge) { SetIDValueAsFloat(id, 10, rnge); }
    public void SetInventorySlot(int id, int slot) { SetIDValueAsInt(id, 11, slot); }

    public void SetIDValueAsInt(int alienID, int valueIndex, int value)
    {
        unpackedID = PlayerPrefs.GetString(alienID.ToString()).Split('/');
        unpackedID[valueIndex] = value.ToString();
        PlayerPrefs.SetString(alienID.ToString(), unpackedID[0] + "/" + unpackedID[1] + "/" + unpackedID[2] + "/" + unpackedID[3] + "/" + unpackedID[4] + "/" + unpackedID[5] + "/" + unpackedID[6] + "/"
        + unpackedID[7] + "/" + unpackedID[8] + "/" + unpackedID[9] + "/" + unpackedID[10] + "/" + unpackedID[11]);
    }

    public void SetIDValueAsFloat(int alienID, int valueIndex, float value)
    {
        unpackedID = PlayerPrefs.GetString(alienID.ToString()).Split('/');
        unpackedID[valueIndex] = value.ToString();
        PlayerPrefs.SetString(alienID.ToString(), unpackedID[0] + "/" + unpackedID[1] + "/" + unpackedID[2] + "/" + unpackedID[3] + "/" + unpackedID[4] + "/" + unpackedID[5] + "/" + unpackedID[6] + "/"
        + unpackedID[7] + "/" + unpackedID[8] + "/" + unpackedID[9] + "/" + unpackedID[10] + "/" + unpackedID[11]);
    }
}
