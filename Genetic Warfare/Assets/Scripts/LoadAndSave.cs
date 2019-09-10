using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LoadAndSave : MonoBehaviour
{
    private string activeAlienIdsString;
    private string[] alienIdsArray;
    public Dictionary<int, string> alienDataDict = new Dictionary<int,string>();

    private int idStringToInt;

    private List<int> ToAddToInvent = new List<int>();
    private List<int> ToAddToFloor = new List<int>();

    private InventoryManager invent;

    private GameObject createdAlien;
    private string[] unpackedID;
    [SerializeField] private GameObject[] aliensArray;

    private List<string> idStringHolder = new List<string>();

    void Start() // Load
    {
        invent = GameObject.Find("Inventory").GetComponent<InventoryManager>(); // Grab inventory so we can add to it 

        //activeAlienIdsString = PlayerPrefs.GetString("alienIdsString"); // Get array of ID's which we seperate with /

        alienIdsArray = PlayerPrefs.GetString("alienIdsString").Split('/'); // Give each of the ID's a place in an array

        Debug.Log(PlayerPrefs.GetString("alienIdsString"));
        foreach (string id in alienIdsArray) // Then pair the id's with it's corresponding string, which will hold the alien data
        {
            int.TryParse(id, out idStringToInt); // have to use tryparse for some reason, parse throws an error            
            alienDataDict[idStringToInt] = PlayerPrefs.GetString(id);
        }

        if (alienDataDict.ContainsValue("")) // lazy solution will fix later but it works for now
        {
            alienDataDict.Remove(0);
        }

        //PlayerPrefs.DeleteKey("alienIdsString"); // Useful for clearing both data structures
        //alienDataDict.Clear();

        foreach (KeyValuePair<int, string> id in alienDataDict)
        {
            if (PlayerPrefs.GetString(id.Key.ToString()).Split('/')[2] == "1") // if the alien is in the inventory (1)
            {
                ToAddToInvent.Add(id.Key);
            }
            else if (PlayerPrefs.GetString(id.Key.ToString()).Split('/')[2] == "2") // if the alien is on the floor (2)
            {
                ToAddToFloor.Add(id.Key);
            }
        }

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0: // Home
                foreach (int item in ToAddToInvent)
                {
                    invent.AddToInventory(item);
                }
                foreach (int item in ToAddToFloor)
                {
                    invent.AddToInventory(item);
                    AddAlienToFloor(item);
                }
                break;
            case 1: // Fighting scene
                break;
            case 2: // Breeding scene
                break;
            case 3: // Shop scene
                break;
            case 4: // Evotree scene
                break;
        }
    }

    public void Add(int id) // Add ID to id string so we have it's data and we can load in future
    {
        // If the id is new and isn't already in the id list, add the id to the list
        // If it's already in the list then we don't need to update it or anything, since the next time it's loaded it's values will be updated in the dictionary
        if (!PlayerPrefs.GetString("alienIdsString").Contains(id.ToString())) 
        {
            PlayerPrefs.SetString("alienIdsString", PlayerPrefs.GetString("alienIdsString") + "/" + id.ToString());
        }          
        // if it's not fighting or in the shop you should add it to the inventory. use a parameter to check (maybe 0-3 or something?)      
    }

    public void AddAlienToFloor(int id)
    {

        // Using it's data we know what type it is, so we instantiate it on the floor. this allows us to retain a ghost copy in the inventory
        createdAlien = Instantiate(aliensArray[int.Parse(PlayerPrefs.GetString(id.ToString()).Split('/')[1])]);

        unpackedID = PlayerPrefs.GetString(id.ToString()).Split('/');

        createdAlien.GetComponent<AlienBase>().SetID(int.Parse(unpackedID[0]));
        GameObject.Find("DataManager").GetComponent<IDManager>().SetInventorySlot(id, int.Parse(unpackedID[11]));

        createdAlien.transform.position = new Vector3(Random.Range(0.0f, 2.0f), Random.Range(0.0f, 3.0f), 0); // Randomize it's position
        GameObject.Find("DataManager").GetComponent<IDManager>().SetPosition(id, 2); // Set it's position to be on the floor
        GameObject.Find("Inventory").GetComponent<InventoryManager>().AlienOnFloor(false, GameObject.Find("DataManager").GetComponent<IDManager>().GetInventorySlot(id));
    }

    public void RemoveFromFloor(GameObject alien)
    {
        // change it's position to be in inventory
        GameObject.Find("DataManager").GetComponent<IDManager>().SetPosition(alien.GetComponent<AlienBase>().GetID(), 1);
        GameObject.Find("Inventory").GetComponent<InventoryManager>().AlienOnFloor(true, GameObject.Find("DataManager").GetComponent<IDManager>().GetInventorySlot(alien.GetComponent<AlienBase>().GetID()));
        // destroy the game object
        Destroy(alien);
    }

    public void DeleteAlien(int alienID)
    {
        PlayerPrefs.SetString("alienIdsString", PlayerPrefs.GetString("alienIdsString").Replace("/" + alienID.ToString(), "")); // Remove from ID string
        alienDataDict.Remove(alienID); // Remove the key & value associated with it from the alien data dictionary

        /*idStringHolder = PlayerPrefs.GetString("alienIdsString").Split('/').ToList(); // Convert the split alien ID's string into a list
        idStringHolder.Remove(alienID.ToString()); // Use the remove function (which you can't do with an array
        idStringHolder.ToArray(); // Convert back to an array
        foreach(string id in idStringHolder) // Then run through the array of id's, adding it to the new string each time
        {
            PlayerPrefs.SetString("alienIdsString", PlayerPrefs.GetString("alienIdsString") + id + "/");
        }

        alienDataDict.Remove(alienID); // Remove (using the alien ID) the dictionary entry*/

    }
}
