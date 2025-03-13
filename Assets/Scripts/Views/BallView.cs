using System;
using System.Collections;
using UnityEngine;
using Views.Interfaces;

namespace Views
{
    public class BallView : MonoBehaviour, IBallView
    {
        private float     _startRadius;
        private float     _endRadius;
        private float     _duration;
        private float     _currentAngle; 
        private float     _finalAngle;
        private int       _minRounds;
        private Transform _wheel;
        private Transform _finalSlot;
        private Action    _onSpinComplete;

        public void Initialize()
        {
        }
        
        public void SetBallMovement(float duration, int minRounds, float startRadius, float endRadius)
        {
            _duration    = duration;
            _minRounds   = minRounds;
            _startRadius = startRadius;
            _endRadius   = endRadius;
        }

        public void SetWheel(Transform wheel)
        {
            _wheel = wheel;
        }

        public void SpinToSlot(Transform finalSlot, Action onSpinComplete)
        {
            _finalSlot       = finalSlot;
            _onSpinComplete  = onSpinComplete;
            
            Vector3 direction = finalSlot.position - _wheel.position;
            _finalAngle       = Mathf.Atan2(direction.z, direction.x);
            
            StartCoroutine(SpinBall());
        }

        private IEnumerator SpinBall()
        {
            float elapsed         = 0f;
            float startRadiusTemp = _startRadius; 

            while (elapsed < _duration)
            {
                elapsed += Time.deltaTime;

                float time   = elapsed / _duration;
                float easedT = 1f - Mathf.Pow(1f - time, 3f);

                _currentAngle = Mathf.Lerp(0, _minRounds * 2 * Mathf.PI + _finalAngle, easedT);

                float   radius = Mathf.Lerp(startRadiusTemp, _endRadius, easedT);
                Vector3 newPos = _wheel.position + new Vector3(Mathf.Cos(_currentAngle), 0, Mathf.Sin(_currentAngle)) * radius;
                transform.position = newPos;

                yield return null;
            }

            _onSpinComplete?.Invoke();
        }
    }
}