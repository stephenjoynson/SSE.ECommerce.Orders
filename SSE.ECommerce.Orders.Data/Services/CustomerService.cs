using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SSE.ECommerce.Orders.Data.DTO;
using SSE.ECommerce.Orders.Data.Interfaces;

namespace SSE.ECommerce.Orders.Data.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public CustomerService(IHttpClientFactory httpClientFactory, ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<CustomerDto> GetCustomerDetails(string email)
        {
            _logger.LogInformation($"About to call Customer API for {email}");
            var customerClient = _httpClientFactory.CreateClient("Customer");
            var httpRequestMessage = new HttpRequestMessage();
            var httpResponseMessage = await customerClient.SendAsync(httpRequestMessage);
            var customerDetails = JsonConvert.DeserializeObject<CustomerDto>(httpResponseMessage.Content.ToString());
            return customerDetails;
        }
    }
}
