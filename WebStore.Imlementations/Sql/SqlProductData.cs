using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
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

        public IEnumerable<Section> GetSections()
        {
            return _context.Sections.ToList();


        }
        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public IEnumerable<Product> GetProducts( ProductFilter filter )
        {
            var query = _context.Products.Include( "Brand" ).Include( "Section" ).AsQueryable();

            if ( filter.BrandId.HasValue )
                query = query.Where( c => c.BrandId.HasValue && c.BrandId.Value.Equals( filter.BrandId.Value ) );
            if ( filter.SectionId.HasValue )
                query = query.Where( c => c.SectionId.Equals( filter.SectionId.Value ) );
            return query.ToList();
        }


        public Product GetProductById( int id )
        {
            return _context.Products.Include( "Brand" ).Include( "Section" ).FirstOrDefault( p => p.Id.Equals( id ) );
        }

        public void SaveChanges( Product product )
        {
            var old = _context.Products.Find( product.Id );
            old.Name = product.Name;
            old.Order = product.Order;
            old.Price = product.Price;
            old.ImageUrl = product.ImageUrl;

            _context.SaveChanges();
        }

        public void AddProduct (Product product)
        {
            _context.Products.Add( product );
            _context.SaveChanges();
        }

        public void Delete( int id )
        {
            var prodtoDelete = _context.Products.Find( id );
            if ( !(prodtoDelete is null) )
            {
                _context.Products.Remove( prodtoDelete );

                _context.SaveChanges();
            }
        }
    }
}
