using System;
using Enums;

namespace Scripts.Helpers
{
    public static class PayoutCalculator
    {
        private const int EuropeanNumbers = 37;
        private const int AmericanNumbers = 38;

        private static float CalculatePayout(BetType betType, bool isAmericanRoulette)
        {
            int totalNumbers = isAmericanRoulette ? AmericanNumbers : EuropeanNumbers;

            return betType switch
            {
                BetType.Single => 35.0f / totalNumbers,
                BetType.Split  => 17.0f / totalNumbers,
                BetType.Street => 11.0f / totalNumbers,
                BetType.Corner => 8.0f / totalNumbers,
                BetType.Line   => 5.0f / totalNumbers,
                BetType.Column => 2.0f / totalNumbers,
                BetType.Dozen  => 2.0f / totalNumbers,
                BetType.Red    => 1.0f / totalNumbers,
                BetType.Black  => 1.0f / totalNumbers,
                BetType.Even   => 1.0f / totalNumbers,
                BetType.Odd    => 1.0f / totalNumbers,
                BetType.Low    => 1.0f / totalNumbers,
                BetType.High   => 1.0f / totalNumbers,
                _ => throw new ArgumentOutOfRangeException(nameof(betType), betType, null)
            };
        }

        public static float CalculatePayoutAmount(BetType betType, bool isAmericanRoulette, float betAmount)
        {
            float  payoutRatio = CalculatePayout(betType, isAmericanRoulette);
            return payoutRatio * betAmount + betAmount;
        }
    }
}