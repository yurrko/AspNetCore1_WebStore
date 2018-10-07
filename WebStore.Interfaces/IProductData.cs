using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities;
using WebStore.Domain.Filters;

namespace WebStore.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts( ProductFilter filter );
        Product GetProductById( int id );
        void SaveChanges(Product product);
        void Delete( int id );
        void AddProduct( Product product );
    }
}
