using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Dto.Product;
using WebStore.Domain.Entities;
using WebStore.Domain.Filters;

namespace WebStore.Interfaces
{
    public interface IProductData
    {
        /// <summary>
        /// Список секций
        /// </summary>
        /// <returns></returns>
        IEnumerable<SectionDto> GetSections();

        /// <summary>
        /// Список брендов
        /// </summary>
        /// <returns></returns>
        IEnumerable<BrandDto> GetBrands();

        /// <summary>
        /// Список товаров
        /// </summary>
        /// <param name="filter">Фильтр товаров</param>
        /// <returns></returns>
        IEnumerable<ProductDto> GetProducts( ProductFilter filter );

        /// <summary>
        /// Продукт
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сущность Product, если нашёл, иначе null</returns>
        ProductDto GetProductById( int id );
    }
}
