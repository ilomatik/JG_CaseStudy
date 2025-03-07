using Scripts.Helpers;

namespace Data
{
    public class GameData
    {
        private BetData _betData;
        
        private int _betCount;
        
        public void Initialize()
        {
            _betData = new BetData();
        }
        
        public void AddBet(BetHolder bet)
        {
            if (_betData.ContainsBet(bet.Id))
            {
                
            }
            else
            {
                bet.SetId(_betCount);
                _betData.AddBet(_betCount, bet);
                _betCount++;
            }
        }
        
        public void RemoveBet(BetHolder bet)
        {
            _betData.RemoveBet(bet.Id);
        }
        
        public void ClearBets()
        {
            _betData.ClearBets();
        }

        public AllBetHolder GetAllBets()
        {
            AllBetHolder allBetHolder = new AllBetHolder();

            foreach (var bet in _betData.GetBets())
            {
                allBetHolder.AddBet(bet.Key, bet.Value);
            }

            return allBetHolder;
        }
    }
}