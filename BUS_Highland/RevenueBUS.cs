using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Highland;
using DTO_Highland;
namespace BUS_Highland
{
    public class RevenueBUS
    {
        public static List<RevenueDTO> GetRevenueByMonth(int fromMonth, int fromYear, int toMonth, int toYear)
        => RevenueDAO.GetRevenueByMonth(fromMonth, fromYear, toMonth, toYear);
    }
}
