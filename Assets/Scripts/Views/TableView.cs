using System.Collections.Generic;
using UnityEngine;

namespace Views
{
    public class TableView : MonoBehaviour
    {
        [SerializeField] private Transform  _slotsParent;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private Material   _greenSlotMaterial;
        [SerializeField] private Material   _blackSlotMaterial;
        [SerializeField] private Material   _redSlotMaterial;
        [SerializeField] private Material   _winningMaterial;
        [SerializeField] private Material   _losingMaterial;
        
        private List<SlotView> _slots;
        private int            _slotCount;
        private float          _slotSpacing;

        public void Initialize(int slotCount, float slotSpacing)
        {
            _slots       = new List<SlotView>();
            _slotCount   = slotCount;
            _slotSpacing = slotSpacing;
            
            CreateSlots();
        }

        public void ShowWinningSlot(int winningNumber)
        {
            int slotsCount = _slots.Count;
            int slotIndex  = _slots.FindIndex(slot => slot.SlotNumber == winningNumber);
            
            for (int i = 0; i < slotsCount; i++)
            {
                SlotView slot = _slots[i];
                
                if (i == slotIndex)
                {
                    slot.ChangeWinningColor();
                    slot.ShowWinning();
                }
                else
                {
                    slot.ChangeLosingColor();
                }
            }
        }
        
        public void ResetSlots()
        {
            foreach (SlotView slot in _slots)
            {
                slot.ChangeDefaultColor();
            }
        }
        
        private void CreateSlots()
        {
            int[] slotNumbers = new int[_slotCount];
            
            for (int i = 1; i < _slotCount + 1; i++)
            {
                slotNumbers[i - 1] = i;
            }

            for (int i = 0; i < slotNumbers.Length; i++)
            {
                Vector3  position = CalculateSlotPosition(i);
                Material material = i % 2 == 0 ? _blackSlotMaterial : _redSlotMaterial;

                GameObject slotObject = Instantiate(_slotPrefab, _slotsParent);
                slotObject.transform.localPosition = position;

                SlotView slotView = slotObject.GetComponent<SlotView>();
                slotView.Initialize(slotNumbers[i], material, _winningMaterial, _losingMaterial);

                _slots.Add(slotView);
            }
        }

        private Vector3 CalculateSlotPosition(int index)
        {
            int row    = (index) % 3;
            int column = (index) / 3;
            return new Vector3(-column * _slotSpacing, 0, -row * _slotSpacing);
        }
    }
}