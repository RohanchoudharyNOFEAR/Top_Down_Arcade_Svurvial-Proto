using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockingCell : Cell, IInteractable
{
    public int WoodCost;
    public int StoneCost;
    public Vector2[] CellsToUnlock;

    public void interact()
    {
        // Check if player has enough resources
        if (Inventory.instance.HasResources("Stone", StoneCost) && Inventory.instance.HasResources("Wood", WoodCost)  )
        {
            // Unlock and instantiate adjacent hexagons
            UnlockArea();
        }
        else
        {
            Debug.Log("Not enough resources to unlock this area.");
        }
    }

    private void UnlockArea()
    {
        Debug.Log("Area Unlocked!");

        for(int i = 0; i < CellsToUnlock.Length; i++) 
        {
            if (GridManager.Instance.IsCellAtIndexUnlocked(CellsToUnlock[i]))
            {
                return;
            }
            else
            {
                GridManager.Instance.UnlockCell(CellsToUnlock[i]);
                if (i == CellsToUnlock.Length-1)
                {
                    Inventory.instance.RemoveResource("Stone", StoneCost);
                    Inventory.instance.RemoveResource("Wood", WoodCost);
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            interact();
        }
    }
}
