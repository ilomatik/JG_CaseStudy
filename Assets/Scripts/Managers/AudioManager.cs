using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip   _onChipDrop;
        [SerializeField] private AudioClip   _onWin;
        [SerializeField] private AudioClip   _onLose;
        [SerializeField] private AudioClip   _onButtonClick;

        public void PlayOnChipDrop()
        {
            _audioSource.PlayOneShot(_onChipDrop);
        }
        
        public void PlayOnWin()
        {
            _audioSource.PlayOneShot(_onWin);
        }
        
        public void PlayOnLose()
        {
            _audioSource.PlayOneShot(_onLose);
        }
        
        public void PlayOnButtonClick()
        {
            _audioSource.PlayOneShot(_onButtonClick);
        }
    }
}