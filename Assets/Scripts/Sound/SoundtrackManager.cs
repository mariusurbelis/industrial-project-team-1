using UnityEngine;
using UnityEngine.UI;

public class SoundtrackManager : MonoBehaviour
{
	public static SoundtrackManager instance;

	private Button audioBtn = null;
	private Button soundEffectsBtn = null;

	public static AudioSource audioSource;
	public static AudioClip soundTrackSound;
	public AudioClip soundTrackSoundStub;

	[SerializeField] private Sprite soundOnImage = null;
	[SerializeField] private Sprite soundOffImage = null;
	private Image buttonImage = null;
	[SerializeField] private Sprite soundEffectsOnImage = null;
	[SerializeField] private Sprite soundEffectsOffImage = null;
	private Image buttonEffectsImage = null;

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
	}

	public void Initialize()
	{
		audioBtn = GameObject.Find("AudioBtn").GetComponent<Button>();
		soundEffectsBtn = GameObject.Find("SoundEffectsBtn").GetComponent<Button>();
		buttonImage = GameObject.Find("AudioImage").GetComponent<Image>();
		buttonEffectsImage = GameObject.Find("SoundEffectsImage").GetComponent<Image>();

		audioSource = GetComponent<AudioSource>();
		buttonImage.GetComponent<Image>();

		audioBtn.onClick.RemoveAllListeners();
		soundEffectsBtn.onClick.RemoveAllListeners();

		audioBtn.onClick.AddListener(() => ToggleSoundtrack());
		soundEffectsBtn.onClick.AddListener(() => ToggleSoundEffects());

		if (!soundTrackSound)
		{
			soundTrackSound = soundTrackSoundStub;
		}
		
		ResetButtonImages();
	}
	public void ToggleSoundtrack()
	{
		if (audioSource.isPlaying)
		{
			audioSource.Stop();
		}
		else
		{
			audioSource.Play();
		}

		ResetButtonImages();
	}

	public void ToggleSoundEffects()
	{
		Sound.soundOn = !Sound.soundOn;
		ResetButtonImages();
	}

	private void ResetButtonImages()
	{
		buttonImage.sprite = audioSource.isPlaying ? soundOnImage : soundOffImage;
		buttonEffectsImage.sprite = Sound.soundOn ? soundEffectsOnImage : soundEffectsOffImage;
	}
}
