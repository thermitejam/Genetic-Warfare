using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateJaotop : MonoBehaviour
{
    [SerializeField] GameObject jao;
    public void SpawnJaotop()
    {
        GameObject JaoInstance = Instantiate(jao);
        JaoInstance.transform.position = new Vector3(Random.Range(0.0f,2.0f), Random.Range(0.0f, 3.0f), 0);
    }
}
