using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    string alienId;
    [SerializeField] Image slot_icon;
    [SerializeField] Text slot_name;
    [SerializeField] Text slot_Species;
    [SerializeField] Text slot_Score;
    [SerializeField] Text slot_Health;
    [SerializeField] Text slot_Damage;
    [SerializeField] Text slot_Defence;
    [SerializeField] Text slot_AttackSpeed;
    [SerializeField] Text slot_MoveSpeed;
    [SerializeField] Text slot_Range;

    public void UpdateInfo(string ID)
    {
        if (Inventory.ins.doesAlienExsist(ID))
        {
            alienId = ID;
            ApplyInfo(Inventory.ins.getAlien(alienId));
        }
        else
        {
            Debug.Log("Error: ALIEN DOES NOT EXSIST");
        }
    }
    public void RefreshInfo()
    {
        if (alienId == null)
        {
            Debug.Log("Error: ALIEN ID FOR SLOT NOT SET");
            return;
        }

        if (Inventory.ins.doesAlienExsist(alienId))
        {
            ApplyInfo(Inventory.ins.getAlien(alienId));
        }
        else
        {
            Debug.Log("Error: ALIEN DOES NOT EXSIST");
        }
    }

    void ApplyInfo(alienEntry alien)
    {
        slot_name.text = alien.name;
        slot_Species.text = alien.species.ToString();
        slot_Score.text = alien.score.ToString();
        slot_Health.text = alien.Health.ToString();
        slot_Damage.text = alien.Damage.ToString();
        slot_Defence.text = alien.Defence.ToString();
        slot_AttackSpeed.text = alien.AttackSpeed.ToString();
        slot_MoveSpeed.text = alien.MovementSpeed.ToString();
        slot_Range.text = alien.Range.ToString();
    }

}
