using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{

    public static AudioClip timerTickSound;


    
    //scream setup
    public static AudioClip screamSound;
    public AudioClip screamSoundStub;

    public static AudioSource screamAudioSource;

    //new round setup
    public static AudioClip newRoundSound;
    public AudioClip newRoundStub;
    public static AudioSource newRoundAudioSource;

    public AudioClip timerTickSoundStub;



    public static AudioClip bombSound;
    public AudioClip bombStub;
    public static AudioSource bombAudioSource;

    public static AudioClip correctSound;
    public AudioClip correctStub;
    public static AudioSource correctAudioSource;

    public static bool soundOn = true;


    private void Awake()
    {

        if(soundOn)
		{
            //audioSource = GetComponent<AudioSource>();
            
            //added from f-add-sound-effects
            screamAudioSource = GetComponent<AudioSource>();
            newRoundAudioSource = GetComponent<AudioSource>();
            bombAudioSource = GetComponent<AudioSource>();
            correctAudioSource = GetComponent<AudioSource>();

            screamSound = screamSoundStub;
            newRoundSound = newRoundStub;
            bombSound = bombStub;
            correctSound = correctStub;
            
            
            timerTickSound = timerTickSoundStub;


            if (!FindObjectOfType<AudioListener>())
            {
                gameObject.AddComponent<AudioListener>();
            }
        }
    }

    public static void PlayScreamSound(AudioClip clip)
    {
        if (soundOn) screamAudioSource.PlayOneShot(clip);
    }

    public static void PlayNewRoundSound(AudioClip clip)
    {
        if (soundOn) newRoundAudioSource.PlayOneShot(clip);
    }

    public static void PlayBombSound(AudioClip clip)
    {
       if (soundOn) bombAudioSource.PlayOneShot(clip);
    }

    public static void PlayCorrectSound(AudioClip clip)
    {
    if (soundOn) correctAudioSource.PlayOneShot(clip);
    }

	public static void PlaySound(AudioClip clip)
	{
        if (soundOn) correctAudioSource.PlayOneShot(clip);
    }
}