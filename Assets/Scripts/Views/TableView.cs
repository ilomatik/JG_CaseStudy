using System.Collections.Generic;
using UnityEngine;
using Views.Interfaces;

namespace Views
{
    public class TableView : MonoBehaviour, IView, ITableView
    {
        [SerializeField] private Material         _winningMaterial;
        [SerializeField] private List<GameObject> _slotObjects;
        
        private Dictionary<int, ITableSlotView> _slots;
        private List<ITableSlotView>            _slotViews;
        private int                             _slotCount;

        public void Initialize()
        {
            _slots     = new Dictionary<int, ITableSlotView>();
            _slotViews = new List<ITableSlotView>();
            _slotCount = _slotObjects.Count;
            
            for (int i = 0; i < _slotCount; i++)
            {
                ITableSlotView slot = _slotObjects[i].GetComponent<ITableSlotView>();
                slot.Initialize(_winningMaterial);
                
                _slotViews.Add(slot);
                _slots    .Add(slot.SlotNumber, slot);
            }
        }

        public void ShowWinningSlot(int winningNumber)
        {
            foreach (KeyValuePair<int, ITableSlotView> slot in _slots)
            {
                ITableSlotView slotView = slot.Value;
                
                if (slot.Key == winningNumber)
                {
                    slotView.ChangeWinningColor();
                    slotView.ShowWinning();
                }
                else
                {
                    //slotView.ChangeLosingColor();
                }
            }
        }
        
        public void ResetSlots()
        {
            foreach (ITableSlotView slot in _slotViews)
            {
                slot.ChangeDefaultColor();
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
    }
}