using System.Collections;
using Events;
using UnityEngine;

namespace Views
{
    public class WheelView : MonoBehaviour
    {
        private int   _wheelSegments;
        private int   _stopNumber;
        private float _spinDuration;
        
        public void Initialize(int wheelSegments)
        {
            _wheelSegments = wheelSegments;
        }
        
        public void SetStopNumber(int stopNumber)
        {
            _stopNumber = stopNumber < 0 ? Random.Range(0, _wheelSegments) : stopNumber;
        }
        
        public void SetSpinDuration(float spinDuration)
        {
            _spinDuration = spinDuration;
        }
        
        public void Spin()
        {
            GameEvents.WheelStartSpin();
            
            StartCoroutine(SpinToNumber(_stopNumber));
        }

        private IEnumerator SpinToNumber(int stopNumber)
        {
            float elapsed    = 0.0f;
            float startAngle = transform.rotation.eulerAngles.y;
            float endAngle   = 360.0f * 3 + (stopNumber * (360.0f / _wheelSegments));

            while (elapsed < _spinDuration)
            {
                elapsed += Time.deltaTime;
                
                float time  = elapsed / _spinDuration;
                float angle = Mathf.Lerp(startAngle, endAngle, time * time);
                
                transform.rotation = Quaternion.Euler(0, angle, 0);
                yield return null;
            }

            transform.rotation = Quaternion.Euler(0, endAngle, 0);
            
            GameEvents.WheelStopSpin();
        }
    }
}