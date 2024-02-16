using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using three.Models;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;


namespace three.API
{
    internal class ApiService
    {
        HttpClient client;

        public ApiService()
        {
            client = new HttpClient();
        }

        public async Task<ObservableCollection<Products>> GetProducts()
        {
            ObservableCollection<Products> items = null;

            try
            {
                var response = await client.GetAsync("http://10.0.2.2:54552/api/Imgs");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<ObservableCollection<Products>>(content);
                    return items;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return null;
        }

        public async Task<bool> AddProducts(Products item)
        {

            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent sContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://10.0.2.2:54552/api/Imgs", sContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<bool> DeleteProducts(int ImgId)
        {
            try
            {
                string apiUrl = $"http://10.0.2.2:54552/api/Imgs/{ImgId}";

                var response = await client.DeleteAsync(apiUrl);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

    }
}
