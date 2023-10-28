using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public static HealthController instance;
    public int playerHealth = 3;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite redHealth;
    [SerializeField] private Sprite blackHealth;
    [SerializeField] private Animator[] animator;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Animator anim in animator)
        {
            anim.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < playerHealth)
            {
                hearts[i].sprite = redHealth;
            }
            else
            {
                animator[i].enabled = true;
                hearts[i].sprite = blackHealth;
            }
        }
    }
}
