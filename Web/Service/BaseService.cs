using Newtonsoft.Json;
using System.Text;
using Web.Models;
using Web.Service.IService;
using static Web.Utility.SD;

namespace Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto,bool withBearer = true)
        {
            try
            {
                HttpClient Client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                //token
                if (withBearer )
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? ApiResponse = null;

                switch (requestDto.ApiType)
                {
                    case ApyType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApyType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApyType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break; ;
                    default: message.Method = HttpMethod.Get; break;
                }

                ApiResponse = await Client.SendAsync(message);

                switch (ApiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var ApiContent = await ApiResponse.Content.ReadAsStringAsync();
                        var ApiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(ApiContent);
                        return ApiResponseDto;
                }
            }
            catch (Exception Ex)
            {
                var Dto = new ResponseDto
                {
                    IsSuccess = false,
                    Message = Ex.Message.ToString(),
                };
                return Dto;
            }
            
        }
    }
}
