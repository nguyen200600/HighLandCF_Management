using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DTO_Highland
{
    public class InventoryDTO
    {
        public DateTime CreatedDate { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public double PriceBase { get; set; }

        public InventoryDTO()
        {
        }

        public InventoryDTO(DataRow row)
        {
            ID = (int)row["ID"];
            PriceBase = row["PriceBase"] == DBNull.Value ? 0 : (double)row["PriceBase"];
            CreatedDate = (DateTime)row["CREATEDATE"];
            Name = row["Name"].ToString();
            Note = row["Note"].ToString();
        }
    }
}
