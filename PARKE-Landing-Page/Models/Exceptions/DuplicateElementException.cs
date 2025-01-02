namespace PARKE_Landing_Page.Models.Exceptions
{
    public class DuplicateElementException : Exception
    {
        //public DuplicateElementException(string message) : base(message) { }
        public DuplicateElementException()
        : base()
        {
        }

        public DuplicateElementException(string message)
            : base(message)
        {
        }

        public DuplicateElementException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DuplicateElementException(string code, object key)
            : base($"Entity with code {code} ({key}) already exists.")
        {
        }
    }
}
