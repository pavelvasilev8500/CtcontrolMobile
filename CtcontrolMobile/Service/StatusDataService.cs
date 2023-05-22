using CtcontrolAPIService.Models;
using CtcontrolMobile.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CtcontrolMobile.Service
{
    class StatusDataService
    {

        private readonly string Uri = ConnectionData.StatusUri;

        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            return client;
        }

        public async Task<List<StatusDataModel>> Get()
        {
            var client = GetClient();
            var result = await client.GetStringAsync(Uri);
            return JsonSerializer.Deserialize<List<StatusDataModel>>(result, options);
        }

        public async Task<StatusDataModel> Get(string id)
        {
            var client = GetClient();
            var result = await client.GetStringAsync(Uri + id);
            if (result == "")
                return new StatusDataModel { Id = "?"};    
            return JsonSerializer.Deserialize<StatusDataModel>(result, options);
        }

        public async Task<StatusDataModel> Put(string id,  StatusDataModel statusDataModel)
        {
            var client = GetClient();
            var response = await client.PutAsync(Uri + id, new StringContent(JsonSerializer.Serialize(statusDataModel), Encoding.UTF8, "application/json"));
            if(response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;
            return JsonSerializer.Deserialize<StatusDataModel>(await response.Content.ReadAsStringAsync(), options);
        }

    }
}
