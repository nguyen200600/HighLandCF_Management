using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DTO_Highland
{
    public class ProductDTO
    {
        public int ID { get; set; }

        public int IDTypeProduct { get; set; }

        public string NameProducts { get; set; }

        public double PriceBasic { get; set; }
        public double SalePrice { get; set; }
        public string Size { get; set; }
        public int Status { get; set; }

        public ProductDTO()
        {
        }

        public ProductDTO(DataRow row)
        {
            ID = (int)row["ID"];
            NameProducts = row["NameProducts"].ToString();
            PriceBasic = (double)row["PriceBasic"];
            SalePrice = (double)row["SalePrice"];
            Status = (int)row["Status"];
            IDTypeProduct = (int)row["IDTypeDink"];
            Size = row["Size"].ToString();
        }
    }
}
