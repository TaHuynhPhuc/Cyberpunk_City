﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
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
        SetAnimationRun();
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
            transform.Translate(Vector3.right * 2 * Time.deltaTime);
        }
        else if(transform.position.x >= targetX && !isStartGame)
        {
            SetAnimationIdle();
            isAnimationStart = false;
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
            onGround = false;
        }else if (!gameModeRun && isPlayerLive && isStartGame)
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
        if (collision.gameObject.CompareTag("Ground") && !gameModeRun && isPlayerLive)
        {
            audioSourceHurt.Play();
            HealthController.instance.playerHealth--;
            Instantiate(explode, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            rig2D.velocity = Vector2.up * powerJump;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            audioSourcePoint.Play();
            point++;
            SetText();
        }
        if (collision.gameObject.CompareTag("Bird"))
        {
            Destroy(collision.gameObject);
            animator.runtimeAnimatorController = flyAnimtor;
            boxCollider.size = new Vector2(0.4993038f, 0.4863439f);
            boxCollider.offset = new Vector2(-0.03840923f, -0.15363f);
            SetAnimationRun();
            gameModeRun = false;
            StartCoroutine(TimeBuffHeart());
        }
        if (collision.gameObject.CompareTag("Dog"))
        {
            Destroy(collision.gameObject);
            animator.runtimeAnimatorController = runAnimtor;
            boxCollider.size = new Vector2(0.9117303f, 0.6633778f);
            boxCollider.offset = new Vector2(-0.08961272f, -0.410507f);
            SetAnimationRun();
            gameModeRun = true;
            StartCoroutine(TimeBuffHeart());
        }
        if(collision.gameObject.CompareTag("DeadBlock"))
        {
            Instantiate(explode, new Vector3(transform.position.x + 0.5f, transform.position.y, 0), Quaternion.identity);
            audioSourceHurt.Play();
            collision.gameObject.SetActive(false);
            HealthController.instance.playerHealth--;
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

    private void SetAnimationIdle()
    {
        animator.SetTrigger("Idle");
    }

    private void CheckDead()
    {
        if (HealthController.instance.playerHealth == 0)
        {
            SetAnimationDead();
            isPlayerLive = false;
        }
    }


    IEnumerator TimeBuffHeart()
    {
        int tempHeart = HealthController.instance.playerHealth;
        HealthController.instance.playerHealth = 100;
        render.material.color = Color.red;
        audioSourceBuff.Play();
        Debug.Log("Buff");
        yield return new WaitForSeconds(5);
        HealthController.instance.playerHealth = tempHeart;
        render.material.color = Color.white;
        Debug.Log("EndBug");
    }
}
