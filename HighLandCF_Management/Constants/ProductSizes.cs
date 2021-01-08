using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighLandCF_Management.Constants
{
    public class ProductSize
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
    public class ProductSizes
    {
        public static List<ProductSize> List() => new List<ProductSize>
        {
            new ProductSize { Id = "S" , Name = "Nhỏ"},
            new ProductSize { Id = "M" , Name = "Vừa"},
            new ProductSize { Id = "L" , Name = "Lớn"}
        };
    }
}
