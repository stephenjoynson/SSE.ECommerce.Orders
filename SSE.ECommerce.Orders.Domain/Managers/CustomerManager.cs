﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SSE.ECommerce.Orders.Data.Interfaces;
using SSE.ECommerce.Orders.Domain.Interfaces;
using SSE.ECommerce.Orders.Domain.Models;

namespace SSE.ECommerce.Orders.Domain.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;

        public CustomerManager(ICustomerService customerService, ILogger logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        public async Task<Customer> GetCustomerDetails(string email)
        {
            _logger.LogInformation("Start of GetCustomerDetails()");
            var customerDetails = await _customerService.GetCustomerDetails(email);
        }
    }
}
