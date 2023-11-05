using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject pauseButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButton();
        }
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        menuPause.SetActive(true);
    }

    public void ContinueButton()
    {
        menuPause.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
