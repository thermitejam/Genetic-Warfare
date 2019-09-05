using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAndSave : MonoBehaviour
{
    private string activeAlienIdsString;
    private string[] alienIdsArray;
    public Dictionary<int, string> alienDataDict = new Dictionary<int,string>();

    private int idStringToInt;

    void Start() // Load
    {
        activeAlienIdsString = PlayerPrefs.GetString("alienIdsString"); // Get array of ID's which we seperate with /

        alienIdsArray = activeAlienIdsString.Split('/'); // Give each of the ID's a place in an array

        foreach (string id in alienIdsArray) // Then pair the id's with it's corresponding string, which will hold the alien data
        {
            int.TryParse(id, out idStringToInt); // have to use tryparse for some reason, parse throws an error
            //alienDataDict.Add(idStringToInt, PlayerPrefs.GetString(id));
            alienDataDict[idStringToInt] = PlayerPrefs.GetString(id);
            Debug.Log("Alien ID: " + idStringToInt + " Data: " + alienDataDict[idStringToInt]); // prints out the loaded 
        }

        //PlayerPrefs.DeleteKey("alienIdsString"); // Useful for clearing both data structures
        //alienDataDict.Clear();

        // using the scene parameter we can determine which scene the alien should be instantiated in
        // using an enum we can also decide whether the alien should be in our inventory, on the ground, or showing as an opponent to fight
        // so here would be the code to instantiate anything that needed to be instantiated
    }

    public void Add(int id) // Add ID to id string so we have it's data and we can load in future
    {
        // If the id is new and isn't already in the id list, add the id to the list
        // If it's already in the list then we don't need to update it or anything, since the next time it's loaded it's values will be updated in the dictionary
        if (!PlayerPrefs.GetString("alienIdsString").Contains(id.ToString())) 
        {
            PlayerPrefs.SetString("alienIdsString", PlayerPrefs.GetString("alienIdsString") + "/" + id.ToString());
        }        
        
    }

    public void Remove(int id)
    {
        PlayerPrefs.SetString("alienIdsString", PlayerPrefs.GetString("alienIdsString").Replace("/" + id.ToString(), "")); // Remove from ID string

        alienDataDict.Remove(id); // Remove the key & value associated with it from the alien data dictionary
    }

}
