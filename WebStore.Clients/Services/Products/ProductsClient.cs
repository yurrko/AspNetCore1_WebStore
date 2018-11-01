using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using WebStore.Clients.Base;
using WebStore.Domain.Dto.Product;
using WebStore.Domain.Entities;
using WebStore.Domain.Filters;
using WebStore.Interfaces;

namespace WebStore.Clients.Services.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient( IConfiguration configuration ) : base( configuration )
        {
            ServiceAddress = "api/products";
        }

        protected sealed override string ServiceAddress { get; set; }

        public IEnumerable<SectionDto> GetSections()
        {
            var url = $"{ServiceAddress}/sections";
            var result = Get<List<SectionDto>>( url );
            return result;
        }

        public SectionDto GetSectionById( int id )
        {
            var url = $"{ServiceAddress}/sections/{id}";
            var result = Get<SectionDto>( url );
            return result;
        }

        public IEnumerable<Brand> GetBrands()
        {
            var url = $"{ServiceAddress}/brands";
            var result = Get<List<Brand>>( url );
            return result;

        }

        public Brand GetBrandById( int id )
        {
            var url = $"{ServiceAddress}/brands/{id}";
            var result = Get<Brand>( url );
            return result;
        }

        public PagedProductDto GetProducts( ProductFilter filter )
        {
            var url = $"{ServiceAddress}";
            var response = Post( url, filter );
            var result = response.Content.ReadAsAsync<PagedProductDto>().Result;
            return result;
        }

        public ProductDto GetProductById( int id )
        {
            var url = $"{ServiceAddress}/{id}";
            var result = Get<ProductDto>( url );
            return result;
        }
    }
}
