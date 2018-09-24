using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TinkoffWinApp.Models.ResponceModels;
using TinkoffWinApp.Support;
using Windows.Web.Http;

namespace TinkoffWinApp.Managers
{
    public interface IRequestManager
    {
        Task<ProductsResponceModel> LoadProducts();
    }

    public class RequestManager : IRequestManager
    {
        public async Task<ProductsResponceModel> LoadProducts()
        {
            return await SendRequest<ProductsResponceModel>("https://config.tinkoff.ru/resources?name=products_info");
        }

        private async Task<string> StartRequest(string address)
        {
            string httpResponseBody = "";
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var httpResponse = await httpClient.GetAsync(new Uri(address));
                    httpResponse.EnsureSuccessStatusCode();
                    httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                }
                catch
                {
                    ContentDialogHelper.ShowMessage("Ошибка при получении данных", "Произошла ошибка при получении данных, пожалуйста, попробуйте еще раз.");
                    httpResponseBody = "";
                }
            }
            return httpResponseBody;
        }

        private async Task<T> SendRequest<T>(string address)
            where T : IResponce, new()
        {
            var responce = await StartRequest(address);
            if (string.IsNullOrEmpty(responce))
            {
                return new T()
                {
                    Success = false
                };
            }
            try
            {
                var responceDeserialize = await Task.Run(() => Serializer.Deserialize<T>(responce));
                return responceDeserialize;
            }
            catch
            {
                ContentDialogHelper.ShowMessage("Получены неверные данные", "Загружены ошибочные данные, пожалуйста, попробуйте снова.");
                return new T()
                {
                    Success = false
                };
            }
        }
    }

    internal static class Serializer
    {
        internal static T Deserialize<T>(string json)
        {
            var res = JsonConvert.DeserializeObject<T>(json);
            return res;
        }
    }    
}
