using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForMenu : MonoBehaviour
{
    private float speed = 5f;

    void Update()
    {
       MoveLeft();
    }

    private void MoveLeft()
    {
        // Cho chạy liên tục qua trái
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
