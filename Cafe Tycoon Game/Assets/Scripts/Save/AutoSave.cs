using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : Singleton<AutoSave>
{
    public GameData gameData;
    public float autoSaveInterval = 30f; // Auto-save every 30 seconds
    public MachineObject[] Machines;
    public TableObject[] Tables;

    private void Start()
    {
        LoadGame();
        SaveGame();
    }

    private IEnumerator AutoSaveRoutine()
    {
        yield return new WaitForSeconds(autoSaveInterval);
        SaveGame();
    }

    public void SaveGame()
    {
        gameData = new GameData(Machines, Tables);

        SaveSystem.Save(gameData);
        StartCoroutine(AutoSaveRoutine());
    }

    public void LoadGame()
    {
        gameData = SaveSystem.Load();

        Machines = gameData.DataToMachine(Machines);
        Tables = gameData.DataToTable(Tables);
        GameManager.Instance.Money = gameData.money;
        GameManager.Instance.Level = gameData.level;
    }


    [ContextMenu("Delete Save File")]
    private void DeleteFile()
    {
        SaveSystem.DeleteSave();
    }
}
