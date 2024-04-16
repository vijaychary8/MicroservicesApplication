using Frontend.IService;
using Frontend.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using static Frontend.Utility.StaticDetails;

namespace Frontend.Service
{
    public class BaseService:IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory) { 
        _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> SendAsync(RequestModel request)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("employeeApi");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                //token


                message.RequestUri = new Uri(request.Url);
                if (request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");

                }
                HttpResponseMessage apiResponse = null;


                switch (request.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                }
                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Acces Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unautherization" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseModel = JsonConvert.DeserializeObject<ResponseModel>(apiContent);
                        return apiResponseModel;

                }
            }catch (Exception ex)
            {
                var response = new ResponseModel
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return response;
            }

        }
    }
}
