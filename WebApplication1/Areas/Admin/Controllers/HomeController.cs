﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Filters;
using WebStore.infrastructure.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area( "Admin" ), Authorize( Roles = "Administrator" )]
    public class HomeController : Controller
    {
        private readonly IProductData _productData;

        public HomeController( IProductData productData )
        {
            _productData = productData;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList()
        {
            var products = _productData.GetProducts( new ProductFilter() );
            return View( products );
        }
    }
}