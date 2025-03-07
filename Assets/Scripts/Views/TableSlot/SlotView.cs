using TMPro;
using UnityEngine;

namespace Views
{
    public class SlotView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _slotNumberText;
        
        private MeshRenderer _meshRenderer;
        private Material     _defaultMaterial;
        private Material     _winningMaterial;
        private Material     _losingMaterial;

        public int SlotNumber { get; private set; }
        
        public void Initialize(int slotNumber, Material defaultMaterial, Material winningMaterial, Material losingMaterial)
        {
            SlotNumber       = slotNumber;
            _defaultMaterial = defaultMaterial;
            _winningMaterial = winningMaterial;
            _losingMaterial  = losingMaterial;
            
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material = _defaultMaterial;
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