using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBreed : MonoBehaviour
{
    [SerializeField] GenerateAlien generator;

    public void PressButton()
    {
        generator.BreedAlien(Inventory.ins.getAlien(0), Inventory.ins.getAlien(1));
    }
}
