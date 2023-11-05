using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource musicBackground;
    [SerializeField] private AudioSource musicGamePlay;

    private bool isGamePlayMusicActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        musicBackground.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.isStartGame && !isGamePlayMusicActivated)
        {
            isGamePlayMusicActivated = true;
            musicBackground.Stop();
            musicGamePlay.Play();
        }
    }
}
