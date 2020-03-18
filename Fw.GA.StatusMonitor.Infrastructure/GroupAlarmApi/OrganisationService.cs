using FW.GA.StatusMonitor.Core.Interfaces;
using FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace Fw.GA.StatusMonitor.Infrastructure.GroupAlarmApi
{
    public class OrganisationService : IOrganizationService
    {
        private readonly string _webServiceBaseUrl;
        private readonly string _webApiKey;
        private readonly string _personalAccessToken;

        public OrganisationService(string webServiceBaseUrl, string webApiKey, string personalAccessToken)
        {
            _webServiceBaseUrl = webServiceBaseUrl;
            _webApiKey = webApiKey;
            _personalAccessToken = personalAccessToken;
        }

        public OrganizationStructure Get()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("API-KEY", _webApiKey);
                client.DefaultRequestHeaders.Add("Personal-Access-Token", _personalAccessToken);

                using (var request = new HttpRequestMessage(HttpMethod.Get, $"{_webServiceBaseUrl}/organization/6371/children"))
                using (var response = client.SendAsync(request).GetAwaiter().GetResult())
                {
                    response.EnsureSuccessStatusCode();

                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<OrganizationStructure>(content) ?? new OrganizationStructure();
                }
            }
        }

        public List<Label> AllLabels(int organizationId)
        {

            return new List<Label>();
        }
    }
}
