using TMPro;
using UnityEngine;

namespace Views
{
    public class SlotView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _slotNumberText;
        [SerializeField] private int         _slotNumber;
        
        private MeshRenderer _meshRenderer;
        private Material     _defaultMaterial;
        private Material     _winningMaterial;
        private Material     _losingMaterial;

        public int SlotNumber => _slotNumber;
        
        public void Initialize(Material winningMaterial, Material losingMaterial)
        {
            _winningMaterial = winningMaterial;
            _losingMaterial  = losingMaterial;
            
            _meshRenderer    = GetComponent<MeshRenderer>();
            _defaultMaterial = _meshRenderer.material;
            
            _slotNumberText.text = SlotNumber.ToString();
        }
        
        public void ChangeWinningColor()
        {
            _meshRenderer.material = _winningMaterial;
        }
        
        public void ChangeLosingColor()
        {
            _meshRenderer.material = _losingMaterial;
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