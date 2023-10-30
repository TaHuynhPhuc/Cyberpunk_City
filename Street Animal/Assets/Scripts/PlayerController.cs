using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Animator animator;
    private Rigidbody2D rig2D;
    private float powerJump = 4.5f;
    private bool onGround;
    public bool isPlayerLive = true;
    public int point = 0;

    public static PlayerController instance;

    void Awake()
    {
        if (instance == null) instance = this;
        rig2D = GetComponent<Rigidbody2D>();
        setText();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra xem player có trên mặt đất không
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        if (collision.gameObject.CompareTag("DeadBlock"))
        {
            // Phá hủy block
            collision.gameObject.SetActive(false);
            HealthController.instance.playerHealth--;
            if (HealthController.instance.playerHealth == 0)
            {
                animator.SetTrigger("Dead");
                isPlayerLive = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            point++;
            setText();
        }
    }

    private void setText()
    {
        textMeshProUGUI.text = "Point: " + point;
    }
}
