using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Highland;
using System.Data;
namespace DAO_Highland
{
    public class TypeProductDAO
    {
        public static bool DeleteTypeProduct(TypeProductDTO tydr)
        {
            string query = "Exec DeleteTypeProduct @id";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { tydr.ID }) == 1)
            {
                return true;
            }
            return false;
        }

        public static List<TypeProductDTO> GetAllListTypeProduct()
        {
            List<TypeProductDTO> listtypeProduct = new List<TypeProductDTO>();
            string query = "select * from TYPEProduct";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                TypeProductDTO typeProduct = new TypeProductDTO(item);
                listtypeProduct.Add(typeProduct);
            }
            return listtypeProduct;
        }

        public static List<TypeProductDTO> GetListTypeProductWithStatusOne(int status)
        {
            List<TypeProductDTO> listtype = new List<TypeProductDTO>();// 0 ẩn , 1 hiện
            string query = "select * from TYPEPRODUCT where Status = " + status;
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                TypeProductDTO type = new TypeProductDTO(item);
                listtype.Add(type);
            }
            return listtype;
        }

        public static bool InsertTypeProduct(TypeProductDTO tydr)
        {
            string query = "Exec InsertTypeProduct @nametype , @status ";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { tydr.NameType, tydr.Status }) == 1)
            {
                return true;
            }
            return false;
        }

        public static bool UpdateTypeProduct(TypeProductDTO tydr)
        {
            string query = "Exec UpdateTypeProduct @id , @nametype , @status ";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { tydr.ID, tydr.NameType, tydr.Status }) == 1)
            {
                return true;
            }
            else
                return false;
        }
    }
}
