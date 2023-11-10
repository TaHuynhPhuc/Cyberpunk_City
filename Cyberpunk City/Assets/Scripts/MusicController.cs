using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource musicBackground;
    [SerializeField] private AudioSource musicGamePlay;
    [SerializeField] private AudioSource musicGameOver;

    private bool isGamePlayMusicActivated = false;
    private bool isGameOverMusicActivated = false;

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.isStartGame && !isGamePlayMusicActivated)
        {
            isGamePlayMusicActivated = true;
            musicBackground.Stop();
            musicGamePlay.Play();
        }
        if (!PlayerController.instance.isPlayerLive && !isGameOverMusicActivated)
        {
            isGameOverMusicActivated = true;
            musicGamePlay.Stop();
            musicGameOver.Play();
            Debug.Log("GameOver");
        }
    }
}
