using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] TextMeshProUGUI bestPointText;
    [SerializeField] GameObject PauseButton;
    [SerializeField] GameObject MenuGameOver;
    [SerializeField] private AudioSource audioSourceHurt;
    [SerializeField] private AudioSource audioSourceBuff;
    [SerializeField] private AudioSource audioSourcePoint;
    [SerializeField] GameObject explode;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] RuntimeAnimatorController flyAnimtor;
    [SerializeField] RuntimeAnimatorController runAnimtor;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] public Animator animator;
    private Rigidbody2D rig2D;
    private Renderer render;
    private float powerJump = 4.5f;
    private bool onGround;
    public bool isPlayerLive = true;
    public int point = 0;
    private int targetX = -7;
    public bool gameModeRun = true;
    public bool isStartGame = false;
    public bool isAnimationStart = true;
    private bool isFlyingUp = false;

    public static PlayerController instance;

    void Awake()
    {
        if (instance == null) instance = this;
        rig2D = GetComponent<Rigidbody2D>();
        SetText();
    }

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 5)
        {
            transform.position = new Vector3(-7, 5, 0);
        }
        if (transform.position.x < targetX && !isStartGame)
        {
            // Di chuyển player sang phải
            SetAnimationRun();
            transform.Translate(Vector3.right * 1.5f * Time.deltaTime);
        }
        else if(transform.position.x >= targetX && !isStartGame)
        {
            SetAnimationIdle();
            isAnimationStart = false;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        CheckDead();
    }

    public void Jump()
    {
        if (!isStartGame && !isAnimationStart)
        {
            animator.ResetTrigger("Idle");
            SetAnimationRun();
            isStartGame = true;
        }
        if (onGround && gameModeRun && isPlayerLive && isStartGame)
        {
            rig2D.AddForce(Vector2.up * powerJump, ForceMode2D.Impulse);
            animator.ResetTrigger("Run");
            SetAnimationJump();
            onGround = false;
        }
        else if (!gameModeRun && isPlayerLive && isStartGame && !isFlyingUp)
        {
            rig2D.velocity = Vector2.up * (powerJump + 0.5f );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra xem player có trên mặt đất không
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetAnimationRun();
            onGround = true;
        }
        if (collision.gameObject.CompareTag("Ground") && !gameModeRun && isPlayerLive)
        {
            audioSourceHurt.Play();
            HealthController.instance.playerHealth--;
            IntateExplode();
            rig2D.velocity = Vector2.up * powerJump;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            audioSourcePoint.Play();
            point++;
            if(point > GameManager.Instance.GetBestPoint())
            {
                GameManager.Instance.SetBestPoint(point);
            }
            SetText();
        }
        if (collision.gameObject.CompareTag("Robot"))
        {
            animator.runtimeAnimatorController = flyAnimtor;
            isFlyingUp = true;
            Destroy(collision.gameObject);
            boxCollider.size = new Vector2(0.5685916f, 1.196473f);
            boxCollider.offset = new Vector2(-0.1256378f, -0.0371899f);
            isStartGame = false;
            gameModeRun = false;
            rig2D.velocity = Vector2.up * (powerJump*2);
            animator.SetTrigger("StartRun");
        }
        if (collision.gameObject.CompareTag("Human"))
        {
            animator.runtimeAnimatorController = runAnimtor;
            Destroy(collision.gameObject);
            IntateExplode();
            boxCollider.size = new Vector2(0.5380859f, 1.104957f);
            boxCollider.offset = new Vector2(-0.2934179f, -0.1897174f);
            SetAnimationRun();
            gameModeRun = true;
            StartCoroutine(TimeBuffHeart());
        }
        if(collision.gameObject.CompareTag("DeadBlock"))
        {
            IntateExplode();
            audioSourceHurt.Play();
            collision.gameObject.SetActive(false);
            HealthController.instance.playerHealth--;
        }
    }

    private void SetText()
    {
        textMeshProUGUI.text = "Point: " + point;
    }

    private void SetTextEndGame()
    {
        pointText.text = "End Point: " + point;
        bestPointText.text = "Best Point: " + GameManager.Instance.GetBestPoint();
    }

    private void SetAnimationRun()
    {
        animator.SetTrigger("Run");
    }

    private void SetAnimationJump()
    {
        animator.SetTrigger("Jump");
    }

    private void SetAnimationDead()
    {
        animator.SetTrigger("Dead");
    }

    private void SetAnimationIdle()
    {
        animator.SetTrigger("Idle");
    }

    private void CheckDead()
    {
        if (HealthController.instance.playerHealth == 0)
        {
            if (!gameModeRun)
            {
                boxCollider.size = new Vector2(0.5380859f, 1.327401f);
                boxCollider.offset = new Vector2(-0.2934179f, -0.3009393f);
            }
            SetAnimationDead();
            isPlayerLive = false;
            PauseButton.SetActive(false);
            textMeshProUGUI.gameObject.SetActive(false);
            MenuGameOver.SetActive(true);
            SetTextEndGame();
        }
    }


    IEnumerator TimeBuffHeart()
    {
        StartCoroutine(BlinkEffect());
        int tempHeart = HealthController.instance.playerHealth;
        HealthController.instance.playerHealth = 100;
        render.material.color = Color.blue;
        audioSourceBuff.Play();
        yield return new WaitForSeconds(5);
        HealthController.instance.playerHealth = tempHeart;
        render.material.color = Color.white;
    }

    public void StartAnimationFly()
    {
        SetAnimationRun();
        isFlyingUp = false;
    }

    public void IntateExplode()
    {
        Instantiate(explode, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }

    IEnumerator BlinkEffect()
    {
        yield return new WaitForSeconds(4);
        render.material.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        render.material.color = Color.blue;
        yield return new WaitForSeconds(0.15f);
        render.material.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        render.material.color = Color.blue;
        yield return new WaitForSeconds(0.15f);
        render.material.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        render.material.color = Color.blue;
    }
}
