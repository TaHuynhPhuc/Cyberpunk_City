using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float speed = 5f;

    private void Start()
    {

    }
    void Update()
    {
        if (PlayerController.instance.isPlayerLive)
        {
            MoveLeft();
        }
    }

    private void MoveLeft()
    {
        // Cho chạy liên tục qua trái
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
