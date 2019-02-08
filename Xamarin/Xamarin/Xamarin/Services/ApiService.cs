using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Model;
using Plugin.Connectivity;
using System.Net.Http;
using Newtonsoft.Json;

namespace Xamarin.Services
{
    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSucces = false,
                    Message = "Por favor conecte a internet"
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!isReachable)
            {
                return new Response
                {
                    IsSucces = false,
                    Message = "Verifique sua conexão com a internet"
                };
            }

            return new Response
            {
                IsSucces = true,
                Message = "Ok"
            };

        }

        public async Task<Response> GetList<T>(string urlBase, string servicePrefix,
            string controller)
        {
            try
            {
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await cliente.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSucces = false,
                        Message = result
                    };
                }
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSucces = true,
                    Message = "Ok",
                    Result = result
                };
            }
            catch ( Exception sms)
            {
                return new Response
                {
                    IsSucces = false,
                    Message = sms.Message
                };
            }
        }
    }
}
