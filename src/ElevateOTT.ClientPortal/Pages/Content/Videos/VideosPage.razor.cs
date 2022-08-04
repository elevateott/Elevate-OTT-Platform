namespace ElevateOTT.ClientPortal.Pages.Content.Videos
{
    public partial class VideosPage : IDisposable
    {
        public Position Position { get; set; } = Position.Left;

        public TabHeaderPosition HeaderPosition { get; set; } = TabHeaderPosition.Before;

      
         private async Task CreateVideo() 
        {
            System.Console.WriteLine("CreateVideo");
        }

        private async Task GetVideo() 
        {
            System.Console.WriteLine("GetVideo");
        }

        private async Task GetVideosByTenant() 
        {
            System.Console.WriteLine("GetVideosByTenant");
        }

        private async Task UpdateVideo() 
        {
            System.Console.WriteLine("UpdateVideo");
        }

        private async Task DeleteVideo() 
        {
            System.Console.WriteLine("DeleteVideo");
        }

        public void Dispose()
        {
        }
    }
}
