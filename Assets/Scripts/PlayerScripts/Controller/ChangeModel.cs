using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModel : MonoBehaviour
{
    public PlayerModel[] playerModels;
    private PlayerState playerState;
    private int currentPlayerModelIndex = 0;

    private void Start()
    {
        playerState = GetComponent<PlayerState>();
        ChangeCurrentModel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CycleEquipment(1);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleEquipment(-1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerState.CurrentPlayerModel?.Use();
        }
    }

    private void CycleEquipment(int direction)
    {
        currentPlayerModelIndex += direction;
        if (currentPlayerModelIndex >= playerModels.Length) currentPlayerModelIndex = 0;
        else if (currentPlayerModelIndex < 0) currentPlayerModelIndex = playerModels.Length - 1;

        ChangeCurrentModel();
    }

    private void ChangeCurrentModel()
    {
        if (playerModels.Length > 0)
        {
            playerState.PlayerModel(playerModels[currentPlayerModelIndex]);
        }
    }
}
