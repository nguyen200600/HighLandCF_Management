using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Highland;
using DTO_Highland;
namespace BUS_Highland
{
    public class BillBUS
    {
        public static void DeleteBill(int idbill) => BillDAO.DeleteBill(idbill);

        public static List<int> GetAllBillNoPament() => BillDAO.GetAllBillNoPament();

        public static int GetIDBillNoPayment() => BillDAO.GetIDBillNoPayment();

        public static List<BillUpDTO> GetListBillInAboutTime(DateTime from, DateTime to) => BillDAO.GetListBillInAboutTime(from, to);

        public static int InsertBill(DateTime ThoiGian, double TongTien, int Employ) => BillDAO.InsertBill(ThoiGian, TongTien, Employ);

        public static bool IsExistAccountInBill(int user) => BillDAO.IsExistAccountInBill(user);

        public static void UpdatetBill(int id, double totalbill, double promotion, double cusPrice, double outPrice, double revenue, DateTime datetime, int employ)
                 => BillDAO.UpdatetBill(id, totalbill, promotion, cusPrice, outPrice, revenue, datetime, employ);
    }
}
