using WebStore.Domain.Dto.Product;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Dto.Product
{
    public class ProductDto : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public BrandDto Brand { get; set; }
        public SectionDto Section { get; set; }
    }
}
