using System;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SSE.ECommerce.Orders.Data.DTO;
using SSE.ECommerce.Orders.Data.Interfaces;
using SSE.ECommerce.Orders.Domain.Interfaces;
using SSE.ECommerce.Orders.Domain.Managers;

namespace SSE.ECommerce.Orders.Test.Domain
{
    [TestFixture]
    public class CustomerManagerTests
    {

        private Mock<ILogger<CustomerManager>> _mockLogger;
        private Mock<ICustomerService> _mockCustomerService;
        private ICustomerManager _customerManager;
        private const string Email = "arthur.carter@reality.com";

        private CustomerDto _customer = new CustomerDto
        {
            Email = Email,
            CustomerId = "X123456",
            Website = "www.arthurcarter.com",
            FirstName = "Arthur",
            LastName = "Carter",
            LastLoggedIn = "15-Dec-2020",
            HouseNumber = "4",
            Street = "Lady Street",
            Town = "London",
            Postcode = "N5 6TE",
            PreferredLanguage = "en-GB"
        };

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<CustomerManager>>();
            _mockCustomerService = new Mock<ICustomerService>();
            _mockCustomerService.Setup(m => m.GetCustomerDetails(Email)).ReturnsAsync(_customer);
            _customerManager = new CustomerManager(_mockCustomerService.Object, _mockLogger.Object);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void When_Email_Is_Not_Supplied__Then_GetCustomerDetails_Throws_ArgumentNullException(string email)
        {
            // Arrange

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _customerManager.GetCustomerDetails(email));
        }

        [Test]
        public void When_Email_Is_Supplied__Then_GetCustomerDetails_Calls_Service_Once()
        {
            // Arrange

            // Act
            _customerManager.GetCustomerDetails(Email);

            // Assert
            _mockCustomerService.Verify(m => m.GetCustomerDetails(Email), Times.Once);
        }

        [Test]
        public void When_Email_Is_Supplied__Then_GetCustomerDetails_Returns_Customer()
        {
            // Arrange

            // Act
            var customerManagerResponse = _customerManager.GetCustomerDetails(Email).Result;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(_customer.CustomerId, customerManagerResponse.CustomerId);
                Assert.AreEqual(_customer.FirstName, customerManagerResponse.FirstName);
                Assert.AreEqual(_customer.LastName, customerManagerResponse.LastName);
                Assert.AreEqual(_customer.HouseNumber, customerManagerResponse.HouseNumber);
                Assert.AreEqual(_customer.Street, customerManagerResponse.Street);
                Assert.AreEqual(_customer.Town, customerManagerResponse.Town);
                Assert.AreEqual(_customer.Postcode, customerManagerResponse.Postcode);
            });
        }
    }
}