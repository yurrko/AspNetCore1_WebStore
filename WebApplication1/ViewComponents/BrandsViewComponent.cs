using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Models.Product;
using WebStore.Interfaces;

namespace WebStore.ViewComponents
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;
        public BrandsViewComponent( IProductData productData )
        {
            _productData = productData;
        }
        public IViewComponentResult Invoke()
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
