using System;
using System.Collections.Generic;
using System.Text;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;

namespace WebStore.infrastructure.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts( ProductFilter filter );
    }
}
