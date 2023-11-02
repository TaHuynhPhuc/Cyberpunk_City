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
        if (transform.position.y > 5)
        {
            transform.position = new Vector3(-7, 5, 0);
        }
    }

    public void Jump()
    {
        if (onGround && gameModeRun)
        {
            rig2D.AddForce(Vector2.up * powerJump, ForceMode2D.Impulse);
            onGround = false;
        }else if (!gameModeRun)
        {
            rig2D.velocity = Vector2.up * powerJump;
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
        if (collision.gameObject.CompareTag("Ground") && !gameModeRun)
        {
            HealthController.instance.playerHealth--;
            rig2D.velocity = Vector2.up * powerJump;
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
            Destroy(collision.gameObject);
            animator.runtimeAnimatorController = Bird1Animtor;
            boxCollider.size = new Vector2(0.5548458f, 0.3641517f);
            boxCollider.offset = new Vector2(-0.01063824f, -0.09253395f);
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
