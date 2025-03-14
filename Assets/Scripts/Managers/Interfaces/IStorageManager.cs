using System.Collections.Generic;
using Scripts;

namespace Managers
{
    public interface IStorageManager
    {
        void Initialize(string playerName);
        void UpdateChipAmount(string chipType, int amountChange);
        void IncrementGamesPlayed();
        void IncrementGamesWon();
        void IncrementGamesLost();
        
        PlayerData Player { get; }
        List<ChipData> GetPlayerChips();
        int            GetPlayerChipAmount(string chipType);
    }
}