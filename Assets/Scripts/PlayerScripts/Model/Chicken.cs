using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : PlayerModel
{
    private void Awake()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.jumpHeight = 5.0f;
                playerMovement.isSpider = false;
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

    public override void Action()
    {

    }
}
