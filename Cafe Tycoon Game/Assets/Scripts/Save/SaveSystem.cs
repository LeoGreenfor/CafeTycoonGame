using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath = Path.Combine(Application.persistentDataPath, "save.json");

    public static void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved to " + savePath);
    }

    public static GameData Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game loaded from " + savePath);
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found! Creating a new one.");
            return new GameData(AutoSave.Instance.Machines, AutoSave.Instance.Tables); // Return a new game state if no save file exists
        }
    }
    public static void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save file deleted.");
        }
    }
}
