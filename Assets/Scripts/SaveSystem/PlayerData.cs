using System.Collections.Generic;

namespace Scripts
{
    [System.Serializable]
    public class PlayerData
    {
        public int            _playerId;
        public string         _playerName;
        public List<ChipData> _chips; 
        public int            _gamesPlayed;
        public int            _gamesWon;
        public int            _gamesLost;
        public string         _lastLoginTime;

        // Constructor
        public PlayerData(int playerId, string name, List<ChipData> chipList, int played, int won, int lost, string lastLogin)
        {
            _playerId      = playerId;
            _playerName    = name;
            _chips         = chipList;
            _gamesPlayed   = played;
            _gamesWon      = won;
            _gamesLost     = lost;
            _lastLoginTime = lastLogin;
        }
        
        public void AddChip(ChipData chip)
        {
            _chips.Add(chip);
        }
    }
}