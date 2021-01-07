using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace DTO_Highland
{
    public class BillUpDTO : BillDTO
    {
        public double CustomerPrice { get; set; }

        public double OutPrice { get; set; }

        public double PromotionPrice { get; set; }

        public double Revenue { get; set; }

        public BillUpDTO(DataRow row) : base(row)
        {
            PromotionPrice = (double)row["PROMOTIONPRICE"];
            CustomerPrice = (double)row["CUSTOMERPRICE"];
            OutPrice = (double)row["OUTPRICE"];
            Revenue = (double)row["REVENUE"];
        }
    }
}
