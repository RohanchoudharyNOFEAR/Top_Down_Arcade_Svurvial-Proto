using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//singleton that Controls Grid(Cells)
public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public Cell[] Cells;
    public List<Cell> UnlockedCells;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initilize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Unlockes Cells corresponding to the given Cell index in grid
    public void UnlockCells(Vector2[] cellIndexs)
    {
        foreach (Vector2 index in cellIndexs)
        {
            var cell = GetCellFromIndex(index);
            if (cell == null || UnlockedCells.Contains(cell))
            {
                return;

            }
            else
            {
                cell.gameObject.SetActive(true);
                cell.IsUnlocked = true;
                UnlockedCells.Add(cell);
                SaveLoadSystem.instance.SetUnlockedAreaSaveData(UnlockedCells);
            }
        }

    }

    //Unlockes single Cells corresponding to the given Cell index in grid
    public void UnlockCell(Vector2 cellIndex)
    {
        
            var cell = GetCellFromIndex(cellIndex);
            if (cell == null || UnlockedCells.Contains(cell))
            {
                return;

            }
            else
            {
                cell.gameObject.SetActive(true);
                cell.IsUnlocked = true;
                UnlockedCells.Add(cell);
                SaveLoadSystem.instance.SetUnlockedAreaSaveData(UnlockedCells);
            }
        

    }

    void Initilize()
    {

        Cells = GameObject.FindObjectsOfType<Cell>();

        foreach (Cell cell in Cells)
        {
            if (cell.IsUnlocked)
            {
                // UnlockedCells.Add(cell);
            }
            else
            {
                cell.gameObject.SetActive(false);
            }
        }

        loadUnlockedCells();

        foreach (Cell cell in UnlockedCells)
        {
            cell.gameObject.SetActive(true);
            cell.IsUnlocked = true;
        }



    }

    void loadUnlockedCells()
    {
        UnlockedCells = SaveLoadSystem.instance.gameData.UnlockedAreas;

    }

    //Helper Function to get cell from index
    Cell GetCellFromIndex(Vector2 cellIndex)
    {
        foreach (Cell cell in Cells)
        {
            if (cell.CellNumber == cellIndex)
            {
                return cell;
            }

        }
        return null;
    }

    public bool IsCellAtIndexUnlocked(Vector2 cellIndex)
    {
        if (GetCellFromIndex(cellIndex).IsUnlocked == true) { return true; }
        else { return false; }
    }

}
