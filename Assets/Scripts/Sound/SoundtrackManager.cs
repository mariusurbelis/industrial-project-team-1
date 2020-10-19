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
	[SerializeField] private GameObject audioBtnGameObject = null;

	public static AudioSource audioSource;

	public static AudioClip soundTrackSound;

	public AudioClip soundTrackSoundStub;

	[SerializeField] private Sprite soundOnImage;

	[SerializeField] private Sprite soundOffImage;

	[SerializeField] private Image buttonImage;

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();

		soundTrackSound = soundTrackSoundStub;

		audioBtn.onClick.AddListener(() => ToggleSound());

		buttonImage.GetComponent<Image>();

		audioSource.PlayOneShot(soundTrackSound);

		if (instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}
	private void ToggleSound()
	{
		if (audioSource.isPlaying)
		{
			audioSource.Stop();
			buttonImage.sprite = soundOffImage;
			Sound.soundOn = false;
		}
		else
		{
			audioSource.PlayOneShot(soundTrackSound);
			buttonImage.sprite = soundOnImage;
			Sound.soundOn = true;
		}
	}
}
