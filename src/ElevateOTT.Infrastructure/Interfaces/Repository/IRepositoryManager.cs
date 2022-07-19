namespace ElevateOTT.Infrastructure.Interfaces.Repository
{
    public interface IRepositoryManager
    {
        IAuthorRepository Author { get; }
        IVideoRepository Video { get; }
        ILiveStreamRepository LiveStream { get; }
        ICategoryRepository Category { get; }
        ICollectionRepository Collection { get; }
        ICommentRepository Comment { get; }
        IContentFeedRepository ContentFeed { get; }
        IExtraRepository Extra { get; }
        IGenreRepository Genre { get; }
        ISeoMetaDataRepository SeoMetaData { get; }
        ISubtitleRepository Subtitle { get; }
        ITagRepository Tag { get; }
        ISubscriptionRepository Subscription { get; }
        IProductItemRepository ProductItem { get; }
        IItemPriceRepository ItemPrice { get; }
        IProductFamilyRepository ProductFamily { get; }

        Task SaveAsync();
    }
}
