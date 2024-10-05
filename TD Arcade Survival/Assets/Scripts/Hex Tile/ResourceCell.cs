using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Representation of Resource Cell - Attacked on Root Cell
public class ResourceCell : Cell
{
    public GameObject[] ResourcePrefabs;

    private void Awake()
    {
      GameObject instance=  Instantiate(ResourcePrefabs[Random.Range(0, ResourcePrefabs.Length)],transform.position, Quaternion.Euler(0, 29f, 0), this.transform);

    }
}
