using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Infrastructure.Interfaces.Repository;

namespace ElevateOTT.Infrastructure.Repository
{
    public sealed class CommentRepository : RepositoryBase<CommentModel>, ICommentRepository
    {
        public CommentRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }


        //public Task<PagedList<CommentModel>> GetCommentsAsync(Guid tenantId, CommentParameters commentParameters, bool trackChanges)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<CommentModel?> GetCommentAsync(Guid commentId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<CommentModel?> FindCommentByConditionAsync(Expression<Func<CommentModel, bool>> expression, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void CreateCommentForTenant(Guid tenantId, CommentModel comment)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CommentModel>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(CommentModel comment)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CommentExistsAsync(Expression<Func<CommentModel, bool>> expression) => await ExistsAsync(expression);
    }
}
