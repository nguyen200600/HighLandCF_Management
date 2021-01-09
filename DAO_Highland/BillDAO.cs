using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Highland;
using System.Data;
namespace DAO_Highland
{
    public class BillDAO
    {
        public static void DeleteBill(int idbill) => DataProvider.Instance.ExcuteNonQuery("Exec DELETEBILL @idbill ", new object[] { idbill });

        public static List<int> GetAllBillNoPament()
        {
            var lstBill = new List<int>();
            var data = DataProvider.Instance.ExcuteQuery("SELECT ID FROM BILL WHERE STATUS = 0 ");
            foreach (DataRow item in data.Rows)
            {
                if (Int32.TryParse(item["ID"].ToString(), out int value))
                {
                    lstBill.Add(value);
                }
            }
            return lstBill;
        }

        public static int GetIDBillNoPayment()
        {
            var data = DataProvider.Instance.ExcuteQuery("SELECT TOP 1 * FROM BILL WHERE AND STATUS = 0");
            if (data.Rows.Count > 0)
            {
                BillDTO bill = new BillDTO(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }

        public static List<BillUpDTO> GetListBillInAboutTime(DateTime ThoiGianTu, DateTime ThoiGianDen)
        {
            var lstBill = new List<BillUpDTO>();
            var query = "SELECT * FROM BILL WHERE CREATEDAY BETWEEN @THOIGIANTU AND @THOIGIANDEN AND STATUS = 1";
            var data = DataProvider.Instance.ExcuteQuery(query, new object[] { ThoiGianTu, ThoiGianDen });
            foreach (DataRow item in data.Rows)
            {
                BillUpDTO detail = new BillUpDTO(item);
                lstBill.Add(detail);
            }
            return lstBill;
        }

        public static int InsertBill(DateTime thoiGian, double tongTien, int employ)
        {
            var query = "Exec InsertBill @thoigian , @tongtien , @employ ";
            var re = DataProvider.Instance.ExcuteScalar(query, new object[] { thoiGian, tongTien, employ }).ToString();
            if (re != "")
                return Convert.ToInt32(re);
            return 1;
        }

        public static bool IsExistAccountInBill(int user)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("select ID from BILL where Employ = " + user);
            if (data.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static void UpdatetBill(int id, double totalbill, double promotion, double cusPrice, double outPrice, double revenue, DateTime datetime, int employ)
        {
            var query = "Exec UpdateBill @IDBILL , @TOTALBILL , @DATETIME , @EMPLOY , @PROMOTIONPRICE , @CUSTOMERPRICE , @OUTPRICE , @REVENUE ";

            DataProvider.Instance.ExcuteNonQuery(query, new object[] { id, totalbill, datetime, employ, promotion, cusPrice, outPrice, revenue });
        }
    }
}
