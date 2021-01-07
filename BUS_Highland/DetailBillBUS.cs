using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Highland;
namespace BUS_Highland
{
    public class DetailBillBUS
    {
        public static void DeleteOneProduct(int idbill, int idProduct) => DetailBillDAO.DeleteOneProduct(idbill, idProduct);

        public static int GetQuantityProduct(int idbill, int idProduct) => DetailBillDAO.GetQuantityProduct(idbill, idProduct);

        public static void InsertDetailBill(int idbill, int idProduct, int quantity) => DetailBillDAO.InsertDetailBill(idbill, idProduct, quantity);

        public static bool IsEmpty(int idbill) => DetailBillDAO.IsEmpty(idbill);

        public static int IsExistProduct(int id) => DetailBillDAO.IsExistProduct(id);

        public static bool IsExistProductByIDBillAndIDProduct(int idbill, int idProduct) => DetailBillDAO.IsExistProductByIDBillAndIDProduct(idbill, idProduct);
    }
}
