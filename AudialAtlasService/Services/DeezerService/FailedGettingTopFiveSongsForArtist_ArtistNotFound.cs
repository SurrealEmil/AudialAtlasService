namespace AudialAtlasService.Services.DeezerService
{
    public class FailedGettingTopFiveSongsForArtist_ArtistNotFound : Exception
    {
        private readonly string _message;

        public FailedGettingTopFiveSongsForArtist_ArtistNotFound()
        {
            _message = base.Message;
        }
        public FailedGettingTopFiveSongsForArtist_ArtistNotFound(string message)
        {
            _message = message;
        }

        public override string Message => _message;
    }
}
