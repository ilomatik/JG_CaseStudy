using UnityEngine;
using Views.Interfaces;

namespace Views
{
    public class SlotView : MonoBehaviour, ITableSlotView
    {
        [SerializeField] private int         _slotNumber;
        
        private MeshRenderer _meshRenderer;
        private Material     _defaultMaterial;
        private Material     _winningMaterial;
        
        public int           SlotNumber { get; private set; }
        
        public void Initialize(Material winningMaterial)
        {
            _winningMaterial     = winningMaterial;
            _meshRenderer        = GetComponent<MeshRenderer>();
            _defaultMaterial     = _meshRenderer.material;
            SlotNumber           = _slotNumber;
        }

        public void ChangeWinningColor()
        {
            _meshRenderer.material = _winningMaterial;
        }
        
        public void ChangeDefaultColor()
        {
            _meshRenderer.material = _defaultMaterial;
        }

        public void ShowWinning()
        {
            Debug.Log($"Slot{SlotNumber} is winning!");
        }
    }
}