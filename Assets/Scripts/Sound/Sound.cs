using UnityEngine;


public class Sound : MonoBehaviour
{
    public static AudioClip screamSound;

    public AudioClip screamSoundStub;

    public static AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        screamSound = screamSoundStub;

        if (!FindObjectOfType<AudioListener>())
        {
            gameObject.AddComponent<AudioListener>();
        }
    }

    public static void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}