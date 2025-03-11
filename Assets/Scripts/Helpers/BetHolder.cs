using Enums;

namespace Scripts.Helpers
{
    public class BetHolder
    {
        public int      BetAreaId { get; private set; }
        public BetType  BetType   { get; private set; }
        public int      Amount    { get; private set; }
        public ChipType ChipType  { get; private set; }
        
        public BetHolder(int betAreaId, BetType betType, ChipType chipType)
        {
            BetAreaId = betAreaId;
            BetType   = betType;
            ChipType  = chipType;
        }
        
        public void SetAmount(int amount)
        {
            Amount += amount;
        }
    }
}