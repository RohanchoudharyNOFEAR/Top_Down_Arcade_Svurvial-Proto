using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    // Resource dictionary to store multiple resource types
    private Dictionary<string, int> resources = new Dictionary<string, int>();

    public static Action<int> UpdateWoodUIEvent;
    public static Action<int> UpdateStoneUIEvent;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        initilize();
    }

    void initilize()
    {
        LoadData();
    }

    void LoadData()
    {
        SetResource("Wood", SaveLoadSystem.instance.gameData.playerWood);
        SetResource("Stone", SaveLoadSystem.instance.gameData.playerStone);

    }

    //And Updates ui
    public void SetResource(string resourceType, int amount)
    {

        resources[resourceType] = amount;
        if (resourceType == "Wood")
        {
            if (UpdateWoodUIEvent != null)
            {
                UpdateWoodUIEvent.Invoke(resources["Wood"]);
            }
        }
        else if (resourceType == "Stone")
        {
            if (UpdateStoneUIEvent != null)
            {
                UpdateStoneUIEvent.Invoke(resources["Stone"]);
            }
        }
    }

    // Method to add resources to the inventory And Updates ui
    public void AddResource(string resourceType, int amount)
    {
        if (resources.ContainsKey(resourceType))
        {
            resources[resourceType] += amount;
        }
        else
        {
            resources[resourceType] = amount;
        }

        if (resourceType == "Stone")
        {
            if (UpdateStoneUIEvent != null)
            {
                UpdateStoneUIEvent.Invoke(resources["Stone"]);
            }
            SaveLoadSystem.instance.SetStoneSaveData(amount);
        }
        if (resourceType == "Wood")
        {
            if (UpdateWoodUIEvent != null)
            {
                UpdateWoodUIEvent.Invoke(resources["Wood"]);
            }
            SaveLoadSystem.instance.SetWoodSaveData(amount);
        }


        Debug.Log($"{amount} {resourceType} added. Total: {resources[resourceType]}");
    }

    // Method to check if player has enough resources
    public bool HasResources(string resourceType, int amount)
    {
        return resources.ContainsKey(resourceType) && resources[resourceType] >= amount;
    }

    // Method to deduct resources from the inventory And Updates ui
    public void RemoveResource(string resourceType, int amount)
    {
        if (HasResources(resourceType, amount))
        {
            resources[resourceType] -= amount;
            Debug.Log($"{amount} {resourceType} used. Remaining: {resources[resourceType]}");
        }
        else
        {
            Debug.Log($"Not enough {resourceType} to remove.");
        }

        if (resourceType == "Stone")
        {
            if (UpdateStoneUIEvent != null)
            {
                UpdateStoneUIEvent.Invoke(resources["Stone"]);
            }
            SaveLoadSystem.instance.SetStoneSaveData(-amount);
        }
        else if (resourceType == "Wood")
        {
            if (UpdateWoodUIEvent != null)
            {
                UpdateWoodUIEvent.Invoke(resources["Wood"]);
            }
            SaveLoadSystem.instance.SetWoodSaveData(-amount);
        }
    }

    private void Update()
    {
        // DisplayInventory();
    }

    public void DisplayInventory()
    {
        foreach (KeyValuePair<string, int> entry in resources)
        {
            Debug.Log($"{entry.Key}: {entry.Value}");
        }
    }
}
