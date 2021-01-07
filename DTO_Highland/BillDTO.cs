using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace DTO_Highland
{
    public class BillDTO
    {
        public DateTime CreateDay { get; set; }

        public int Employ { get; set; }

        public int ID { get; set; }

        public int Idtable { get; set; }

        public int Status { get; set; }

        public double Total { get; set; }

        public BillDTO(DataRow row)
        {
            ID = (int)row["ID"];
            CreateDay = Convert.ToDateTime(row["CreateDay"]);
            Total = (double)row["TotalBill"];
            Employ = (int)row["Employ"];
            Status = (int)row["Status"];
        }
    }
}
