using ElevateOTT.Domain.Entities.Content;
using ElevateOTT.Domain.Entities.Mux;
using ElevateOTT.Domain.Entities.Products;
using ElevateOTT.Domain.Entities.Subscriptions;

namespace ElevateOTT.Infrastructure.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            // Configure one-to-one relationships
            //modelBuilder.Entity<AppUserModel>()
            // .HasOne(a => a.SubscriptionSettings)
            // .WithOne(b => b.AppUser)
            // .HasForeignKey<SubscriptionSettingsModel>(b => b.TenantId);

            modelBuilder.Entity<VideoModel>()
                .HasOne(a => a.SeoMetaData)
                .WithOne(b => b.Video)
                .HasForeignKey<SeoMetaDataModel>(b => b.VideoId);

            modelBuilder.Entity<CategoryModel>()
                .HasOne(a => a.SeoMetaData)
                .WithOne(b => b.Category)
                .HasForeignKey<SeoMetaDataModel>(b => b.VideoId);

            modelBuilder.Entity<CollectionModel>()
                .HasOne(a => a.SeoMetaData)
                .WithOne(b => b.Collection)
                .HasForeignKey<SeoMetaDataModel>(b => b.VideoId);


            // Configure one-to-many relationships
            modelBuilder.Entity<VideoModel>()
                .HasOne(p => p.VideoFolder)
                .WithMany(b => b.Videos)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<VideoModel>()
                .HasOne(p => p.Collection)
                .WithMany(b => b.Videos)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Configure many-to-many relationships
            modelBuilder.Entity<VideoAuthorModel>().HasKey(x => new { x.VideoId, x.AuthorId });
            modelBuilder.Entity<VideoGenreModel>().HasKey(x => new { x.VideoId, x.GenreId });
            modelBuilder.Entity<VideoTagModel>().HasKey(x => new { x.VideoId, x.TagId });
            modelBuilder.Entity<VideoCategoryModel>().HasKey(x => new { x.VideoId, x.CategoryId });
            modelBuilder.Entity<CategoryCollectionModel>().HasKey(x => new { x.CategoryId, x.CollectionId });

            base.OnModelCreating(modelBuilder);

            //SeedDummyData(modelBuilder);
        }

        private void SeedDummyData(ModelBuilder modelBuilder)
        {
            // Seeding done via Postman
            // modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        }

        public DbSet<AssetImageModel>? AssetImages { get; set; }
        public DbSet<AuthorModel>? Authors { get; set; }
        public DbSet<CategoryCollectionModel>? CategoriesCollections { get; set; }
        public DbSet<CategoryModel>? Categories { get; set; }
        public DbSet<CategoryVideoPositionModel>? CategoryVideoPositions { get; set; }
        public DbSet<CollectionModel>? Collections { get; set; }
        public DbSet<CollectionVideoPositionModel>? CollectionVideoPositions { get; set; }
        public DbSet<CommentModel>? Comments { get; set; }
        public DbSet<ContentFeedModel>? ContentFeeds { get; set; }
        public DbSet<ExtraModel>? Extras { get; set; }
        public DbSet<GenreModel>? Genres { get; set; }
        public DbSet<ProductFamilyModel>? ProductFamilies { get; set; }
        public DbSet<ItemPriceModel>? ItemPrices { get; set; }
        public DbSet<LiveStreamModel>? LiveStreams { get; set; }
        public DbSet<MuxPlaybackIdModel>? MuxPlaybackIds { get; set; }
        public DbSet<ProductItemModel>? ProductItems { get; set; }
        public DbSet<SubscriptionModel>? Subscriptions { get; set; }
        public DbSet<SubscriptionItemModel>? SubscriptionItems { get; set; }
        public DbSet<SubtitleModel>? Subtitles { get; set; }
        public DbSet<TagModel>? Tags { get; set; }
        public DbSet<VideoAuthorModel>? VideosAuthors { get; set; }
        public DbSet<VideoAuthorModel>? VideosCategories { get; set; }
        public DbSet<VideoFolderModel>? VideoFolders { get; set; }
        public DbSet<VideoGenreModel>? VideosGenres { get; set; }
        public DbSet<VideoModel>? Videos { get; set; }
        public DbSet<VideoTagModel>? VideosTags { get; set; }
    }
}
