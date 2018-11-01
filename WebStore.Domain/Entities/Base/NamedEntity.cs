using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    public class NamedEntity : BaseEntity, INamedEntity
    {
        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}
