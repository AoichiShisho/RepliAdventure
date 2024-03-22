using UnityEngine;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI coinsText;
    private DataManager dataManager;

    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        // dataManagerが正しくアタッチされているか確認する
        if (dataManager == null)
        {
            Debug.LogError("DataManager is not attached to any GameObject in the scene.");
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        PlayerData data = FindObjectOfType<DataManager>().LoadPlayerData();
        lifeText.text = "Life: " + data.life;
        coinsText.text = "Coins: " + data.coins;
    }
}
