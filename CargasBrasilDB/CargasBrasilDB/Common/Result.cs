namespace CargasBrasilDB.Common
{
    public enum ErrorType { None, Validation, NotFound, Conflict, Unauthorized }

    
    public class Result
    {
        protected Result(bool success, string message, ErrorType error)
        {
            Success = success;
            Message = message;
            Error = error;
        }

        public bool Success { get; }
        public string Message { get; }
        public ErrorType Error { get; }

        public virtual object? Payload => null;

        public static Result Ok(string message) => new(true, message, ErrorType.None);
        public static Result Fail(ErrorType error, string message) => new(false, message, error);
    }

    public class Result<T> : Result
    {
        private Result(bool success, string message, ErrorType error, T? data)
            : base(success, message, error) => Data = data;

        public T? Data { get; }

        public override object? Payload => Data;

        public static Result<T> Ok(T data, string message) => new(true, message, ErrorType.None, data);
        public static new Result<T> Fail(ErrorType error, string message) => new(false, message, error, default);
    }

    
    public record ApiResponse(bool Sucesso, string Mensagem, object? Data = null);
}
