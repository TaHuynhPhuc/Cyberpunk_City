using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Vector3 posisionDefault;
    private float halfLength;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        posisionDefault = transform.position;
        // Lấy chiều dài của mặt đâts chia đôi
        halfLength = boxCollider2D.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Nếu vị trí hiện tại đi qua trái quá chiều dài mặt đất thì đặt lại vị trí
        if (transform.position.x < posisionDefault.x - halfLength)
        {
            transform.position = posisionDefault;
        }
    }
}
