using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rig2D;
    private float powerJump = 4;
    private bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        if (onGround)
        {
            rig2D.AddForce(Vector2.up * powerJump, ForceMode2D.Impulse);
            onGround = false;
        }
    }

    // Kiểm tra xem player có trên mặt đất không
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
}
