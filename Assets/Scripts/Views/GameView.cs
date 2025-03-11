using System.Collections.Generic;
using Constants;
using Enums;
using Events;
using UnityEngine;
using Views.BetAreas;
using Views.Chip;

namespace Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameObject    _wheelObject;
        [SerializeField] private GameObject    _tableObject;
        [SerializeField] private List<BetArea> _betAreas;

        private int       _playerId;
        private WheelView _wheel;
        private TableView _table;

        public void Initialize(float wheelSpeed)
        {
            _playerId = PlayerPrefs.GetInt(GameConstant.PLAYER_ID_COUNTER);
            
            _wheel = _wheelObject.GetComponent<WheelView>();
            _table = _tableObject.GetComponent<TableView>();

            _table.Initialize();
            _wheel.Initialize();
            _wheel.SetSpinDuration(wheelSpeed);
        }
        
        private void InitializeBetAreas()
        {
            foreach (BetArea betArea in _betAreas)
            {
            }
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
}