namespace AudialAtlasService.Repositories.GenreRepoExceptions
{
    public class GenreAlreadyExistsException : Exception
    {
        private readonly string _message;

        public GenreAlreadyExistsException() : base()
        {
            _message = base.Message;
        }
        public GenreAlreadyExistsException(string message) : base(message)
        {
            _message = message;
        }

        public override string Message => _message;
    }
}
