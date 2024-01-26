namespace AudialAtlasService.Repositories.SongRepoExceptions
{
    public class SongFailedToAddToDatabaseException : Exception
    {
        private readonly string _message;

        public SongFailedToAddToDatabaseException()
        {
            _message = "Failed to add song to database";
        }
        public SongFailedToAddToDatabaseException(string message)
        {
            _message = message;
        }

        public override string Message => base.Message;
    }
}
