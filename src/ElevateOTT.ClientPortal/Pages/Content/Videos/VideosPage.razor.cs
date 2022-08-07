namespace ElevateOTT.ClientPortal.Pages.Content.Videos
{
    public partial class VideosPage : IDisposable
    {
        [Inject] private IBreadcrumbService? BreadcrumbService { get; set; }

        protected override void OnInitialized()
        {
            BreadcrumbService?.SetBreadcrumbItems(new List<BreadcrumbItem>
            {
                new(Resource.Home, "/"),
                new(Resource.Videos, "#", true)
            });
        }



        public void Dispose()
        {
        }
    }
}
