namespace AssetManagementBlazor.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public bool Success => ErrorMessage == null;

        public ApiResponse(T data)
        {
            Data = data;
            ErrorMessage = null;
        }
        public ApiResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Data = default;
        }
        public ApiResponse() { }
    }
}
