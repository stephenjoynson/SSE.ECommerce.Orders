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
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<CustomerDto> GetCustomerDetails(string email)
        {
            _logger.LogInformation($"About to call Customer API for {email}");
            var customerClient = _httpClientFactory.CreateClient("Customer");
            var requestUri = "api/GetUserDetails?code=uu2ToG/dcsg3DI8CGlpLro1PyLhZNUWHpdPv8VmWFLBaxM0fvUZvkA==";
            var customerRequest = new CustomerRequest
            {
                Email = email
            };
            var httpResponseMessage = await customerClient.PostAsync(requestUri, new StringContent(JsonConvert.SerializeObject(customerRequest)));
            var httpResponseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var customerDetails = JsonConvert.DeserializeObject<CustomerDto>(httpResponseString);
            return customerDetails;
        }

    }
}
