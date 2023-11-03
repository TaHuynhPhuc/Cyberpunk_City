using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadBlock") || collision.gameObject.CompareTag("HammerBlock"))
        {
            Transform hammerUpper = collision.gameObject.transform.Find("HammerUpper");
            Transform HammerLower = collision.gameObject.transform.Find("HammerLower");
            if (hammerUpper != null && HammerLower != null)
            {
                hammerUpper.gameObject.SetActive(true);
                HammerLower.gameObject.SetActive(true);
            }
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Bird") || collision.gameObject.CompareTag("Explode") || collision.gameObject.CompareTag("Dog"))
        {
            Destroy(collision.gameObject);
        }
    }
}
