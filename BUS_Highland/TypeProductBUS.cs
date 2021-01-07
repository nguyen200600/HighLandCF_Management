using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Highland;
using DTO_Highland;
namespace BUS_Highland
{
    public class TypeProductBUS
    {
        public static bool DeleteTypeProduct(TypeProductDTO tydr) => TypeProductDAO.DeleteTypeProduct(tydr);

        public static List<TypeProductDTO> GetAllListTypeProduct() => TypeProductDAO.GetAllListTypeProduct();

        public static List<TypeProductDTO> GetListTypeProductWithStatusOne(int status) => TypeProductDAO.GetListTypeProductWithStatusOne(status);

        public static bool InsertTypeProduct(TypeProductDTO tydr) => TypeProductDAO.InsertTypeProduct(tydr);

        public static bool UpdateTypeProduct(TypeProductDTO tydr) => TypeProductDAO.UpdateTypeProduct(tydr);
    }
}
