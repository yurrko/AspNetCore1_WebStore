 using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Dto.Product
{
    public class BrandDto : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
    }
}
