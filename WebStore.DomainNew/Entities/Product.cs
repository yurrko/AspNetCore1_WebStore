using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.DomainNew.Entities
{
    [Table ( "Products" )]
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        [ForeignKey( "SectionId" )]
        public int SectionId { get; set; }
        public int? BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
