using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public void RemoveAndRearrangeChips(ChipView chipView)
        {
            TableChipHolder holder = _chipHolders.Find(chipHolder => chipHolder.ChipType == chipView.ChipType);
    
            if (holder == null) return;

            List<ChipHolderView> chipHolderViews    = holder.GetChipHolderViews();
            ChipHolderView       chipHolderToRemove = chipHolderViews.Find(chipHolder => chipHolder.CurrentChip == chipView);
            
            if (chipHolderToRemove == null) return;
    
            chipHolderToRemove.RemoveChip();
            holder.DecreaseChipAmount();

            for (int i = 0; i < chipHolderViews.Count - 1; i++)
            {
                if (chipHolderViews[i].IsEmpty && !chipHolderViews[i + 1].IsEmpty)
                {
                    ChipView  chipToMove = chipHolderViews[i + 1].CurrentChip;
                    Transform newParent  = chipHolderViews[i].Position;
                    Transform chip       = chipToMove.transform;
                    
                    chip.SetParent(newParent);
                    chip.localPosition = Vector3.zero;
                    chip.localRotation = Quaternion.identity;

                    chipHolderViews[i].AddChip(chipToMove);
                    chipHolderViews[i + 1].RemoveChip();
                }
            }
            
            chipHolderViews[chipHolderViews.Count(x => !x.IsEmpty)].RemoveChip();
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
            if (_chipAmount >= _chipPositions.Count)
            {
                return;
            }
            
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

        public List<ChipHolderView> GetChipHolderViews()
        {
            List<ChipHolderView> chipHolderViews = new List<ChipHolderView>();
            
            foreach (var chipHolder in _chipPositions)
            {
                chipHolderViews.Add(chipHolder);
            }
            
            return chipHolderViews;
        }
    }

    [Serializable]
    public class ChipHolderView
    {
        [SerializeField] private Transform _position;
        public bool      IsEmpty  => CurrentChip == null;
        public Transform Position => _position;
        
        public ChipView CurrentChip { get; private set; }

        public void AddChip(ChipView chipView)
        {
            Transform chipTransform = chipView.transform;
            chipTransform.SetParent(_position);
            chipTransform.localPosition    = Vector3.zero;
            chipTransform.localEulerAngles = new Vector3(90f, 0f, 0f);
            
            CurrentChip = chipView;
        }
        
        public void RemoveChip()
        {
            if (CurrentChip == null) return;
            
            CurrentChip.transform.SetParent(_position.parent);
            CurrentChip = null;
        }
    }
}