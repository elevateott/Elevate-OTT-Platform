using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Common.Interfaces.Repository
{
    public interface IRepositoryManager
    {
        IVideoRepository Video { get; }
        ILiveStreamRepository LiveStream { get; }
        IPodcastRepository Podcast { get; }
        IAssetImageRepository AssetImage { get; }
        IContentFeedRepository ContentFeed { get; }
        IAuthorRepository Author { get; }
        ICategoryRepository Category { get; }
        ICollectionRepository Collection { get; }

        //ICommentRepository Comment { get; }
        //IExtraRepository Extra { get; }
        //ISubtitleRepository Subtitle { get; }
        //ISubscriptionRepository Subscription { get; }
        //IProductItemRepository ProductItem { get; }
        //IItemPriceRepository ItemPrice { get; }
        //IProductFamilyRepository ProductFamily { get; }

        Task SaveAsync();
    }
}
