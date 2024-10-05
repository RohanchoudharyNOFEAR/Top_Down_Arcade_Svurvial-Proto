using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerWood;
    public int playerStone;
    [SerializeField] private List<Cell> unlockedAreas;
    public List<Cell> UnlockedAreas
    {
        get
        {
            if (unlockedAreas != null)
            { return unlockedAreas; }
            else { unlockedAreas = new List<Cell>(); return unlockedAreas; }
        }

        set { unlockedAreas = value; }
    }
}

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem instance;
    public GameData gameData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        DontDestroyOnLoad(gameObject);
        LoadGame();
        
    }

    private void Start()
    {

    }

    public async void SetWoodSaveData(int woodCount)
    {
        if (gameData == null)
        {
            gameData = new GameData();
            gameData.playerWood = woodCount;
        }
        else
        {
            gameData.playerWood += woodCount;
        }
      await  SaveGame();
    }
    public async void SetStoneSaveData(int StoneCount)
    {
        if (gameData == null)
        {
            gameData = new GameData();
            gameData.playerStone = StoneCount;
        }
        else
        {
            gameData.playerStone += StoneCount;
        }
       await SaveGame();
    }
    public async void SetUnlockedAreaSaveData(List<Cell> unlockedCell)
    {
        if (gameData == null)
        {
            gameData = new GameData();
            gameData.UnlockedAreas = unlockedCell;
        }
        else
        {
            gameData.UnlockedAreas = unlockedCell;
        }

      await  SaveGame();
    }

    public async void SetSaveData(int woodCount, int StoneCount, List<Cell> unlockedCell)
    {
        gameData = new GameData();
        gameData.playerWood = woodCount;
        gameData.playerStone = StoneCount;
        gameData.UnlockedAreas = unlockedCell;

      await  SaveGame();
    }

    public async Task  SaveGame()
    {
        string jsonData = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("SaveData", jsonData);
        PlayerPrefs.Save();
        await Task.Delay(500);
        Debug.Log("Game Saved!" + gameData.playerWood + "<-wood " + gameData.playerStone + "<-Stone " + gameData.UnlockedAreas + " unlocked area");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SaveData"))
        {
            string jsonData = PlayerPrefs.GetString("SaveData");
            gameData = JsonUtility.FromJson<GameData>(jsonData);
            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.Log("No Save Data Found!");
        }
    }

    public  async void ResetData()
    {
        gameData = new GameData();
        await SaveGame();
        Application.Quit();
    }
}
