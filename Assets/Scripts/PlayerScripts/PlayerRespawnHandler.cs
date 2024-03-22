using System.Collections;
using UnityEngine;

public class PlayerRespawnHandler : MonoBehaviour
{
    public PlayerData playerData;
    public Animator animator;
    public Canvas uiCanvas;
    private Coroutine transitionCoroutine;

    public FallTransition fallTransition;
    public Vector3 startPosition;
    public CameraController cameraController;

    public PlayerUIManager playerUIManager;
    private DataManager dataManager;

    [SerializeField, Range(-30f, 30f)] private float stageRange = 25f;
    private bool isFalling = false;

    private void Start()
    {
        dataManager = FindObjectOfType<DataManager>(); // DataManager のインスタンスを取得
        playerData = dataManager.LoadPlayerData(); // ゲーム開始時にプレイヤーデータを読み込む
    }

    void Update()
    {
        if (transform.position.y < stageRange && !isFalling)
        {
            isFalling = true;
            LoseLife();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hazard") && !isFalling)
        {
            isFalling = true;
            LoseLife();
        }
    }

    void LoseLife()
    {
        // データの読み込み
        playerData = dataManager.LoadPlayerData();
        
        if (playerData.life > 0)
        {
            playerData.life--;
            // データを保存
            dataManager.SavePlayerData(playerData);
        }
        uiCanvas.enabled = false;
        
        // UI の更新
        if (playerUIManager != null) playerUIManager.UpdateUI();
        
        if (playerData.life < 0)
        {
            // ゲームオーバー処理
        }
        else
        {
            if (transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine);
            }
            transitionCoroutine = StartCoroutine(TransitionSequence());
        }
    }

    IEnumerator TransitionSequence()
    {
        yield return fallTransition.FadeOut(fallTransition.fallMaterial, fallTransition.transitionTime);
        transform.position = startPosition;
        cameraController.ResetPosition();
        uiCanvas.enabled = true;
        yield return new WaitForSeconds(1);
        yield return fallTransition.FadeIn(fallTransition.fallMaterial, fallTransition.transitionTime);
        isFalling = false;
    }
}
