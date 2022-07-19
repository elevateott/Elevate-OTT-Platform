namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositoryExtraExtensions
    {
        // Example of filter extension method
        //public static IQueryable<ExtraModel> FilterExtraModels(this IQueryable<ExtraModel> extras, uint minAge, uint maxAge) => 
        //    extras.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        //public static IQueryable<ExtraModel> Search(this IQueryable<ExtraModel> extras, string searchTerm)
        //{
        //    if (string.IsNullOrWhiteSpace(searchTerm)) 
        //        return extras; 
        //    var lowerCaseTerm = searchTerm.Trim().ToLower(); 
        //    return extras.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        //}

        //public static IQueryable<ExtraModel> Sort(this IQueryable<ExtraModel> extras, string orderByQueryString)
        //{
        //    if (string.IsNullOrWhiteSpace(orderByQueryString))
        //        return extras.OrderBy(e => e.Name);

        //    var orderQuery = OrderQueryBuilder.CreateOrderQuery<ExtraModel>(orderByQueryString);

        //    if (string.IsNullOrWhiteSpace(orderQuery))
        //        return extras.OrderBy(e => e.Name);

        //    return extras.OrderBy(orderQuery);
        //}
    }
}
