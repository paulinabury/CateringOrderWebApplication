namespace CateringOrderWebApplication.Data.CollaborativeFiltering;

public interface ICollaborativeFiltering
{
    Dictionary<Guid, double> GetRecommendations(Guid userId);
}