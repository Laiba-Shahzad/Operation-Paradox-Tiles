using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource audioSource;

    [Header("Game Sounds")]
    public AudioClip startSound;
    public AudioClip teleportSound;
    public AudioClip gravitySound;
    public AudioClip timeWarpSound;
    public AudioClip slowSound;
    public AudioClip glitchSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    void Awake()
    {
        Instance = this;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}