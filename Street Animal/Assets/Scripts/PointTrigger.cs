using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointTrigger : MonoBehaviour
{
    public static PointTrigger instance;
    public int point = 0;
    public TextMeshProUGUI textMPro;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        setText();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            point++;
            setText();
        }
    }

    private void setText()
    {
        textMPro.text = "Point: " + point;
    }
}
