namespace Library.Application.Result
{
    public class Error
    {
        public string Details { get; set; }

        public Error(string details)
        {
            Details = details;
        }
    }
}
