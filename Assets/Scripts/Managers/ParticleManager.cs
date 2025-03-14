using UnityEngine;

namespace Managers
{
    public class ParticleManager : MonoBehaviour, IParticleManager
    {
        [SerializeField] private GameObject _onBallStopOnWheelSlotParticle;
        [SerializeField] private GameObject _onNumberWinParticle;
        [SerializeField] private GameObject _onWinParticle;
        [SerializeField] private GameObject _onDropChipParticle;
        
        public void PlayOnDropChipParticle(Vector3 position)
        {
            Instantiate(_onDropChipParticle, position, Quaternion.identity);
        }
        
        public void PlayOnWinParticle(Vector3 position)
        {
            Instantiate(_onWinParticle, position, Quaternion.identity);
        }

        public void PlayOnNumberWinParticle(Vector3 position)
        {
            Instantiate(_onNumberWinParticle, position, Quaternion.identity);
        }
        
        public void PlayOnBallStopOnWheelSlotParticle(Vector3 position)
        {
            Instantiate(_onBallStopOnWheelSlotParticle, position, Quaternion.identity);
        }
    }
}