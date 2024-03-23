using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : PlayerModel
{
    private void Awake()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.jumpHeight = 0f;
                playerMovement.isSpider = true;
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
