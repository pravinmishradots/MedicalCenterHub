using ChildCare.DTOs;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using NuGet.Protocol;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ChildCare.Services
{
    public interface IHttpService
    {
        HttpClient? Client { get; set; }
        Task<BaseApiResponseModel<T>> GetAsync<T>(string uri);
        Task<BaseApiResponseModel<T>> GetAsync<T>(string uri, IDictionary<string, string> queryParams);
        Task<BaseApiResponseModel<T>> PostAsync<T>(string uri, object value);
        Task<BaseApiResponseModel<T>> PostMultipartAsync<T>(string uri, MultipartContent content);

        Task<BaseApiResponseModel<T>> DeleteAsync<T>(string uri);
        string AccessToken { get; set; }
    }

    public class HttpService : IHttpService
    {


        public HttpClient? Client { get; set; }

        public string AccessToken { get; set; }


        public async Task<BaseApiResponseModel<T>> GetAsync<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendRequest<T>(request);
        }

        public async Task<BaseApiResponseModel<T>> GetAsync<T>(string uri, IDictionary<string, string> queryParams)
        {
            uri = QueryHelpers.AddQueryString(uri, queryParams!);

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendRequest<T>(request);
        }

        public async Task<BaseApiResponseModel<T>> PostAsync<T>(string uri, object value)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");

            return await SendRequest<T>(request);
        }


        public async Task<BaseApiResponseModel<T>> PostMultipartAsync<T>(string uri, MultipartContent content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = content;
            return await SendRequest<T>(request);
        }

        public async Task<BaseApiResponseModel<T>> DeleteAsync<T>(string uri)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent("application/json");

            return await SendRequest<T>(request);

        }



        // helper methods

        private async Task<BaseApiResponseModel<T>> SendRequest<T>(HttpRequestMessage request)
        {
            try
            {
                var datatoken = this.AccessToken!;
                if (datatoken != null)
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", datatoken);
                if (Client == null)
                    throw new InvalidOperationException("The http client property can't be null.");
                var response = await Client.SendAsync(request);
                // auto logout on 401 response
                // throw exception on error response
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                    throw new Exception(error!["message"]);
                }


                var result = await response.Content.ReadFromJsonAsync<BaseApiResponseModel<T>>();

                if (result == null)
                    throw new InvalidCastException();

                return result;

            }
            catch (HttpRequestException ex)
            {
                return new BaseApiResponseModel<T> { StatusCode = ex.StatusCode, Message = (ex.InnerException ?? ex).Message };
            }
            catch (Exception ex)
            {
                return new BaseApiResponseModel<T> { StatusCode = HttpStatusCode.InternalServerError, Message = (ex.InnerException ?? ex).Message };
            }
        }
    }
}

