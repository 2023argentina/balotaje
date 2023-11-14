using UnityEngine;

namespace Core.Audio
{
    public interface IGameAudioPlayer
    {
        public void PlayClip(AudioClip clip);
    }
    
    public class AudioManager: MonoBehaviour, IGameAudioPlayer
    {
        [SerializeField] private AudioSource audioSource;
        
        public void PlayClip(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}