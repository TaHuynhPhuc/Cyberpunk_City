using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody2D rig2D;
    private float powerJump = 4.5f;
    private bool onGround;
    private bool isDeadAnimationPlayed = false;

    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }

        if (collision.gameObject.CompareTag("DeadBlock"))
        {
            collision.gameObject.SetActive(false);
            HealthController.instance.playerHealth--;

            if (HealthController.instance.playerHealth == 0 && !isDeadAnimationPlayed)
            {
                animator.SetTrigger("Dead");
                isDeadAnimationPlayed = true;
                Debug.Log("Dead");
            }
        }
    }
}
