using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public PlayerModel CurrentPlayerModel { get; private set; }

    public void PlayerModel(PlayerModel newPlayerModel)
    {
        if (CurrentPlayerModel != null)
        {
            Destroy(CurrentPlayerModel.gameObject);
        }

        Vector3 spawnPosition = transform.TransformPoint(new Vector3(0, 0, 0));

        CurrentPlayerModel = Instantiate(newPlayerModel, spawnPosition, Quaternion.identity, transform);
    }
}
