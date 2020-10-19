using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public static AudioClip screamSound;

    public AudioClip screamSoundStub;

    public static AudioSource audioSource;

    public static bool soundOn = true;


    private void Awake()
    {
        if(soundOn)
		{
            audioSource = GetComponent<AudioSource>();

            screamSound = screamSoundStub;


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