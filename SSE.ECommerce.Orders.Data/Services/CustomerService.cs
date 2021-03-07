using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SSE.ECommerce.Orders.Data.DTO;
using SSE.ECommerce.Orders.Data.Interfaces;
using SSE.ECommerce.Orders.Data.Models;

namespace SSE.ECommerce.Orders.Data.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CustomerService> _logger;
        private readonly IConfiguration _configuration;

        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<CustomerDto> GetCustomerDetails(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email), "No value supplied for argument");
            }

            _logger.LogInformation($"About to call Customer API for {email}");
            var customerClient = _httpClientFactory.CreateClient("Customer");
            var requestUri = $"api/GetUserDetails?code={_configuration["OrdersSettings:CustomerApiKey"]}";
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
