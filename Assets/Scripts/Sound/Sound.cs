using UnityEngine;


public class Sound : MonoBehaviour
{

    
    //scream setup
    public static AudioClip screamSound;
    public AudioClip screamSoundStub;
    public static AudioSource screamAudioSource;

    //new round setup
    public static AudioClip newRoundSound;
    public AudioClip newRoundStub;
    public static AudioSource newRoundAudioSource;


    public static AudioClip bombSound;
    public AudioClip bombStub;
    public static AudioSource bombAudioSource;

    public static AudioClip correctSound;
    public AudioClip correctStub;
    public static AudioSource correctAudioSource;

    private void Awake()
    {
        screamAudioSource = GetComponent<AudioSource>();
        newRoundAudioSource = GetComponent<AudioSource>();
        bombAudioSource = GetComponent<AudioSource>();
        correctAudioSource = GetComponent<AudioSource>();

        screamSound = screamSoundStub;
        newRoundSound = newRoundStub;
        bombSound = bombStub;
        correctSound = correctStub;

        if (!FindObjectOfType<AudioListener>())
        {
            gameObject.AddComponent<AudioListener>();
        }
    }

    public static void PlayScreamSound(AudioClip clip)
    {
        screamAudioSource.PlayOneShot(clip);
    }

    public static void PlayNewRoundSound(AudioClip clip)
    {
        newRoundAudioSource.PlayOneShot(clip);
    }

    public static void PlayBombSound(AudioClip clip)
    {
        bombAudioSource.PlayOneShot(clip);
    }

    public static void PlayCorrectSound(AudioClip clip)
    {
        correctAudioSource.PlayOneShot(clip);
    }
}