using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DTO_Highland
{
    public class MenuDTO
    {
        public int ID { get; set; }

        public int IdProduct { get; set; }

        public string NameProduct { get; set; }

        public double PriceBasic { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public double TotalPrice { get; set; }

        public MenuDTO(DataRow row)
        {
            ID = (int)row["ID"];
            NameProduct = row["NameProducts"].ToString();
            Quantity = (int)row["Quantity"];
            PriceBasic = (double)row["PriceBasic"];
            TotalPrice = (double)row["TotalPrice"];
            IdProduct = (int)row["IDProduct"];
            Size = row["Size"]?.ToString();
        }
    }
}
