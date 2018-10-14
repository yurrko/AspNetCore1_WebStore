using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain.Dto.Product;
using WebStore.Domain.Filters;
using WebStore.Interfaces;

namespace WebStore.Implementations.Sql
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _context;

        public SqlProductData( WebStoreContext context )
        {
            _context = context;
        }

        public IEnumerable<SectionDto> GetSections()
        {
            return _context.Sections.Select( s => new SectionDto()
            {
                Id = s.Id,
                Name = s.Name,
                Order = s.Order,
                ParentId = s.ParentId
            } ).ToList();
        }

        public IEnumerable<BrandDto> GetBrands()
        {
            return _context.Brands.Select( b => new BrandDto()
            {
                Id = b.Id,
                Name = b.Name,
                Order = b.Order
            } ).ToList();
        }

        public IEnumerable<ProductDto> GetProducts( ProductFilter filter )
        {
            var query = _context.Products.Include( "Brand" ).Include( "Section" ).AsQueryable();
            if ( filter.BrandId.HasValue )
                query = query.Where( c => c.BrandId.HasValue && c.BrandId.Value.Equals( filter.BrandId.Value ) );
            if ( filter.SectionId.HasValue )
                query = query.Where( c => c.SectionId.Equals( filter.SectionId.Value ) );
            return query.Select( p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Brand = p.BrandId.HasValue ? new BrandDto() { Id = p.Brand.Id, Name = p.Brand.Name, Order = p.Order } : null
            } ).ToList();
        }

        public ProductDto GetProductById( int id )
        {
            var product = _context.Products.Include( "Brand" ).Include( "Section" ).FirstOrDefault( p => p.Id.Equals( id ) );
            if ( product == null )
                return null;

            var dto = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Order = product.Order,
                Price = product.Price
            };
            if ( product.Brand != null )
                dto.Brand = new BrandDto()
                {
                    Id = product.Brand.Id,
                    Name = product.Brand.Name,
                    Order = product.Brand.Order
                };
            return dto;
        }
    }
}
