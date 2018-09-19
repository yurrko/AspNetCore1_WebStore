﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.infrastructure.Interfaces;
using WebStore.Models.Product;

namespace WebStore.ViewComponents
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;
        public BrandsViewComponent( IProductData productData )
        {
            _productData = productData;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = GetBrands();
            return View( brands );
        }
        private IEnumerable<BrandViewModel> GetBrands()
        {
            var dbBrands = _productData.GetBrands();
            return dbBrands.Select( b => new BrandViewModel
            {
                Id = b.Id ,
                Name = b.Name ,
                Order = b.Order ,
                ProductsCount = 0
            } ).OrderBy( b => b.Order ).ToList();
        }
    }
}
