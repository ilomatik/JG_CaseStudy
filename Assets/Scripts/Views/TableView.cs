using System.Collections.Generic;
using UnityEngine;

namespace Views
{
    public class TableView : MonoBehaviour
    {
        [SerializeField] private List<SlotView> _slots;
        
        private int _slotsCount;
        
        public void Initialize()
        {
            _slotsCount = _slots.Count;
            
            foreach (SlotView slot in _slots)
            {
                slot.Initialize();
            }
        }

        public void ShowWinningSlot(int winningNumber)
        {
            int slotIndex = _slots.FindIndex(slot => slot.SlotValue == winningNumber);
            
            for (int i = 0; i < _slotsCount; i++)
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
    }
}