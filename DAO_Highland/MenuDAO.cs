using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Highland;
using System.Data;
namespace DAO_Highland
{
    public class MenuDAO
    {
        public static List<MenuDTO> GetListMenuByIDBill(int idBill)
        {
            List<MenuDTO> listmenu = new List<MenuDTO>();
            string query = "SELECT DE.IDPRODUCT, D.ID, D.NAMEPRODUCTS, DE.QUANTITY, D.PRICEBASIC, DE.QUANTITY*D.PRICEBASIC AS TOTALPRICE, D.[SIZE], STATUS = 0 FROM DBO.BILL AS BI, DBO.DETAILBILL AS DE, DBO.PRODUCT AS D WHERE DE.IDBILL = BI.ID AND DE.IDPRODUCT = D.ID AND BI.STATUS = 0 AND DE.IDBILL = " + idBill;//0 chưa thanh toán / 1 đã thanh toán rồi.
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                MenuDTO menu = new MenuDTO(item);
                listmenu.Add(menu);
            }
            return listmenu;
        }

        public static List<MenuDTO> GetListMenuByIDTable(int id)
        {
            List<MenuDTO> listmenu = new List<MenuDTO>();
            string query = "SELECT DE.IDPRODUCT, D.ID, D.NAMEPRODUCTS, DE.QUANTITY, D.PRICEBASIC, DE.QUANTITY*D.PRICEBASIC AS TOTALPRICE, D.[SIZE] , STATUS = 0 FROM DBO.BILL AS BI, DBO.DETAILBILL AS DE, DBO.PRODUCT AS D WHERE DE.IDBILL = BI.ID AND DE.IDPRODUCT = D.ID AND BI.STATUS = 0 AND BI.IDTABLE = " + id;//0 chưa thanh toán / 1 đã thanh toán rồi.
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                MenuDTO menu = new MenuDTO(item);
                listmenu.Add(menu);
            }
            return listmenu;
        }

        public static List<MenuDTO> GetReviewBill(int idBill)
        {
            List<MenuDTO> listmenu = new List<MenuDTO>();
            string query = "SELECT DE.IDPRODUCT, D.ID, D.NAMEPRODUCTS, DE.QUANTITY, D.PRICEBASIC, DE.QUANTITY*D.PRICEBASIC AS TOTALPRICE, D.[SIZE] , STATUS = 0 FROM DBO.BILL AS BI, DBO.DETAILBILL AS DE, DBO.PRODUCT AS D WHERE DE.IDBILL = BI.ID AND DE.IDPRODUCT = D.ID AND BI.STATUS = 1 AND DE.IDBILL = " + idBill;//0 chưa thanh toán / 1 đã thanh toán rồi.
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                MenuDTO menu = new MenuDTO(item);
                listmenu.Add(menu);
            }
            return listmenu;
        }
    }
}
