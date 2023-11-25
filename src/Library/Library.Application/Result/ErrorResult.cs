using Library.Application.Helpers.Extensions;

namespace Library.Application.Result
{
    public class ErrorResult : Result
    {
        public IReadOnlyCollection<Error> Errors { get; }

        public ErrorResult(ICollection<Error> errors)
        {
            IsSuccess = false;
            if (errors is null)
            {
                Errors = Array.Empty<Error>();
            }
            else 
            {
                Errors = errors.AsReadOnly();
            }
        }
    }
}
