using Library.Application.Helpers;
using Library.Application.Result;

namespace Library.Application.Tests.Helpers
{
    public class ErrorHelperTests
    {
        private ErrorResult ErrorResult { get; set; }
        public ErrorHelperTests()
        {
            ErrorResult = new ErrorResult(new List<Error>()
            {
                new Error(AppErrorCode.NotFound.ToString())
            });
        }

        [Fact]
        public void ReturnTrueWhenErrorResultHasSpecifiedError()
        {
            bool hasError = ErrorResult.CheckIfErrorExists(AppErrorCode.NotFound);

            Assert.True(hasError);
        }

        [Fact]
        public void ReturnFalseWhenErrorResultDoesntHaveSpecifiedError()
        {
            bool hasError = ErrorResult.CheckIfErrorExists(AppErrorCode.InvalidEntity);

            Assert.False(hasError);
        }
    }
}
