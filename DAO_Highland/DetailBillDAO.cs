using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Highland;
using System.Data;

namespace DAO_Highland
{
    public class DetailBillDAO
    {
        public static void DeleteOneProduct(int idbill, int idProduct)
        {
            string query = "EXEC DELETEDETAILBILL @idbill , @idProduct ";
            DataProvider.Instance.ExcuteNonQuery(query, new object[] { idbill, idProduct });
        }

        public static int GetQuantityProduct(int idbill, int idProduct)
        {
            var data = DataProvider.Instance.ExcuteQuery("SELECT [IDBILL], [IDProduct], [QUANTITY]  FROM DBO.DETAILBILL AS DE WHERE IDBILL = " + idbill + " and IDProduct = " + idProduct);
            if (data.Rows.Count > 0)
            {
                DetailBillDTO debill = new DetailBillDTO(data.Rows[0]);
                return debill.Quantity;
            }
            return 0;
        }

        public static void InsertDetailBill(int idbill, int idProduct, int quantity) => DataProvider.Instance.ExcuteNonQuery("EXEC INSERTBILLINFO @idbill , @idProduct , @quantity ", new object[] { idbill, idProduct, quantity });

        public static bool IsEmpty(int idbill)
        {
            var data = DataProvider.Instance.ExcuteQuery("SELECT [IDBILL], [IDProduct], [QUANTITY]  FROM DBO.DETAILBILL AS DE WHERE IDBILL = " + idbill);
            if (data.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static int IsExistProduct(int id)
        {
            var query = "SELECT DE.IDProduct FROM DETAILBILL AS DE WHERE DE.IDProduct = " + id;
            var data = DataProvider.Instance.ExcuteQuery(query);
            if (data.Rows.Count > 0)
            {
                return Convert.ToInt32(data.Rows[0]["IDProduct"].ToString());
            }
            return -1;
        }

        public static bool IsExistProductByIDBillAndIDProduct(int idbill, int idProduct)
        {
            var data = DataProvider.Instance.ExcuteQuery("SELECT [IDBILL], [IDProduct], [QUANTITY]  FROM DETAILBILL AS DE WHERE IDBILL = " + idbill + " and IDProduct = " + idProduct);
            if (data.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
