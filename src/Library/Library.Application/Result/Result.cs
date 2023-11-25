namespace Library.Application.Result
{
    public abstract class Result : IResult
    {
        public bool IsSuccess { get; protected set; }
        public bool IsOperationSuccessfully() => IsSuccess;
    }

    public abstract class Result<T> : Result
    {
        public T Data { get; protected set; }

        protected Result(T data)
        {
            Data = data;
        }
    }
}
