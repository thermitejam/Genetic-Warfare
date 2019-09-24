using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory ins;

    private void Awake()
    {
        ins = this;
    }

    [SerializeField] int money;
    [SerializeField] List<alienEntry> aliens = new List<alienEntry>();

    [SerializeField] int maxInventorySize = 12;

    private void Start()
    {
        aliens = XMLSaveLoad.ins.Load().aliens;
    }

    public bool doesAlienExsist(string id)
    {
        foreach (alienEntry alien in aliens)
        {
            if (alien.id == id)
            {
                return true;
            }
        }
        return false;
    }
    public alienEntry getAlien(string id)
    {
        foreach (alienEntry alien in aliens)
        {
            if (alien.id == id)
            {
                return alien;
            }
        }
        Debug.Log("Error: DID NOT FINE AN ALIEN, WITH THAT ID");
        return null;
    }

    public alienEntry getAlien(int index)
    {
        if (index < aliens.Count || index >= 0)
        {
            return aliens[index];
        }
        else
        {
            Debug.Log("Error: INDEX OUT OF BOUNDS");
            return null;
        }
    }

    public void addAlienToInventory(alienEntry alien)
    {
        aliens.Add(alien);
    }

    public void deleteAlien(alienEntry alien)
    {
        aliens.Remove(alien);
    }

    public void deleteAlien(string id)
    {
        foreach (alienEntry alien in aliens)
        {
            if (alien.id == id)
            {
                aliens.Remove(alien);
                return;
            }
        }
        Debug.Log("Error: DID NOT FINE AN ALIEN, WITH THAT ID");
    }

    public void deleteAlien(int index)
    {
        if (index < aliens.Count || index >= 0)
        {
            aliens.RemoveAt(index);
        }
        else
        {
            Debug.Log("Error: INDEX OUT OF BOUNDS");
        }

    }

    public void AddMoney(int amount)
    {
        money += amount;
    }
    public void RemoveMoney(int amount)
    {
        money -= amount;
    }
    public int GetMoney ()
    {
        return money;
    }
    public int getAlienCount()
    {
        return aliens.Count;
    }

    public List<alienEntry> GetInventory()
    {
        return aliens;
    }
}
