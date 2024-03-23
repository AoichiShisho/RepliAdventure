using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField, Range(50f, 200f)] private float rotationSpeed = 100f;
    [SerializeField] private GameObject coinParticlePrefab;

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DataManager dataManager = FindObjectOfType<DataManager>();
            if (dataManager != null)
            {
                PlayerData data = dataManager.LoadPlayerData();
                data.coins++;
                dataManager.SavePlayerData(data);

                PlayerUIManager uiManager = FindObjectOfType<PlayerUIManager>();
                if (uiManager != null)
                {
                    uiManager.UpdateUI();
                }
                else
                {
                    Debug.LogError("PlayerUIManager not found in the scene.");
                }

                PlayCoinParticleEffect();
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("DataManager not found in the scene.");
            }
        }
    }

    private void PlayCoinParticleEffect()
    {
        if (coinParticlePrefab != null)
        {
            GameObject particleInstance = Instantiate(coinParticlePrefab, transform.position, Quaternion.identity);
            Destroy(particleInstance, particleInstance.GetComponent<ParticleSystem>().main.duration);
        }
        else
        {
            Debug.LogError("Coin particle prefab is not assigned.");
        }
    }
}
