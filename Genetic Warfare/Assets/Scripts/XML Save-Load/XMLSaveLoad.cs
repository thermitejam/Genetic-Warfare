using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLSaveLoad : MonoBehaviour
{
    public static XMLSaveLoad ins;
    
    private void OnApplicationQuit()
    {
        SaveGame tmp = new SaveGame();
        tmp.aliens = Inventory.ins.GetInventory();
        tmp.money = Inventory.ins.GetMoney();
        Save(tmp);
    }

    private void Awake()
    {
        ins = this;
    }

    public void Save(SaveGame data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
        FileStream stream = new FileStream(Application.dataPath + "/SaveGame.xml", FileMode.Create);
        serializer.Serialize(stream, data);
        stream.Close();
    }
    public SaveGame Load()
    {
        SaveGame saveGame = new SaveGame();
        XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
        FileStream stream = new FileStream(Application.dataPath + "/SaveGame.xml", FileMode.OpenOrCreate);
        saveGame = serializer.Deserialize(stream) as SaveGame;
        stream.Close();
        return saveGame;
    }
}

[System.Serializable]
public class alienEntry
{
    public string id;
    public string name;
    //public Color color;
    public AlienSpecies species;
    public int pos;
    public int ShopPrice;
    public float score;
    public float Defence;
    public float AttackSpeed;
    public float MovementSpeed;
    public float Damage;
    public float Health;
    public float Range;
}

[System.Serializable]
public class SaveGame
{
    public int money;
    public List<alienEntry> aliens = new List<alienEntry>();
}
