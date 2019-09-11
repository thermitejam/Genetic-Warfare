using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LoadAndSave : MonoBehaviour
{
    // Start function variables
    private InventoryManager invent;
    private string[] alienIdsArray;
    private int idStringToInt;
    public Dictionary<int, string> alienDataDict = new Dictionary<int,string>(); 
    private List<int> ToAddToInvent = new List<int>();
    private List<int> ToAddToFloor = new List<int>();

    private GameObject createdAlien;
    [SerializeField] private GameObject[] aliensArray;
    private IDManager idManager;

    void Start()
    {
        invent = GameObject.Find("Inventory").GetComponent<InventoryManager>(); // Grab inventory so we can add to it 
        idManager = GameObject.Find("DataManager").GetComponent<IDManager>();

        alienIdsArray = PlayerPrefs.GetString("alienIdsString").Split('/'); // Give each of the ID's a place in an array

        foreach (string id in alienIdsArray) // Then pair the id's with it's corresponding string, which will hold the alien data
        {
            int.TryParse(id, out idStringToInt); // TryParse instead of just parsing (i don't know why)       
            alienDataDict[idStringToInt] = PlayerPrefs.GetString(id);
        }

        if (alienDataDict.ContainsValue("")) // Removes initial value at the start which has no value
        {
            alienDataDict.Remove(0);
        }

        //PlayerPrefs.DeleteKey("alienIdsString"); // Useful for clearing both data structures
        //alienDataDict.Clear();

        foreach (KeyValuePair<int, string> id in alienDataDict) // Add aliens to lists so we can load them into the correct scenes
        {
            //PlayerPrefs.GetString(id.Key.ToString()).Split('/')[2]
            if (idManager.GetPosition(id.Key) == 1 || idManager.GetPosition(id.Key) == 3) // If the alien's in the inventory or the bleeding slot, add it to the inventory
            {
                ToAddToInvent.Add(id.Key);
            }
            else if (idManager.GetPosition(id.Key) == 2) // If the alien should b on the floor (2)
            {
                ToAddToFloor.Add(id.Key);
            }
        }

        switch (SceneManager.GetActiveScene().buildIndex) // Decide which aliens to load and where to load them based on what scene we're in
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
                foreach (int item in ToAddToInvent)
                {
                    invent.AddToInventory(item);                  
                }
                foreach (int item in ToAddToFloor)
                {
                    invent.AddToInventory(item);
                    GameObject.Find("DataManager").GetComponent<IDManager>().SetInventorySlot(item, idManager.GetInventorySlot(item)); // Set the inventory slot to be the slot of the inventory alien
                    GameObject.Find("Inventory").GetComponent<InventoryManager>().AlienOnFloor(false, GameObject.Find("DataManager").GetComponent<IDManager>().GetInventorySlot(item)); // Make it's inventory slot un-interactable
                }
                    break;
            case 3: // Shop scene
                break;
            case 4: // Evotree scene
                break;
        }
    }

    public void AddToActiveAliens(int id) // Add ID to id string so we have it's data and we can load in future
    {
        if (!PlayerPrefs.GetString("alienIdsString").Contains(id.ToString())) 
        {
            PlayerPrefs.SetString("alienIdsString", PlayerPrefs.GetString("alienIdsString") + "/" + id.ToString());
        }            
    }

    public void AddAlienToFloor(int id)
    {
        // Using it's data we know what type it is, so we instantiate it on the floor. this allows us to retain a ghost copy in the inventory
        createdAlien = Instantiate(aliensArray[idManager.GetAlienType(id)]); // Instantiate an alien based on it's type
        createdAlien.GetComponent<AlienBase>().SetID(id); // Set the id of the new alien to the id of the inventory alien

        GameObject.Find("DataManager").GetComponent<IDManager>().SetInventorySlot(id, idManager.GetInventorySlot(id)); // Set the inventory slot to be the slot of the inventory alien
        GameObject.Find("DataManager").GetComponent<IDManager>().SetPosition(id, 2); // Set it's position to be on the floor

        createdAlien.transform.position = new Vector3(Random.Range(0.0f, 2.0f), Random.Range(0.0f, 3.0f), 0); // Randomize it's position
        
        GameObject.Find("Inventory").GetComponent<InventoryManager>().AlienOnFloor(false, GameObject.Find("DataManager").GetComponent<IDManager>().GetInventorySlot(id)); // Make it's inventory slot un-interactable
    }

    public void RemoveFromFloor(GameObject alien)
    {
        // Change it's position to be in inventory
        GameObject.Find("DataManager").GetComponent<IDManager>().SetPosition(alien.GetComponent<AlienBase>().GetID(), 1);
        GameObject.Find("Inventory").GetComponent<InventoryManager>().AlienOnFloor(true, GameObject.Find("DataManager").GetComponent<IDManager>().GetInventorySlot(alien.GetComponent<AlienBase>().GetID()));
        
        // Destroy the game object
        Destroy(alien);
    }

    public void DeleteAlien(int alienID)
    {
        PlayerPrefs.SetString("alienIdsString", PlayerPrefs.GetString("alienIdsString").Replace("/" + alienID.ToString(), "")); // Remove from ID string
        alienDataDict.Remove(alienID); // Remove the key & value associated with it from the alien data dictionary
    }
}
