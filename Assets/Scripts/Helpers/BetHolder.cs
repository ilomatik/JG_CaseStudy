using Enums;

namespace Scripts.Helpers
{
    public class BetHolder
    {
        public int      Id       { get; private set; }
        public BetType  BetType  { get; private set; }
        public int      Amount   { get; private set; }
        public ChipType ChipType { get; private set; }
        
        public BetHolder(BetType betType, ChipType chipType)
        {
            BetType  = betType;
            ChipType = chipType;
        }
        
        public void SetId(int id)
        {
            Id = id;
        }
        
        public void SetAmount(int amount)
        {
            Amount = amount;
        }
    }
}