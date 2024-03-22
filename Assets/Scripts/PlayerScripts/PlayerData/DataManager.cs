using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    private string dataPath;

    void Start()
    {
        dataPath = Application.persistentDataPath + "/playerData.json";
    }

    public void SavePlayerData(PlayerData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, jsonData); // 指定したパスにファイルが存在しない場合は新しいファイルを作成する
        Debug.Log("Data saved to " + dataPath);
    }

    public PlayerData LoadPlayerData()
    {
        if (File.Exists(dataPath))
        {
            string jsonData = File.ReadAllText(dataPath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(jsonData);
            Debug.Log("Data loaded from " + dataPath);
            return data;
        }
        else
        {
            Debug.Log("No data found at " + dataPath + ", returning new PlayerData object");
            return new PlayerData();
        }
    }
}
