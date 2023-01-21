using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtons : MonoBehaviour
{
    [SerializeField]
    private AudioType audioType;

    [Space]
    [SerializeField]
    private Sprite activeSprite;

    [SerializeField]
    private Sprite inactiveSprite;

    private AudioManager audioManager;
    private Button button;
    private Image btnImage;

    private bool isMuted;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        button = GetComponent<Button>();
        btnImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (button != null)
        {
            button.onClick.AddListener(ToggleSetting);
        }

        InitState();
    }

    private void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void InitState()
    {
        if (audioManager == null) return;
        if (btnImage == null) return;

        isMuted = false;
        switch (audioType)
        {
            case AudioType.SFX:
                isMuted = audioManager.SfxMuted;
                break;

            case AudioType.Music:
                isMuted = audioManager.MusicMuted;
                break;            
        }

        SetSprite();
    }

    private void ToggleSetting()
    {
        if (audioManager == null) return;

        audioManager.ToggleAudio(audioType);
        isMuted = !isMuted;

        SetSprite();
    }

    private void SetSprite()
    {
        if (isMuted)
        {
            btnImage.sprite = inactiveSprite;
        }
        else
        {
            btnImage.sprite = activeSprite;
        }
    }
}
