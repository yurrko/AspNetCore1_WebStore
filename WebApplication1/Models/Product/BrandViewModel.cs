using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.Models.Product
{
    public class BrandViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProductsCount { get; set; }
        public int Order { get; set; }
    }
}
