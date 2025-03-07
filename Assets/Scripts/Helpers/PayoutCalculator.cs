using System;
using Enums;
using UnityEngine;

namespace Scripts.Helpers
{
    public class PayoutCalculator
    {
        private const int EuropeanNumbers = 37;
        private const int AmericanNumbers = 38;

        private static float CalculatePayout(BetType betType, bool isAmericanRoulette)
        {
            int totalNumbers = isAmericanRoulette ? AmericanNumbers : EuropeanNumbers;

            switch (betType)
            {
                case BetType.StraightUp:
                    return 35.0f / totalNumbers;

                case BetType.Split:
                    return 17.0f / totalNumbers;

                case BetType.Street:
                    return 11.0f / totalNumbers;

                case BetType.Corner:
                    return 8.0f / totalNumbers;

                case BetType.SixLine:
                    return 5.0f / totalNumbers;

                case BetType.Column:
                    return 2.0f / totalNumbers;

                case BetType.Dozen:
                    return 2.0f / totalNumbers;

                case BetType.EvenMoney:
                    return 1.0f / totalNumbers;

                case BetType.TopLine:
                    if (isAmericanRoulette)
                    {
                        return 6.0f / AmericanNumbers;
                    }
                    else
                    {
                        Debug.LogError("Top Line bet is only available in American Roulette.");
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(betType), betType, null);
            }
            
            return 0.0f;
        }

        public static float CalculatePayoutAmount(BetType betType, bool isAmericanRoulette, float betAmount)
        {
            float  payoutRatio = CalculatePayout(betType, isAmericanRoulette);
            return payoutRatio * betAmount + betAmount;
        }
    }
}