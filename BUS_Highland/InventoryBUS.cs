using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Highland;
using DTO_Highland;
namespace BUS_Highland
{
    public class InventoryBUS
    {
        public static bool Add(InventoryDTO invent) => InventoryDAO.Add(invent);

        public static bool Delete(int idInventory) => InventoryDAO.Delete(idInventory);

        public static List<InventoryDTO> GetInventoryByTimeNow() => InventoryDAO.GetInventoryByTimeNow(DateTime.Now.Month, DateTime.Now.Year);

        public static bool Update(InventoryDTO invent) => InventoryDAO.Update(invent);
    }
}
