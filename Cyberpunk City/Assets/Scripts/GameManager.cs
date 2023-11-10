using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private const string bestPoint = "Best Point"; 
    
    private void Awake()
    {
        MakeInstance();
        IsGameStartedForTheFirstTime();
    }

    private void MakeInstance()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetBestPoint(int point)
    {
        PlayerPrefs.SetInt(bestPoint, point);
    }

    public int GetBestPoint()
    {
        return PlayerPrefs.GetInt(bestPoint);
    }

    private void IsGameStartedForTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt(bestPoint, 0);
            PlayerPrefs.SetInt("IsGameStartedForTheFirstTime", 0);
        }
    }
}
