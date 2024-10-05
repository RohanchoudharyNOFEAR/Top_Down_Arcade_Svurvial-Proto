using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOT USED IN GAME
//Just an Simple Grid Generator for Testing layout
public class HexGridGenerator: MonoBehaviour
{
    [SerializeField]
    private GameObject[] hexs;

    public int gridWidth = 10; // Width of the hex grid
    public int gridHeight = 10; // Height of the hex grid
    public float hexWidth = 1.732f; // Adjust according to hex tile size
    public float hexHeight = 2f;

    // Start is called before the first frame update
    void Start()
    {
       // placeGrid();
       GenerateHexGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void placeGrid()
    {

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {//for x
             //[cell size(1) - dist to place cell attach to each other(0.84)(also the width of cell hex)]=0.16(distance thats removed from 1 cell size to make it close to other cells)
             //for y   
             //[cell size(1) - dist to place cell attach to each other(0.75)(also the height of cell hex)]=0.25(distance thats removed from 1 cell size to make it close to other cells)
                float offfset = 0;
                if (j % 2 == 0)
                {
                    //offfset = 0.418f;
                    offfset = 10.18f;
                }
                //  Instantiate(hexs[Random.Range(0,hexs.Length)], new Vector3(offfset + i - (i * 0.16f), 0, j - (j * 0.25f)), Quaternion.identity);
                Instantiate(hexs[Random.Range(0, hexs.Length)], new Vector3(offfset + i - (i * 0.001f), 0, j - (j * 0.001f)), Quaternion.identity);
            }
        }
    }

    void GenerateHexGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Offset every other row
                float xOffset = (y % 2 == 0) ? 0 : hexWidth / 2;
                Vector3 hexPos = new Vector3(x * hexWidth + xOffset, 0, y * hexHeight * 0.75f);
                Instantiate(hexs[Random.Range(0, hexs.Length)], hexPos, Quaternion.Euler(0, 29f, 0),this.transform);
            }
        }
    }

}


