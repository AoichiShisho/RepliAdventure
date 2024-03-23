using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : PlayerModel
{
    public override void Action()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.jumpHeight = 5.0f;
            }
            else
            {
                Debug.LogError("PlayerMovementコンポーネントがPlayerオブジェクトに見つかりません。");
            }
        }
        else
        {
            Debug.LogError("Playerオブジェクトがシーン内に見つかりません。");
        }
    }
}
