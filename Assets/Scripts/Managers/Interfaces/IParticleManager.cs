using UnityEngine;

namespace Managers
{
    public interface IParticleManager
    {
        void PlayOnWinParticle(Vector3 position);
        void PlayOnNumberWinParticle(Vector3 position);
        void PlayOnBallStopOnWheelSlotParticle(Vector3 position);
        void PlayOnDropChipParticle(Vector3 position);
    }
}