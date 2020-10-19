using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public static AudioClip screamSound;
    public static AudioClip timerTickSound;

    public AudioClip screamSoundStub;
    public AudioClip timerTickSoundStub;


    public static AudioSource audioSource;

    public static bool soundOn = true;


    private void Awake()
    {
        if(soundOn)
		{
            audioSource = GetComponent<AudioSource>();

            screamSound = screamSoundStub;
            timerTickSound = timerTickSoundStub;


            if (!FindObjectOfType<AudioListener>())
            {
                gameObject.AddComponent<AudioListener>();
            }
        }
    }

    public static void PlaySound(AudioClip clip)
    {
		if (soundOn) audioSource.PlayOneShot(clip);
    }

	
}