
namespace Domain
{
    public class OperationResult<T>
    {
        public bool isSuccessfull { get; private set; }

        public T Data { get; private set; }

        public string Message { get; private set; }

        public string ErrorMessage {  get; private set; }

        private OperationResult(bool isSuccesful, T data, string message, string errorMessage)
        {
            isSuccessfull = isSuccesful;
            Message = message;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static OperationResult<T> Successfull(T data, string message = "Operation Successfull")
        {
            return new OperationResult<T>(true, data, message, null);
        }
        public static OperationResult<T> Failure(string errorMessage, string message = "Operation Failed")
        {
            return new OperationResult<T>(false, default, message, errorMessage);
        }
    }
}
