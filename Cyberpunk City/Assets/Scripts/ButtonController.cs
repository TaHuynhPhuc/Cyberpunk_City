using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject MusicMenu;
    [SerializeField] private GameObject CharacterMenu;
    [SerializeField] private GameObject profile;

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
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void SettingButton()
    {
        MusicMenu.SetActive(true);
    }

    public void OutButton()
    {
        MusicMenu.SetActive(false);
        CharacterMenu.SetActive(false);
        profile.SetActive(false);
    }

    public void CharacterButton() 
    {
        CharacterMenu.SetActive(true);
    }

    public void ProfileButton()
    {
        profile.SetActive(true);
    }
}
