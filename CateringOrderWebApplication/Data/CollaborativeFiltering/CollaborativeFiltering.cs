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

            // Pobierz oceny użytkownika
            Dictionary<Guid, int> userRatings = GetUserRatings(userId);

            // Pobierz cateringi, które użytkownik jeszcze nie ocenił
            List<Guid> unratedCaterings = GetUnratedCaterings(userId);

            // Dla każdego cateringu, oblicz zalecenie
            foreach (var cateringId in unratedCaterings)
            {
                double recommendation = CalculateItemBasedRecommendation(userId, cateringId, userRatings);
                recommendations.Add(cateringId, recommendation);
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

        private List<Guid> GetUnratedCaterings(Guid userId)
        {
            var allCaterings = dbContext.Caterings.Select(c => c.Id).ToList();
            var userRatings = GetUserRatings(userId);

            // Wszystkie cateringi, których użytkownik jeszcze nie ocenił
            var unratedCaterings = allCaterings.Except(userRatings.Keys).ToList();

            return unratedCaterings;
        }

        private double CalculateItemBasedRecommendation(Guid userId, Guid cateringId, Dictionary<Guid, int> userRatings)
        {
            var ratingsForCatering = dbContext.Ratings
                .Where(r => r.CateringId == cateringId && userRatings.ContainsKey(r.UserId))
                .ToList();

            if (ratingsForCatering.Count == 0)
            {
                // Brak ocen dla tego cateringu od innych użytkowników
                return 0;
            }

            double sumSimilarities = 0;
            double weightedSum = 0;

            foreach (var ratingForCatering in ratingsForCatering)
            {
                double cosineSimilarity = CalculateCosineSimilarity(cateringId, ratingForCatering.CateringId);
                double jaccardSimilarity = CalculateJaccardSimilarity(userId, ratingForCatering.UserId);

                // Uwzględnij obie miary podobieństwa
                sumSimilarities += Math.Abs(cosineSimilarity) + Math.Abs(jaccardSimilarity);

                // Uwzględnij obie miary podobieństwa w obliczeniach ważonych
                weightedSum += (cosineSimilarity + jaccardSimilarity) * userRatings[ratingForCatering.UserId];
            }

            if (sumSimilarities == 0)
            {
                // Brak podobieństwa między cateringami
                return 0;
            }

            return weightedSum / sumSimilarities;
        }

        private double CalculateJaccardSimilarity(Guid userId1, Guid userId2)
        {
            var ratings1 = dbContext.Ratings
                .Where(r => r.UserId == userId1)
                .Select(r => r.CateringId)
                .ToHashSet();

            var ratings2 = dbContext.Ratings
                .Where(r => r.UserId == userId2)
                .Select(r => r.CateringId)
                .ToHashSet();

            var intersection = ratings1.Intersect(ratings2).Count();
            var union = ratings1.Union(ratings2).Count();

            if (union == 0)
            {
                // Brak ocen dla obu użytkowników
                return 0;
            }

            return (double)intersection / union;
        }

        private double CalculateCosineSimilarity(Guid cateringId1, Guid cateringId2)
        {
            var ratings1 = dbContext.Ratings
                .Where(r => r.CateringId == cateringId1)
                .ToDictionary(r => r.UserId, r => r.Rate);

            var ratings2 = dbContext.Ratings
                .Where(r => r.CateringId == cateringId2)
                .ToDictionary(r => r.UserId, r => r.Rate);

            var commonUsers = ratings1.Keys.Intersect(ratings2.Keys).ToList();

            double dotProduct = 0;
            double magnitude1 = 0;
            double magnitude2 = 0;

            foreach (var userId in commonUsers)
            {
                dotProduct += ratings1[userId] * ratings2[userId];
                magnitude1 += Math.Pow(ratings1[userId], 2);
                magnitude2 += Math.Pow(ratings2[userId], 2);
            }

            if (magnitude1 == 0 || magnitude2 == 0)
            {
                // Brak ocen dla wspólnych użytkowników
                return 0;
            }

            return dotProduct / (Math.Sqrt(magnitude1) * Math.Sqrt(magnitude2));
        }
    }
}