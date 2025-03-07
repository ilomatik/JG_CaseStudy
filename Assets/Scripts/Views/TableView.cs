using System.Collections.Generic;
using UnityEngine;

namespace Views
{
    public class TableView : MonoBehaviour
    {
        [SerializeField] private Material       _winningMaterial;
        [SerializeField] private Material       _losingMaterial;
        [SerializeField] private List<SlotView> _slotViews;
        
        private Dictionary<int, SlotView> _slots;
        private int            _slotCount;

        public void Initialize()
        {
            _slots     = new Dictionary<int, SlotView>();
            _slotCount = _slotViews.Count;
            
            for (int i = 0; i < _slotCount; i++)
            {
                SlotView slot = _slotViews[i];
                slot.Initialize(_winningMaterial, _losingMaterial);
                _slots.Add(slot.SlotNumber, slot);
            }
        }

        public void ShowWinningSlot(int winningNumber)
        {
            foreach (KeyValuePair<int, SlotView> slot in _slots)
            {
                SlotView slotView = slot.Value;
                
                if (slot.Key == winningNumber)
                {
                    slotView.ChangeWinningColor();
                    slotView.ShowWinning();
                }
                else
                {
                    slotView.ChangeLosingColor();
                }
            }
        }
        
        public void ResetSlots()
        {
            foreach (SlotView slot in _slotViews)
            {
                slot.ChangeDefaultColor();
            }
        }
    }
}