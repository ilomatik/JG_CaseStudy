using System;
using UnityEngine;

namespace Views.Interfaces
{
    public interface IBallView
    {
        void Initialize();
        void SetBallMovement(float duration, int minRounds, float startRadius, float endRadius);
        void SetWheel(Transform wheel);
        void SpinToSlot(Transform finalSlot, Action onSpinComplete);
    }
}