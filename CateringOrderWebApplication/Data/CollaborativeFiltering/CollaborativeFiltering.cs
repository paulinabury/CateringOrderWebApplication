namespace CateringOrderWebApplication.Data.CollaborativeFiltering
{
    public class CollaborativeFiltering : ICollaborativeFiltering
    {
        private CateringOrderDbContext dbContext;

        public CollaborativeFiltering(CateringOrderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Dictionary<Guid, double> GetRecommendations(Guid userId)
        {
            Dictionary<Guid, double> recommendations = new Dictionary<Guid, double>();

            // Pobierz oceny dla użytkownika
            Dictionary<Guid, int> userRatings = GetUserRatings(userId);

            // Pobierz innych użytkowników, którzy ocenili te same cateringi
            List<Guid> similarUsers = GetSimilarUsers(userId);

            // Dla każdego podobnego użytkownika, oblicz podobieństwo i zalecenia
            foreach (var otherUserId in similarUsers)
            {
                if (otherUserId == userId) continue;

                double similarity = CalculatePearsonSimilarity(userId, otherUserId);

                foreach (var cateringId in userRatings.Keys)
                {
                    if (!userRatings.ContainsKey(cateringId))
                    {
                        if (!recommendations.ContainsKey(cateringId))
                            recommendations[cateringId] = 0;

                        recommendations[cateringId] += similarity * userRatings[cateringId];
                    }
                }
            }

            return recommendations;
        }

        private Dictionary<Guid, int> GetUserRatings(Guid userId)
        {
            var userRatings = dbContext.Ratings
                .Where(r => r.UserId == userId)
                .ToDictionary(r => r.CateringId, r => r.Rate);

            return userRatings;
        }

        private List<Guid> GetSimilarUsers(Guid userId)
        {
            var userRatings = GetUserRatings(userId);

            var similarUsers = dbContext.Ratings
                .Where(r => userRatings.ContainsKey(r.CateringId) && r.UserId != userId)
                .Select(r => r.UserId)
                .Distinct()
                .ToList();

            return similarUsers;
        }

        private double CalculatePearsonSimilarity(Guid userId1, Guid userId2)
        {
            Dictionary<Guid, int> ratings1 = GetUserRatings(userId1);
            Dictionary<Guid, int> ratings2 = GetUserRatings(userId2);

            // Znajdź wspólne cateringi ocenione przez obu użytkowników
            List<Guid> commonCaterings = new List<Guid>(ratings1.Keys);
            commonCaterings = commonCaterings.Intersect(ratings2.Keys).ToList();

            // Liczniki i mianowniki do obliczenia korelacji Pearsona
            double sumXY = 0, sumX = 0, sumY = 0, sumX2 = 0, sumY2 = 0;
            int n = commonCaterings.Count;

            foreach (Guid cateringId in commonCaterings)
            {
                int rating1 = ratings1[cateringId];
                int rating2 = ratings2[cateringId];

                sumXY += rating1 * rating2;
                sumX += rating1;
                sumY += rating2;
                sumX2 += Math.Pow(rating1, 2);
                sumY2 += Math.Pow(rating2, 2);
            }

            // Oblicz korelację Pearsona
            double numerator = sumXY - sumX * sumY / n;
            double denominator = Math.Sqrt((sumX2 - Math.Pow(sumX, 2) / n) * (sumY2 - Math.Pow(sumY, 2) / n));

            if (denominator == 0)
            {
                // Dziel przez zero, zwróć 0 jako korelację (brak podobieństwa)
                return 0;
            }

            return numerator / denominator;
        }
    }
}