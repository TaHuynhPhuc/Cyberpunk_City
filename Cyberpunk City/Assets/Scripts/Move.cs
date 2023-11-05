using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    void Update()
    {
        if (PlayerController.instance.isPlayerLive && PlayerController.instance.isStartGame)
        {
            MoveLeft();
        }
    }

    private void MoveLeft()
    {
        // Cho chạy liên tục qua trái
        transform.position += Vector3.left * SpeedRealTime.instance.currentSpeed * Time.deltaTime;
    }
}
