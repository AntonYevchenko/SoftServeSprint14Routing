using ProductsWithRouting.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductsWithRouting.Services
{
    public class FilterProductsService
    {
        public IEnumerable<Product> FilterProducts(IEnumerable<Product> products, int? filterId, string filterName)
        {
            IEnumerable<Product> filteredProducts = products;

            if (!string.IsNullOrEmpty(filterName) || filterId.HasValue)
            {
                filteredProducts = products.Where(p =>
                    (!filterId.HasValue || p.Id == filterId) &&
                    (string.IsNullOrEmpty(filterName) || p.Name.Contains(filterName)));
            }

            return filteredProducts;
        }
    }
}
