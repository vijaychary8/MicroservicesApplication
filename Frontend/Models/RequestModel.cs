using static Frontend.Utility.StaticDetails;

namespace Frontend.Models
{
    public class RequestModel
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string? Url { get; set; }
        public object Data { get; set; }
        public string? AccesToken { get; set; }
    }
}
