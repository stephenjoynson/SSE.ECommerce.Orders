using System.Threading.Tasks;
using SSE.ECommerce.Orders.Domain.Models;

namespace SSE.ECommerce.Orders.Domain.Interfaces
{
    public interface ICustomerManager
    {
        Task<Customer> GetCustomerDetails(string email);
    }
}