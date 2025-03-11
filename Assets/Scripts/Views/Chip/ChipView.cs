using Enums;
using UnityEngine;

namespace Views.Chip
{
    public class ChipView : MonoBehaviour
    {
        [SerializeField] private ChipType _chipType;
        
        public bool     IsOnBetArea { get; private set; }
        public ChipType ChipType => _chipType;
        
        public void SetOnBetArea(bool value)
        {
            IsOnBetArea = value;
        }
    }
}