using Enums;

namespace Scripts.Helpers
{
    public class EnumComparison
    {
        public static ChipType GetChipType(string chipType)
        {
            switch (chipType)
            {
                case "One":
                    return ChipType.One;
                case "Five":
                    return ChipType.Five;
                case "Ten":
                    return ChipType.Ten;
                case "TwentyFive":
                    return ChipType.TwentyFive;
                case "Fifty":
                    return ChipType.Fifty;
                case "Hundred":
                    return ChipType.Hundred;
            }

            return (ChipType)0;
        }
    }
}