using System.Collections.Generic;
using Scripts.Helpers;

namespace Data
{
    public class BetData
    {
        private Dictionary<int, BetHolder> _bets;
        
        public BetData()
        {
            _bets = new Dictionary<int, BetHolder>();
        }
        
        public void AddBet(int id, BetHolder bet)
        {
            _bets.Add(id, bet);
        }
        
        public void RemoveBet(int id)
        {
            _bets.Remove(id);
        }
        
        public void ClearBets()
        {
            _bets.Clear();
        }
        
        public bool ContainsBet(int id)
        {
            return _bets.ContainsKey(id);
        }
        
        public BetHolder GetBet(int id)
        {
            return _bets[id];
        }

        public Dictionary<int, BetHolder> GetBets()
        {
            return _bets;
        }
    }
}