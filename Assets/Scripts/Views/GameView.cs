using System;
using System.Collections.Generic;
using Constants;
using Enums;
using UnityEngine;
using Views.BetAreas;
using Views.Chip;
using Views.Interfaces;

namespace Views
{
    public class GameView : MonoBehaviour, IView
    {
        [Header("Game Objects")]
        [SerializeField] private GameObject         _wheelObject;
        [SerializeField] private GameObject         _tableObject;
        
        [Header("Chip Objects")]
        [SerializeField] private GameObject         _chipPrefab;
        [SerializeField] private GameObject         _tableChipHolder;
        [SerializeField] private List<ChipMaterial> _chipMaterials;
        
        [Header("Bet Areas")]
        [SerializeField] private List<BetArea>      _betAreas;

        private int                  _playerId;
        private IWheelView           _wheel;
        private ITableView           _table;
        private ITableChipHolderView _tableChipHolderView;

        public void Initialize()
        {
            _playerId = PlayerPrefs.GetInt(GameConstant.PLAYER_ID_COUNTER);
            
            _wheel               = _wheelObject.GetComponent<IWheelView>();
            _table               = _tableObject.GetComponent<ITableView>();
            _tableChipHolderView = _tableChipHolder.GetComponent<ITableChipHolderView>();

            _table.Initialize();
            _wheel.Initialize();
            _tableChipHolderView.Initialize();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetWheelSpinDuration(float duration)
        {
            _wheel.SetSpinDuration(duration);
        }
        
        public void SpawnChip(ChipType chipType, Vector3 position)
        {
            GameObject chip = Instantiate(_chipPrefab, position, Quaternion.identity);
            ChipView chipView = chip.GetComponent<ChipView>();
            chipView.Initialize(chipType);
            chipView.SetChipMaterial(_chipMaterials.Find(material => material.ChipType == chipType).Material);
        }
        
        public void SpinWheel(bool isRandom, int wheelStopValue)
        {
            _table.ResetSlots();
            _wheel.SetStopNumber(isRandom, wheelStopValue);
            _wheel.Spin();
        }
        
        public void ShowResult(int value)
        {
            _table.ShowWinningSlot(value);
        }
        
        public void PlaceChip(int betAreaId, GameObject chip)
        {
            BetArea betArea = _betAreas.Find(area => area.Id == betAreaId);
            betArea.PlaceChip(chip.GetComponent<ChipView>());
        }
        
        public void RemoveChip(int betAreaId, GameObject chip)
        {
            BetArea betArea = _betAreas.Find(area => area.Id == betAreaId);
            betArea.RemoveChip(chip.GetComponent<ChipView>());
        }
    }
    
    [Serializable]
    public class ChipMaterial
    {
        [SerializeField] private ChipType _chipType;
        [SerializeField] private Material _material;
        
        public ChipType ChipType => _chipType;
        public Material Material => _material;
    }
}