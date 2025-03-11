using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Views.Interfaces;

namespace Views
{
    public class WheelView : MonoBehaviour,  IWheelView
    {
        [SerializeField] private List<WheelSlotView> _wheelSegments;
        [SerializeField] private BallView            _ballView;
        
        private int         _wheelSegmentsCount;
        private int         _stopNumber;
        private float       _spinSpeed;
        private IEnumerator _spinWheelCoroutine;
        
        public void Initialize()
        {
            _wheelSegmentsCount = _wheelSegments.Count;
            
            foreach (WheelSlotView slot in _wheelSegments)
            {
                slot.Initialize();
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetStopNumber(bool isRandomize, int stopNumber)
        {
            _stopNumber = isRandomize ? Random.Range(0, _wheelSegmentsCount) : stopNumber;
        }
        
        public void SetSpinDuration(float spinSpeed)
        {
            _spinSpeed = spinSpeed;
        }
        
        public void Spin()
        {
            if (_spinWheelCoroutine == null)
            {
                _spinWheelCoroutine = SpinWheel();
                StartCoroutine(_spinWheelCoroutine);
            }
            
            ResetWheel();
            GameEvents.WheelStartSpin();
            SpinBall(_stopNumber);
        }

        private IEnumerator SpinWheel()
        {
            while (true)
            {
                transform.Rotate(Vector3.up, _spinSpeed * Time.deltaTime);
                yield return null;
            }
        }
        
        private void SpinBall(int stopNumber)
        {
            _ballView.SpinToSlot(stopNumber, _wheelSegmentsCount, () =>
            {
                SetSlotColors(stopNumber);
                GameEvents.WheelStopSpin();
                GameEvents.WheelSpinComplete(_wheelSegments[stopNumber].Value);
            });
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