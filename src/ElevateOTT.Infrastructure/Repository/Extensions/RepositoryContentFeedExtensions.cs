namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositoryContentFeedExtensions
    {
        // Example of filter extension method
        //public static IQueryable<ContentFeedModel> FilterContentFeedModels(this IQueryable<ContentFeedModel> contentFeeds, uint minAge, uint maxAge) => 
        //    contentFeeds.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        //public static IQueryable<ContentFeedModel> Search(this IQueryable<ContentFeedModel> contentFeeds, string searchTerm)
        //{
        //    if (string.IsNullOrWhiteSpace(searchTerm)) 
        //        return contentFeeds; 
        //    var lowerCaseTerm = searchTerm.Trim().ToLower(); 
        //    return contentFeeds.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        //}

        //public static IQueryable<ContentFeedModel> Sort(this IQueryable<ContentFeedModel> contentFeeds, string orderByQueryString)
        //{
        //    if (string.IsNullOrWhiteSpace(orderByQueryString))
        //        return contentFeeds.OrderBy(e => e.Name);

        //    var orderQuery = OrderQueryBuilder.CreateOrderQuery<ContentFeedModel>(orderByQueryString);

        //    if (string.IsNullOrWhiteSpace(orderQuery))
        //        return contentFeeds.OrderBy(e => e.Name);

        //    return contentFeeds.OrderBy(orderQuery);
        //}
    }
}
