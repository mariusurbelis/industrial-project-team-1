using UnityEngine;

namespace SquirrelForest
{
    public class Sound : MonoBehaviour
    {
        public static AudioClip dieSound;

        public AudioClip dieSoundStub;

        public static AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            dieSound = dieSoundStub;

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
}