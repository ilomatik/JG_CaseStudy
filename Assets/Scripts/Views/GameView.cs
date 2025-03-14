using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Constants;
using Enums;
using Events;
using UnityEngine;
using Views.BetAreas;
using Views.Chip;
using Views.Interfaces;

namespace Views
{
    public class GameView : MonoBehaviour, IView, IGameView
    {
        [Header("Game Objects")]
        [SerializeField] private GameObject         _wheelObject;
        [SerializeField] private GameObject         _tableObject;
        [SerializeField] private Transform          _chipMoveTarget;
        
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
        private List<ChipView>       _placedChips;

        public void Initialize()
        {
            _playerId = PlayerPrefs.GetInt(GameConstant.PLAYER_ID_COUNTER);
            
            _wheel               = _wheelObject.GetComponent<IWheelView>();
            _table               = _tableObject.GetComponent<ITableView>();
            _tableChipHolderView = _tableChipHolder.GetComponent<ITableChipHolderView>();
            _placedChips         = new List<ChipView>();

            _table.Initialize();
            _wheel.Initialize();
            _tableChipHolderView.Initialize();
        }
        
        public void InitializeBetAreas()
        {
            foreach (BetArea betArea in _betAreas)
            {
                betArea.Initialize();
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

        public void SetWheelSpinDuration(float duration)
        {
            _wheel.SetSpinDuration(duration);
        }
        
        public void SpawnChip(ChipType chipType)
        {
            GameObject chip     = Instantiate(_chipPrefab);
            ChipView   chipView = chip.GetComponent<ChipView>();
            chipView.Initialize(chipType);
            chipView.SetChipMaterial(_chipMaterials.Find(material => material.ChipType == chipType).Material);
            
            _tableChipHolderView.AddChipToHolder(chipView);
        }

        public void AddChipToHolder(ChipView chipView)
        {
            _tableChipHolderView.AddChipToHolder(chipView);
        }
        
        public void SpinWheel(int wheelStopValue)
        {
            bool isRandom = wheelStopValue == -1;
            
            _table.ResetSlots();
            _wheel.SetStopNumber(isRandom, wheelStopValue);
            _wheel.Spin();
        }
        
        public void ShowResult(int value)
        {
            _table.ShowWinningSlot(value);
        }
        
        public void PlaceChip(GameObject chip)
        {
            ChipView chipView = chip.GetComponent<ChipView>();
            
            _placedChips.Add(chipView);
            _tableChipHolderView.RemoveAndRearrangeChips(chipView);
        }
        
        public void RemoveChipFromBetArea(int betAreaId, GameObject chip)
        {
            ChipView chipView = chip.GetComponent<ChipView>();
            BetArea  betArea  = _betAreas.Find(area => area.Id == betAreaId);
            
            betArea.RemoveChip(chipView);
            chipView.MoveTo(_chipMoveTarget.position, () => Destroy(chip));
        }
        
        public async void ClearPlacedChips()
        {
            await Task.Delay(1000);

            GameEvents.WheelStopSpin();
            if (_placedChips.Count == 0) return;
            
            foreach (ChipView chip in _placedChips)
            {
                chip.MoveTo(_chipMoveTarget.position, () => Destroy(chip.gameObject));
            }
            
            _placedChips.Clear();
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