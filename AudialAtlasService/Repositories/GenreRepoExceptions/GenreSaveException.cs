namespace AudialAtlasService.Repositories.GenreRepoExceptions
{
    public class GenreSaveException : Exception
    {
        private readonly string _message;

        public GenreSaveException() : base()
        {
            _message = base.Message;
        }

        public GenreSaveException(string message, Exception innerException) : base(message, innerException)
        {
            _message = message;
        }

        public override string Message => _message;
    }

}
