using CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification( ProductSpecParams specParams )
            : base(x=>
                   (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search))
                   && (specParams.Brand.Count == 0 || specParams.Brand.Contains(x.Brand))
                   &&(specParams.Type.Count == 0 || specParams.Type.Contains(x.Type))
            )
        {
            ApplyingPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            switch (specParams.Sort)
            {
                case "priceAsc": 
                    AddOrderBy(x => x.Price);
                    break;
                case "priceDesc": 
                    AddOrderByDesc(x => x.Price);
                    break;
                default:
                    AddOrderBy(x => x.Name); 
                    break;
            }

        }
    }
}
