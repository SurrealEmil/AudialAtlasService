namespace AudialAtlasService.Repositories.SongRepoExceptions
{
    public class SongNotFoundException : Exception
    {
        private readonly string _message;

        public SongNotFoundException()
        {
            _message = "No song found";
        }
        public SongNotFoundException(string message)
        {
            _message = message;
        }

        public override string Message => base.Message;
    }
}
