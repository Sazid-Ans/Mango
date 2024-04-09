using MangoWeb.Models;
using MangoWeb.Service.Iservice;
using MangoWeb.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace MangoWeb.BaseService
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
        public async Task<ResponseDto?> SendAsync(RequestDto request , bool withBearer)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("mangoAPI");
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpRequestMessage.Headers.Add("Accept", "Application/Json");
            if (withBearer)
            {
                var token = _tokenProvider.GetToken();
                httpRequestMessage.Headers.Add("Authorization", $"Bearer {token}");
            }
            httpRequestMessage.RequestUri = new Uri(request.URI);
            if (request.Data != null)
            {
                httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(request.Data)
                                   , Encoding.UTF8, "application/json");
            }

            switch (request.ApiType)
            {
                case SD.ApiType.Post:
                    httpRequestMessage.Method = HttpMethod.Post;
                    break;
                case SD.ApiType.Put:
                    httpRequestMessage.Method = HttpMethod.Put;
                    break;
                case SD.ApiType.Delete:
                    httpRequestMessage.Method = HttpMethod.Delete;
                    break;
                default:
                    httpRequestMessage.Method = HttpMethod.Get;
                    break;
            }

            HttpResponseMessage ApiResponse = await httpClient.SendAsync(httpRequestMessage);
            var response = await ApiResponse.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ResponseDto>(response);
            try
            {
                switch (ApiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDto {status= (int)ApiResponse.StatusCode, isSuccess = false , message = "NotFound" };

                    case HttpStatusCode.Forbidden:
                        return new ResponseDto {status = (int)ApiResponse.StatusCode, isSuccess = false , message = "Forbidden" };

                    case HttpStatusCode.Unauthorized:
                        return new ResponseDto {status = (int)ApiResponse.StatusCode, isSuccess = false , message = "Unauthorized"};

                    case HttpStatusCode.InternalServerError:
                        return new ResponseDto {status = (int)ApiResponse.StatusCode, isSuccess = false , message = "InternalServerError" };
                    case HttpStatusCode.BadRequest:
                        return new ResponseDto {status = (int)ApiResponse.StatusCode, isSuccess = false , message = "BadRequest" }; //message = res.message // if not given ? it will throw nullrefrence exception when res is null;

                    default:
                        //var response = await ApiResponse.Content.ReadAsStringAsync();
                        //var res = JsonConvert.DeserializeObject<ResponseDto>(response);
                        return res;
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto { isSuccess = false, message = ex.Message };
            }

        }
    }
}
