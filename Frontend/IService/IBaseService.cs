using Frontend.Models;

namespace Frontend.IService
{
    public interface IBaseService
    {
      public Task<ResponseModel> SendAsync(RequestModel request);
    }
}
