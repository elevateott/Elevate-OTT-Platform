using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Repository.Extensions.Utility;

namespace ElevateOTT.Infrastructure.Repository.Extensions
{
    public static class RepositoryCommentExtensions
    {
        // Example of filter extension method
        //public static IQueryable<CommentModel> FilterCommentModels(this IQueryable<CommentModel> comments, uint minAge, uint maxAge) => 
        //    comments.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        public static IQueryable<CommentModel> Search(this IQueryable<CommentModel> comments, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) 
                return comments; 
            var lowerCaseTerm = searchTerm.Trim().ToLower(); 
            return comments.Where(e => e.Comment.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<CommentModel>? Sort(this IQueryable<CommentModel> comments, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return comments.OrderBy(e => e.Comment);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<CommentModel>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return comments.OrderBy(e => e.Comment);

            return comments.OrderBy(orderQuery);
        }
    }
}
