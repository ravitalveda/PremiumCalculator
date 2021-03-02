using PremiumCalculator.Models;

namespace PremiumCalculator.Helpers
{
    public static class PremiumHelper
    {
        public static double GetAgePoints(int age)
        {
            switch (age)
            {
                case 17: return 0.1;
                case 18: return 0.2;
                case 19: return 0.3;
                case 20: return 0.35;
                case 21: return 0.35;
                case 22: return 0.35;
                case 23: return 0.35;
                case 24: return 0.35;
                case 25: return 0.35;
                case 26: return 0.40;
                case 27: return 0.45;
                default: return 0;
            }
        }

        public static double GetHeightPoints(int height)
        {
            switch (height)
            {
                case 150: return 0.51;
                case 151: return 0.53;
                case 152: return 0.53;
                case 153: return 0.53;
                case 154: return 0.53;
                case 155: return 0.53;
                case 156: return 0.55;
                case 157: return 0.58;
                case 158: return 0.59;
                case 159: return 0.6;
                case 160: return 0.8;
                default: return 0;
            }
        }

        public static double GetRating(PremiumModel ratingModel)
        {
            var rating = ratingModel.Alpha + ratingModel.Beta * (ratingModel.HeightPoints * ratingModel.AgePoints);

            return rating ?? 0.0;
        }
    }
}