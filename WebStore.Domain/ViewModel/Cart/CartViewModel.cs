using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.ViewModel.Product;

namespace WebStore.Domain.ViewModel.Cart
{
    public class CartViewModel
    {
        public Dictionary<ProductViewModel, int> Items { get; set; }

        public int ItemsCount
        {
            get
            {
                return Items?.Sum( x => x.Value ) ?? 0;
            }
        }

    }
}
