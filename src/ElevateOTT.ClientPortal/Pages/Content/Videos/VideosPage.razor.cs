namespace ElevateOTT.ClientPortal.Pages.Content.Videos
{
    public partial class VideosPage
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
    }
}
