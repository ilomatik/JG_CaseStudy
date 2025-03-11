using System.Collections.Generic;
using Enums;

namespace Scripts.Bet
{
    public class PlayerBet
    {
        public BetType   BetType { get; private set; }
        public List<int> Numbers { get; private set; }
        public int       TotalChipValue { get; private set; }

        public PlayerBet(BetType betType, List<int> numbers, ChipType chipValue)
        {
            BetType        = betType;
            Numbers        = numbers;
            TotalChipValue = (int)chipValue;
        }

        public void IncreaseBet(ChipType chipValue)
        {
            TotalChipValue += (int)chipValue;
        }

        public void DecreaseBet(ChipType chipValue)
        {
            TotalChipValue -= (int)chipValue;
        }
    }

}