namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositorySubtitleExtensions
    {
        // Example of filter extension method
        //public static IQueryable<SubtitleModel> FilterSubtitleModels(this IQueryable<SubtitleModel> subtitles, uint minAge, uint maxAge) => 
        //    subtitles.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        //public static IQueryable<SubtitleModel> Search(this IQueryable<SubtitleModel> subtitles, string searchTerm)
        //{
        //    if (string.IsNullOrWhiteSpace(searchTerm)) 
        //        return subtitles; 
        //    var lowerCaseTerm = searchTerm.Trim().ToLower(); 
        //    return subtitles.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        //}

        //public static IQueryable<SubtitleModel> Sort(this IQueryable<SubtitleModel> subtitles, string orderByQueryString)
        //{
        //    if (string.IsNullOrWhiteSpace(orderByQueryString))
        //        return subtitles.OrderBy(e => e.Name);

        //    var orderQuery = OrderQueryBuilder.CreateOrderQuery<SubtitleModel>(orderByQueryString);

        //    if (string.IsNullOrWhiteSpace(orderQuery))
        //        return subtitles.OrderBy(e => e.Name);

        //    return subtitles.OrderBy(orderQuery);
        //}
    }
}
