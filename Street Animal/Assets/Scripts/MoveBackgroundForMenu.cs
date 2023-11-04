using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackgroundForMenu : MonoBehaviour
{
    private Material material;
    private float distance;

    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Repeat();
    }

    private void Repeat()
    {
        distance += Time.deltaTime * speed;
        material.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}
