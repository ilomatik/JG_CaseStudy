using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Views
{
    public class WheelView : MonoBehaviour
    {
        [SerializeField] private List<WheelSlotView> _wheelSegments;
        
        private int   _wheelSegmentsCount;
        private int   _stopNumber;
        private float _spinDuration;
        
        public void Initialize()
        {
            _wheelSegmentsCount = _wheelSegments.Count;
            
            foreach (WheelSlotView slot in _wheelSegments)
            {
                slot.Initialize();
            }
        }
        
        public void SetStopNumber(int stopNumber)
        {
            _stopNumber = stopNumber < 0 ? Random.Range(0, _wheelSegmentsCount) : stopNumber;
        }
        
        public void SetSpinDuration(float spinDuration)
        {
            _spinDuration = spinDuration;
        }
        
        public void Spin()
        {
            ResetWheel();
            GameEvents.WheelStartSpin();
            
            StartCoroutine(SpinToNumber(_stopNumber));
        }

        private IEnumerator SpinToNumber(int stopNumber)
        {
            float elapsed    = 0.0f;
            float startAngle = transform.rotation.eulerAngles.y;
            float endAngle   = 360.0f * 3 + (stopNumber * (360.0f / _wheelSegmentsCount));

            while (elapsed < _spinDuration)
            {
                elapsed += Time.deltaTime;
                
                float time  = elapsed / _spinDuration;
                float angle = Mathf.Lerp(startAngle, endAngle, time * time);
                
                transform.rotation = Quaternion.Euler(0, angle, 0);
                yield return null;
            }

            transform.rotation = Quaternion.Euler(0, endAngle, 0);
            
            SetSlotColors(stopNumber);
            GameEvents.WheelStopSpin();
            GameEvents.WheelSpinComplete(_wheelSegments[stopNumber].Value);
        }
        
        private void SetSlotColors(int stopNumber)
        {
            for (int i = 0; i < _wheelSegmentsCount; i++)
            {
                WheelSlotView slot = _wheelSegments[i];
                
                if (i == stopNumber)
                {
                    slot.ChangeWinningColor();
                }
                else
                {
                    slot.ChangeLosingColor();
                }
            }
        }

        private void ResetWheel()
        {
            for (int i = 0; i < _wheelSegmentsCount; i++)
            {
                WheelSlotView slot = _wheelSegments[i];
                slot.ChangeDefaultColor();
            }
        }
    }
}