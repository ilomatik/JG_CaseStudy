using Enums;
using UnityEngine;

namespace Views.Chip
{
    public class ChipView : MonoBehaviour
    {
        public bool     IsOnBetArea { get; private set; }
        public ChipType ChipType    { get; private set; }
        
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
    }
}