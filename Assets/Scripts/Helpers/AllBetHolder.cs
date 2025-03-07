using System.Collections.Generic;

namespace Scripts.Helpers
{
    public class AllBetHolder
    {
        public int BetCount => _bets.Count;
        
        private Dictionary<int, BetHolder> _bets;

        public AllBetHolder()
        {
            _bets = new Dictionary<int, BetHolder>();
        }
        
        public void AddBet(int id, BetHolder bet)
        {
            _bets.Add(id, bet);
        }
        
        public BetHolder GetBet(int id)
        {
            return _bets[id];
        }
    }
}