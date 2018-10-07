using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Filters;
using WebStore.Domain.Models.Product;
using WebStore.Interfaces;

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

        public IActionResult Edit( int? id )
        {
            Product product;
            if ( id.HasValue )
            {
                product = _productData.GetProductById( id.Value );
                if ( product == null )
                    return NotFound();
            }
            else
            {
                product = new Product();
            }

            return View( product );
        }

        public IActionResult Create()
        {
            return RedirectToAction( nameof( Edit ) );
        }

        [HttpPost]
        public IActionResult Edit( ProductViewModel model )
        {
            if ( !ModelState.IsValid )
                return View( model );
            if ( model.Id > 0 )
            {
                var dbItem = _productData.GetProductById( model.Id );
                if ( dbItem is null )
                    return NotFound();

                dbItem.Name = model.Name;
                dbItem.Order = model.Order;
                dbItem.ImageUrl = model.ImageUrl;
                dbItem.Price = model.Price;

                _productData.SaveChanges( dbItem );
            }
            else
            {
                var newProd = new Product
                {
                    Name = model.Name,
                    Order = model.Order,
                    BrandId = 1,
                    SectionId = 1,
                    ImageUrl = model.ImageUrl,
                    Price = model.Price,
                };

                _productData.AddProduct( newProd );
            }
            return RedirectToAction( nameof( ProductList ) );
        }

        public IActionResult Details( int id )
        {
            var product = _productData.GetProductById( id );
            if ( product == null )
                return NotFound();

            return View( product );
        }

        public IActionResult Delete( int id )
        {
            _productData.Delete( id );
            return RedirectToAction( nameof( ProductList ) );
        }
    }
}