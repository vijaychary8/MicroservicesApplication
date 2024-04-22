namespace AuthApi.Models
{
    public class ResponseModel
    {
        public object? Result { get; set; }
        public Boolean IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}
