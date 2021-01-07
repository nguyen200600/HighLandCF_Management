using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DTO_Highland
{
    public class DetailBillDTO
    {
        public int IDbill { get; set; }

        public int IDProduct { get; set; }

        public int Quantity { get; set; }

        public DetailBillDTO(DataRow row)
        {
            IDbill = (int)row["IDBill"];
            IDProduct = (int)row["IDProduct"];
            Quantity = (int)row["Quantity"];
        }
    }
}
