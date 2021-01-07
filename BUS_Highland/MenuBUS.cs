using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Highland;
using DTO_Highland;
namespace BUS_Highland
{
    public class MenuBUS
    {
        public static List<MenuDTO> GetListMenuByIDBill(int idBill) => MenuDAO.GetListMenuByIDBill(idBill);

        public static List<MenuDTO> GetListMenuByIDTable(int id) => MenuDAO.GetListMenuByIDTable(id);

        public static List<MenuDTO> GetReviewBill(int idBill) => MenuDAO.GetReviewBill(idBill);
    }
}
