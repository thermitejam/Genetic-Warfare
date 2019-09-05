using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateJaotop : MonoBehaviour
{
    [SerializeField] GameObject jao;
    public void SpawnJaotop()
    {
        Instantiate(jao);
    }
}
