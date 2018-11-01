using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain.Dto.Product;
using WebStore.Domain.Entities;
using WebStore.Domain.Filters;
using WebStore.Interfaces;


namespace WebStore.ServicesHosting.Controllers
{
    [Produces( "application/json" )]
    [Route( "api/products" )]
    public class ProductsApiController : Controller, IProductData
    {
        private readonly IProductData _productData;

        public ProductsApiController( IProductData productData )
        {
            _productData = productData;
        }

        [HttpGet( "sections" )]//GET api/products/sections
        public IEnumerable<SectionDto> GetSections()
        {
            return _productData.GetSections();
        }

        [HttpGet( "sections/{id}" )]
        public SectionDto GetSectionById( int id )
        {
            return _productData.GetSectionById( id );
        }

        [HttpGet( "brands" )]//GET api/products/brands
        public IEnumerable<Brand> GetBrands()
        {
            return _productData.GetBrands();
        }

        [HttpGet( "brands/{id}" )]
        public Brand GetBrandById( int id )
        {
            return _productData.GetBrandById( id );
        }

        [HttpPost]//POST api/products
        public PagedProductDto GetProducts( [FromBody]ProductFilter filter )
        {
            return _productData.GetProducts( filter );
        }

        [HttpGet( "{id}" )]//GET api/products/{id}
        public ProductDto GetProductById( int id )
        {
            return _productData.GetProductById( id );
        }
    }
}