using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;                                            // Dal = Data Access Layer  ------ Veritabanı operayonlarını içerir...
using System.Collections.Generic;                        // Veri erişim işlerini yapan bir interface.. 
using System.Linq;                                       // Her Entity diğer katmanlarda kodlanır..
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetails();



    }
}
