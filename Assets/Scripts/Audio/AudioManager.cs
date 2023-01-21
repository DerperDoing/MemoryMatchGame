using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    Music,
    SFX
}

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource;

    [Space]
    [SerializeField]
    private AudioSource sfxSource;

    [SerializeField]
    private AudioClip cardSelectClip;

    [SerializeField]
    private AudioClip cardsMatchedClip;

    [SerializeField]
    private AudioClip levelCompletedClip;

    [SerializeField]
    private AudioClip levelFailedClip;

    private bool sfxMuted;
    private bool musicMuted;

    public bool SfxMuted => sfxMuted;
    public bool MusicMuted => musicMuted;

    private void Awake()
    {
        musicMuted = false;
        sfxMuted = false;
    }

    private void OnEnable()
    {
        EventAggregator.changeGameStateEvent += OnGameStateChange;
        EventAggregator.cardSelectedEvent += OnCardSelect;
        EventAggregator.matchedEvent += OnCardsMatched;        
    }

    private void OnDisable()
    {
        EventAggregator.changeGameStateEvent -= OnGameStateChange;
        EventAggregator.cardSelectedEvent -= OnCardSelect;
        EventAggregator.matchedEvent -= OnCardsMatched;
    }

    private void OnGameStateChange(GameStates newState)
    {
        switch (newState)
        {
            //case GameStates.Home:
            //    sfxSource.Stop();
            //    break;                        

            case GameStates.LevelCompleted:
                PlaySfx(levelCompletedClip);
                break;

            case GameStates.LevelFailed:
                PlaySfx(levelFailedClip);
                break;            

            default:
                sfxSource.Stop();
                break;
        }
    }

    private void OnCardSelect()
    {
        PlaySfx(cardSelectClip);
    }

    private void OnCardsMatched(bool matched)
    {
        if (matched) PlaySfx(cardsMatchedClip);        
    }

    private void PlaySfx(AudioClip clip)
    {
        if (sfxMuted) return;
        if (clip == null) return;        

        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void ToggleAudio(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.Music:
                ToggleMusic();
                break;

            case AudioType.SFX:
                ToggleSfx();
                break;            
        }
    }

    private void ToggleSfx()
    {
        sfxMuted = !sfxMuted;
        sfxSource.enabled = sfxMuted;        
    }

    private void ToggleMusic()
    {
        musicMuted = !musicMuted;
        if (musicMuted)
        {
            musicSource.Stop();
        }
        else
        {
            musicSource.Play();
        }
    }    
}
