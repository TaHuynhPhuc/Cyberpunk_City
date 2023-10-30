using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameObj;
    private int point;
    private int pointChallenge = 1;

    // Start is called before the first frame update
    private void Awake()
    {
        point = PointTrigger.instance.point;
    }

    // Update is called once per frame
    void Update()
    {
        if (point == pointChallenge)
        {
            gameObj.SetActive(true);
        }
    }
}
