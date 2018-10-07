using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain.Models.Cart;
using WebStore.Domain.Models.Order;

namespace WebStore.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetUserOrders( string userName );

        Order GetOrderById( int id );

        Order CreateOrder( OrderViewModel orderModel, CartViewModel transformCart, string userName );
    }
}
