using System.Collections.Generic;
using Constants;
using Enums;
using Events;
using UnityEngine;
using Views.Chip;
using Views.Interfaces;

namespace Views.BetAreas
{
    public class BetArea : MonoBehaviour
    {
        [SerializeField] private int              _id;
        [SerializeField] private BetType          _betType;
        [SerializeField] private List<GameObject> _slotObjects;
        
        public int     Id      => _id;
        public BetType BetType => _betType;
        
        private List<ChipView>       _placedChips;
        private List<ITableSlotView> _slots;

        public void Initialize()
        {
            _placedChips = new List<ChipView>();
            
            _slots = new List<ITableSlotView>();
            
            foreach (GameObject slotObject in _slotObjects)
            {
                _slots.Add(slotObject.GetComponent<ITableSlotView>());
            }
        }

        public bool IsBetWinning(int winningNumber)
        {
            foreach (ITableSlotView slot in _slots)
            {
                if (slot.SlotNumber == winningNumber)
                {
                    slot.ChangeWinningColor();
                    slot.ShowWinning();
                    Debug.Log($"{BetType} is winning!");
                    return true;
                }
            }

            Debug.Log($"{BetType} is losing!");
            return false;
        }
        
        public void PlaceChip(ChipView chip)
        {
            chip.transform.SetParent(transform);
            chip.transform.position = GetPlacementPosition();
            chip.SetOnBetArea(true);
            
            _placedChips.Add(chip.transform.GetComponent<ChipView>());
            
            BetEvents.PlaceBet(PlayerPrefs.GetInt(GameConstant.PLAYER_ID_COUNTER), _betType, GetSlotNumbers(), chip.ChipType);
        }
        
        public void RemoveChip(ChipView chip)
        {
            _placedChips.Remove(chip);
            Destroy(chip.gameObject);
        }

        private Vector3 GetPlacementPosition()
        {
            return transform.position + Vector3.up * 0.2f * _placedChips.Count;
        }
        
        private List<int> GetSlotNumbers()
        {
            List<int> slotNumbers = new List<int>();
            
            foreach (ITableSlotView slot in _slots)
            {
                slotNumbers.Add(slot.SlotNumber);
            }

            return slotNumbers;
        }
    }
}