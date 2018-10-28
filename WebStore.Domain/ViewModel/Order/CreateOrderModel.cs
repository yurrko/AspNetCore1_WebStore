using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Dto.Order;

namespace WebStore.Domain.ViewModel.Order
{
    public class CreateOrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
