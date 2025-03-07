using System;
using System.Collections;
using UnityEngine;

namespace Views
{
    public class BallView : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _spinDuration;

        public void SpinToSlot(int slotIndex, int totalSlots, Action onSpinComplete)
        {
            StartCoroutine(SpinToSlotCoroutine(slotIndex, totalSlots, onSpinComplete));
        }

        private IEnumerator SpinToSlotCoroutine(int slotIndex, int totalSlots, Action onSpinComplete)
        {
            float elapsed = 0.0f;
            float startAngle = transform.localRotation.eulerAngles.y;
            float endAngle = 360.0f * 3 + (slotIndex * (360.0f / totalSlots));

            while (elapsed < _spinDuration)
            {
                elapsed += Time.deltaTime;

                float time = elapsed / _spinDuration;
                float angle = Mathf.Lerp(startAngle, endAngle, time * time);

                float radians = angle * Mathf.Deg2Rad;
                Vector3 position = new Vector3(Mathf.Sin(radians) * _radius, 0, Mathf.Cos(radians) * _radius);

                transform.localPosition = position;
                transform.localRotation = Quaternion.Euler(0, angle, 0);

                yield return null;
            }

            float finalRadians = endAngle * Mathf.Deg2Rad;
            Vector3 finalPosition = new Vector3(Mathf.Sin(finalRadians) * _radius, 0, Mathf.Cos(finalRadians) * _radius);

            transform.localPosition = finalPosition;
            transform.localRotation = Quaternion.Euler(0, endAngle, 0);
            onSpinComplete?.Invoke();
        }
    }
}