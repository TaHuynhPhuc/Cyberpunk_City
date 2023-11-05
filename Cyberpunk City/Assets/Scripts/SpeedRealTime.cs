using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRealTime : MonoBehaviour
{
    public static SpeedRealTime instance;
    private float baseSpeed = 5f; // Tốc độ ban đầu
    private float acceleration = 0.05f; // Tốc độ tăng dần

    public float currentSpeed; // Tốc độ hiện tại

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        currentSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed += acceleration * Time.deltaTime;
    }
}
