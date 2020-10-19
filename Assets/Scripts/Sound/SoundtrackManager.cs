﻿using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SoundtrackManager : MonoBehaviour
{
    public static SoundtrackManager instance;

    [SerializeField] private Button audioBtn = null;
    [SerializeField] private Button soundEffectsBtn = null;

    public static AudioSource audioSource;
    public static AudioClip soundTrackSound;
    public AudioClip soundTrackSoundStub;

    [SerializeField] private Sprite soundOnImage = null;
    [SerializeField] private Sprite soundOffImage = null;
    [SerializeField] private Image buttonImage = null;
    [SerializeField] private Sprite soundEffectsOnImage = null;
    [SerializeField] private Sprite soundEffectsOffImage = null;
    [SerializeField] private Image buttonEffectsImage = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        soundTrackSound = soundTrackSoundStub;

        audioBtn.onClick.AddListener(() => ToggleSoundtrack());

        soundEffectsBtn.onClick.AddListener(() => ToggleSoundEffects());

        buttonImage.GetComponent<Image>();

    }

    void Start()
    {
        audioSource.PlayOneShot(soundTrackSound);
        buttonImage.sprite = soundOnImage;
        Sound.soundOn = true;
    }
    private void ToggleSoundtrack()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            buttonImage.sprite = soundOffImage;
        }
        else
        {
            audioSource.PlayOneShot(soundTrackSound);
            buttonImage.sprite = soundOnImage;
        }
    }

    private void ToggleSoundEffects()
    {
        Sound.soundOn = !Sound.soundOn;
        buttonEffectsImage.sprite = Sound.soundOn ? soundEffectsOnImage : soundEffectsOffImage;
    }
}
