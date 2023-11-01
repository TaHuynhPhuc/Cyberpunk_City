using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] RuntimeAnimatorController Bird1Animtor;
    [SerializeField] RuntimeAnimatorController Dog1Animtor;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Animator animator;
    private Rigidbody2D rig2D;
    private float powerJump = 4.5f;
    private bool onGround;
    public bool isPlayerLive = true;
    public int point = 0;
    public bool gameModeRun = true;

    public static PlayerController instance;

    void Awake()
    {
        if (instance == null) instance = this;
        rig2D = GetComponent<Rigidbody2D>();
        SetText();
    }

    private void Start()
    {
        SetAnimationRun();
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
            if (gameModeRun)
            {
                onGround = false;
            }
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
                SetAnimationDead();
                isPlayerLive = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            point++;
            SetText();
        }
        if (collision.gameObject.CompareTag("Bird"))
        {
            animator.runtimeAnimatorController = Bird1Animtor;
            boxCollider.size = new Vector2(0, -0.1404536f);
            boxCollider.offset = new Vector2(0.4747639f, 0.3668645f);
            SetAnimationRun();
            gameModeRun = false;
        }
    }

    private void SetText()
    {
        textMeshProUGUI.text = "Point: " + point;
    }

    private void SetAnimationRun()
    {
        animator.SetTrigger("Run");
    }

    private void SetAnimationDead()
    {
        animator.SetTrigger("Dead");
    }
}
