using UnityEngine;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioSource sfxAudioSource;
        [SerializeField] private AudioSource bgAudioSource;
        [SerializeField] private AudioSource loopingAudioSource;
        [SerializeField] private AudioClip bgMusic;
        [SerializeField] private AudioClip gameOverSound;

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
            if (sfxAudioSource == null) return;
            sfxAudioSource.PlayOneShot(clip);
        }
        public void PlayGameOverSound()
        {
            PlaySound(gameOverSound);
        }
        public void PlayLoopingSound(AudioClip clip)
        {
            if (clip == null) return;
            if (loopingAudioSource == null) return;
            
            loopingAudioSource.clip = clip;
            loopingAudioSource.loop = true;
            loopingAudioSource.Play();
        }
        public void StopLoopingSound()
        {
            if (loopingAudioSource == null) return;
            
            loopingAudioSource.Stop();
            loopingAudioSource.loop = false;
            loopingAudioSource.clip = null;
        }
        public void PlayBGMusic()
        {
            if (bgMusic == null) return;
            if (bgAudioSource == null) return;
            
            bgAudioSource.loop = true;
            bgAudioSource.clip = bgMusic;
            bgAudioSource.Play();
        }
        public void StopBGMusic()
        {
            if (bgAudioSource == null) return;
            
            bgAudioSource.Stop();
            bgAudioSource.clip = null;
        }
    }
}