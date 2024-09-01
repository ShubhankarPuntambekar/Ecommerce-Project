namespace API.Errors
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int code, string message, string? details)
        {
            StatusCode = code;
            ErrorMessage = message;
            Details = details;
        }
        public int StatusCode {  get; set; }
        public string ErrorMessage { get; set; }
        public string? Details { get; set; }
    }
}
