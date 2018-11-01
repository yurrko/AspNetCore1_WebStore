using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain.Dto.Order;
using WebStore.Domain.ViewModel.Cart;
using WebStore.Domain.ViewModel.Order;
using WebStore.Interfaces;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrdersService _ordersService;

        public CartController( ICartService cartService, IOrdersService ordersService )
        {
            _cartService = cartService;
            _ordersService = ordersService;
        }

        public IActionResult Details()
        {
            var model = new DetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            };
            return View( model );
        }

        public IActionResult DecrementFromCart( int id )
        {
            _cartService.DecrementFromCart( id );
            return Json( new { id, message = "Количество товара уменьшено на 1" } );
        }

        public IActionResult RemoveFromCart( int id )
        {
            _cartService.RemoveFromCart( id );
            return Json( new { id, message = "Товар удален из корзины" } );
        }

        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            return RedirectToAction( "Details" );
        }

        public IActionResult AddToCart( int id)
        {
            _cartService.AddToCart( id );
            return Json( new { id, message = "Товар добавлен в корзину" } );
        }

        public IActionResult GetCartView()
        {
            return ViewComponent( "Cart" );
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CheckOut( OrderViewModel model )
        {
            if ( ModelState.IsValid )
            {

                CartViewModel orderItems = _cartService.TransformCart();

                List<OrderItemDto> itemsInOrder = new List<OrderItemDto>();

                foreach ( var item in orderItems.Items )
                {
                    itemsInOrder.Add( new OrderItemDto
                    {
                        Id = item.Key.Id,
                        Price = item.Key.Price,
                        Quantity = item.Value,
                    } );
                }

                var orderResult = _ordersService.CreateOrder( new CreateOrderModel
                {
                    OrderItems = itemsInOrder,
                    OrderViewModel = model,
                }, User.Identity.Name );
                _cartService.RemoveAll();
                return RedirectToAction( "OrderConfirmed", new { id = orderResult.Id } );

            }
            var detailsModel = new DetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = model
            };
            return View( "Details", detailsModel );
        }

        public IActionResult OrderConfirmed( int id )
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}