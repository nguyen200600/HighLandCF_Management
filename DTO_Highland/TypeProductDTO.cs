using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DTO_Highland
{
    public class TypeProductDTO
    {
        public int ID { get; set; }

        public string NameType { get; set; }

        public int Status { get; set; }

        public TypeProductDTO()
        {
        }


        public TypeProductDTO(DataRow row)
        {
            ID = (int)row["ID"];
            NameType = row["NameType"].ToString();
            Status = (int)row["Status"];
        }
    }
}
