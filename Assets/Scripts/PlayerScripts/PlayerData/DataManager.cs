using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    private string dataPath;

    private void Awake()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "playerData.json");
    }

    public void SavePlayerData(PlayerData data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(dataPath, jsonData);
    }

    public PlayerData LoadPlayerData()
    {
        if (File.Exists(dataPath))
        {
            string jsonData = File.ReadAllText(dataPath);
            return JsonUtility.FromJson<PlayerData>(jsonData);
        }
        return new PlayerData(); // ファイルが存在しない場合はデフォルト値を返す
    }
}