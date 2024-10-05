using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryViewUI : MonoBehaviour
{
    public TMP_Text WoodText;
    public TMP_Text StoneText;

    private void OnEnable()
    {
        Inventory.UpdateWoodUIEvent += UpdateWoodText;
        Inventory.UpdateStoneUIEvent += UpdateStoneText;
    }

    private void OnDisable()
    {
        Inventory.UpdateWoodUIEvent -= UpdateWoodText;
        Inventory.UpdateStoneUIEvent -= UpdateStoneText;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateWoodText(int woodCount)
    {
        WoodText.text = "Wood : " + woodCount;

    }

    void UpdateStoneText(int stoneCount)
    {

        StoneText.text = "Stone : " + stoneCount;
    }

}
