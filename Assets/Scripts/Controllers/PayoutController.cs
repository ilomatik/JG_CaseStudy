using Enums;
using Events;

namespace Controllers
{
    public class PayoutController
    {
        public PayoutController()
        {
            // Constructor
        }
        
        public void PayoutToPlayer(int amount)
        {
            int hundred     = amount / 100;
            amount         -= hundred * 100;
            
            int fifty       = amount / 50;
            amount         -= fifty * 50;
            
            int twentyFive  = amount / 25;
            amount         -= twentyFive * 25;
            
            int ten         = amount / 10;
            amount         -= ten * 10;
            
            int five        = amount / 5;
            amount         -= five * 5;
            
            GameEvents.OnChipAmountChanged(ChipType.Hundred.ToString(), hundred);
            GameEvents.OnChipAmountChanged(ChipType.Fifty.ToString(), fifty);
            GameEvents.OnChipAmountChanged(ChipType.TwentyFive.ToString(), twentyFive);
            GameEvents.OnChipAmountChanged(ChipType.Ten.ToString(), ten);
            GameEvents.OnChipAmountChanged(ChipType.Five.ToString(), five);
            GameEvents.OnChipAmountChanged(ChipType.One.ToString(), amount);
        }
    }
}