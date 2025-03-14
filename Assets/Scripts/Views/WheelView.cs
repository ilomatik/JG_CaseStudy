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
        [SerializeField] private GameObject          _ballView;
        [SerializeField] private Transform           _ballParent;
        
        private int         _wheelSegmentsCount;
        private int         _stopNumber;
        private float       _spinSpeed;
        private IBallView   _ball;
        private IEnumerator _spinWheelCoroutine;
        
        public void Initialize()
        {
            _wheelSegmentsCount = _wheelSegments.Count;
            _ball               = _ballView.GetComponent<IBallView>();
            _ball.Initialize();
            _ball.SetBallMovement(5f, 5, 4.25f, 3.5f);
            _ball.SetWheel(_ballParent);
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
            _stopNumber = isRandomize ? Random.Range(1, _wheelSegmentsCount) : stopNumber;
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
                //transform.Rotate(Vector3.up, _spinSpeed * Time.deltaTime);
                yield return null;
            }
        }
        
        private void SpinBall(int stopNumber)
        {
            WheelSlotView slot = _wheelSegments.Find(x => x.Value == stopNumber);
            
            _ball.SpinToSlot(slot.transform, () =>
            {
                SetSlotColors(stopNumber);
                GameEvents.WheelSpinComplete(slot.Value);
                ParticleEvents.BallStopOnWheelSlotParticle(slot.transform.position);
            });
        }
        
        private void SetSlotColors(int stopNumber)
        {
            for (int i = 0; i < _wheelSegmentsCount; i++)
            {
                WheelSlotView slot = _wheelSegments[i];
            }
        }

        private void ResetWheel()
        {
            for (int i = 0; i < _wheelSegmentsCount; i++)
            {
                WheelSlotView slot = _wheelSegments[i];
            }
        }
    }
}