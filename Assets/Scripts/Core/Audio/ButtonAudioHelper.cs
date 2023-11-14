using Core.Audio;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class ButtonAudioHelper : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private AudioClip clip;

    private IGameAudioPlayer _audioPlayer;
    
    private void Start()
    {
        _audioPlayer = ServiceLocator.Instance.GetService<IGameAudioPlayer>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(PlaySound);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(PlaySound);
    }
    
    public void PlaySound()
    {
        _audioPlayer.PlayClip(clip);
    }
}
