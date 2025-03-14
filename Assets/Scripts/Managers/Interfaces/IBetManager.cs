using System.Collections.Generic;
using Enums;
using Scripts.Bet;

namespace Managers
{
    public interface IBetManager
    {
        void Initialize();
        void PlaceBet(int playerId, BetType betType, List<int> numbers, ChipType chipValue);
        void RemoveBet(int playerId, BetType betType, List<int> numbers, ChipType chipValue);
        List<PlayerBet> GetWinningBets(int playerId, int winningNumber);
    }
}