using CtcontrolAPIService.Services;
using CtcontrolMobile.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CtcontrolMobile.Service
{
    class ClientDataService
    {
        private readonly string Uri = ConnectionData.ClientUri;

        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            return client;
        }

        public async Task<List<ClientDataModel>> Get()
        {
            var client = GetClient();
            var result = await client.GetStringAsync(Uri);
            return JsonSerializer.Deserialize<List<ClientDataModel>>(result, options);
        }

        public async Task<ClientDataModel> Get(string id)
        {
            var client = GetClient();
            var result = await client.GetStringAsync(Uri + id);
            return JsonSerializer.Deserialize<ClientDataModel>(result, options);
        }
    }
}
