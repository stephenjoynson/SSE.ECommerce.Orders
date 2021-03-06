using System.Threading.Tasks;
using SSE.ECommerce.Orders.Data.DTO;

namespace SSE.ECommerce.Orders.Data.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerDetails(string email);
    }
}