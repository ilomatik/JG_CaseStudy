using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Views.Chip
{
    public class ChipView : MonoBehaviour
    {
        public bool     IsOnBetArea { get; private set; }
        public ChipType ChipType    { get; private set; }
        
        private List<int> _slotNumbers;

        public void Initialize(ChipType chipType)
        {
            ChipType = chipType;
        }
        
        public void SetChipMaterial(Material material)
        {
            GetComponent<MeshRenderer>().material = material;
        }
        
        public void SetOnBetArea(bool value)
        {
            IsOnBetArea = value;
        }
        
        public void SetSlotNumbers(List<int> slotNumbers)
        {
            _slotNumbers = slotNumbers;
        }
        
        public List<int> GetSlotNumbers()
        {
            List<int> slotNumbers = new List<int>();
            
            foreach (int slotNumber in _slotNumbers)
            {
                slotNumbers.Add(slotNumber);
            }

            return slotNumbers;
        }

        public void MoveTo(Vector3 position, Action onMoveComplete = null)
        {
            StartCoroutine(SmoothMove(position, 1.0f, onMoveComplete));
        }

        private IEnumerator SmoothMove(Vector3 targetPosition, float duration, Action onMoveComplete)
        {
            Vector3 startPosition = transform.position;
            float   elapsedTime   = 0f;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                elapsedTime       += Time.deltaTime;
                
                yield return null;
            }

            transform.position = targetPosition;
            onMoveComplete?.Invoke();
        }
    }
}