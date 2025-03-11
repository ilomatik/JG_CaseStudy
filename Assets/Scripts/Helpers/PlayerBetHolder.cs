using System.Collections.Generic;
using System.Linq;
using Enums;
using Scripts.Bet;

namespace Scripts.Helpers
{
    public class PlayerBetHolder
    {
        private List<PlayerBet> _bets;
        
        public PlayerBetHolder()
        {
            _bets = new List<PlayerBet>();
        }

        public void AddBet(BetType betType, List<int> numbers, ChipType chipValue)
        {
            PlayerBet existingBet = _bets.FirstOrDefault(b => b.BetType == betType && AreArraysEqual(b.Numbers, numbers));

            if (existingBet != null)
            {
                existingBet.IncreaseBet(chipValue);
            }
            else
            {
                _bets.Add(new PlayerBet(betType, numbers, chipValue));
            }
        }

        public void RemoveBet(BetType betType, List<int> numbers, ChipType chipValue)
        {
            PlayerBet existingBet = _bets.FirstOrDefault(b => b.BetType == betType && AreArraysEqual(b.Numbers, numbers));

            if (existingBet != null)
            {
                existingBet.DecreaseBet(chipValue);
                
                if (existingBet.TotalChipValue <= 0) 
                {
                    _bets.Remove(existingBet);
                }
            }
        }

        public List<PlayerBet> GetAllBets()
        {
            return _bets;
        }

        private bool AreArraysEqual(List<int> firstNumberList, List<int> secondNumberList)
        {
            if (firstNumberList.Count != secondNumberList.Count) return false;

            for (int i = 0; i < firstNumberList.Count; i++)
            {
                if (firstNumberList[i] != secondNumberList[i]) return false;
            }
            
            return true;
        }
    }
}