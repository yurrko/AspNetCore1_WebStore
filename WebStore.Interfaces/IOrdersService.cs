using System.Collections.Generic;
using WebStore.Domain.Dto.Order;
using WebStore.Domain.ViewModel.Order;

namespace WebStore.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<OrderDto> GetUserOrders( string userName );
        OrderDto GetOrderById( int id );
        OrderDto CreateOrder( CreateOrderModel orderModel, string userName );
    }
}
