using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    private IDManager idManager;

    private void Start()
    {
        idManager = GameObject.Find("DataManager").GetComponent<IDManager>(); // Get the ID manager script from the Data Manager       
        // The ID Manager can change and retrieve using an alien ID
        // An alien ID holds all it's data. It stores all these things
        /* ID Data Key:
       0: Alien ID
       1: Alien Type (0=Jaotop, 1=Qayrat, update when more are added)
       2: Position (0 = nowhere(just spawned), 1 = inventory, 2 = floor (anything on the floor is also in the inventory)
       3: Score
       4: Shop Price
       5: Defence
       6: Attack Speed
       7: Movement Speed
       8: Damage
       9: Health
       10: Range
       11: Inventory Slot (-1 by default because it has no slot)
        */
        // You don't need to know them since we have ID manager
        // You would get a value with idManager.GetDefence(alienID);
        // You would set a value with idManager.SetDefence(alienID, value);
        // To create an alien you just Instantiate(alien)
        // Then to generate stats you would do alien.GetComponent<It's Script>.Create()
        // That will generate a new id and base stats and everything else
        // When we move from inventory to the floor we don't .Create() since we already have the aliens ID 
        // We just create an empty game object and do alien.SetID(alienID)
        // So as long as we have an alien ID we can generate a new alien anywhere we want without a game object
    }
}
