using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private AudioSource loopingAudioSource;
    [SerializeField] private AudioClip bgMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        sfxAudioSource.PlayOneShot(clip);
    }
    
    public void PlayLoopingSound(AudioClip clip)
    {
        if (clip == null) return;

        loopingAudioSource.loop = true;
        loopingAudioSource.clip = clip;
        loopingAudioSource.Play();
    }
    
    public void StopLoopingSound()
    {
        sfxAudioSource.Stop();
        sfxAudioSource.loop = false;
        sfxAudioSource.clip = null;
    }


    public void PlayBGMusic()
    {
        if (bgMusic == null) return;
        bgAudioSource.loop = true;
        bgAudioSource.clip = bgMusic;
        bgAudioSource.Play();
    }
}
