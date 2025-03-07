using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Scripts.Helpers
{
    public class PayoutCalculator
    {
        private static readonly Dictionary<BetType, float> PayoutOdds = new Dictionary<BetType, float>
        {
            { BetType.OneToTwelve, 2.0f },
            { BetType.ThirteenToTwentyFour, 2.0f },
            { BetType.TwentyFiveToThirtySix, 2.0f },
            { BetType.Red, 1.0f },
            { BetType.Black, 1.0f },
            { BetType.Even, 1.0f },
            { BetType.Odd, 1.0f },
            { BetType.OneToEighteen, 1.0f },
            { BetType.NineteenToThirtySix, 1.0f }
        };

        public static float CalculatePayout(BetType betType, float amountWagered, bool isWinningBet)
        {
            if (!isWinningBet)
            {
                return 0.0f;
            }

            if (PayoutOdds.TryGetValue(betType, out float odds))
            {
                return amountWagered * (odds + 1);
            }

            Debug.LogError("Payout odds not found for bet type: " + betType);
            return 0.0f;
        }
    }
}