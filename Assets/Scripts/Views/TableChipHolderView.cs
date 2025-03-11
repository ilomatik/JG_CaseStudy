using System;
using System.Collections.Generic;
using Enums;
using TMPro;
using UnityEngine;
using Views.Chip;
using Views.Interfaces;

namespace Views
{
    public class TableChipHolderView : MonoBehaviour, IView, ITableChipHolderView
    {
        [SerializeField] private List<TableChipHolder> _chipHolders;

        public void Initialize()
        {
            
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void AddChipToHolder(ChipView chipView)
        {
            ChipType        chipType = chipView.ChipType;
            TableChipHolder holder   = _chipHolders.Find(chipHolder => chipHolder.ChipType == chipType);

            if (holder.HasEmptyPosition)
            {
                holder.AddChip(chipView);
            }
            else
            {
                holder.IncreaseChipAmount();
            }
        }
    }

    [Serializable]
    public class TableChipHolder
    {
        [SerializeField] private TextMeshPro          _chipAmountText;
        [SerializeField] private ChipType             _chipType;
        [SerializeField] private List<ChipHolderView> _chipPositions;
        
        public ChipType ChipType         => _chipType;
        public bool     HasEmptyPosition => _chipPositions.Exists(chipHolder => chipHolder.IsEmpty);
        
        private int _chipAmount;

        public void AddChip(ChipView chipView)
        {
            ChipHolderView holder = _chipPositions.Find(chipHolder => chipHolder.IsEmpty);
            holder.AddChip(chipView);
            IncreaseChipAmount();
        }
        
        public void RemoveLastChip()
        {
            ChipHolderView holder = _chipPositions.FindLast(chipHolder => !chipHolder.IsEmpty);
            holder.RemoveChip();
            DecreaseChipAmount();
        }
        
        public void IncreaseChipAmount()
        {
            _chipAmount++;
            _chipAmountText.text = _chipAmount.ToString();
        }
        
        public void DecreaseChipAmount()
        {
            _chipAmount--;
            _chipAmountText.text = _chipAmount.ToString();
        }
    }

    [Serializable]
    public class ChipHolderView
    {
        [SerializeField] private Transform _position;
        public bool IsEmpty  => _currentChip == null;
        
        private ChipView _currentChip;

        public void AddChip(ChipView chipView)
        {
            chipView.transform.SetParent(_position);
            chipView.transform.localPosition = Vector3.zero;
            _currentChip = chipView;
        }
        
        public void RemoveChip()
        {
            if (_currentChip == null) return;
            
            _currentChip.transform.SetParent(null);
            _currentChip = null;
        }
    }
}