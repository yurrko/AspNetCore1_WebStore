﻿using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Models.Cart;
using WebStore.Models.Order;

namespace WebStore.infrastructure.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetUserOrders( string userName );

        Order GetOrderById( int id );

        Order CreateOrder( OrderViewModel orderModel, CartViewModel transformCart, string userName );
    }
}
