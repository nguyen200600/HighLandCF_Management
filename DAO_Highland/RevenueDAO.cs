using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Highland;
using System.Data;
namespace DAO_Highland
{
    public class RevenueDAO
    {
        public static List<RevenueDTO> GetRevenueByMonth(int fromMonth, int fromYear, int toMonth, int toYear)
        {
            List<RevenueDTO> List = new List<RevenueDTO>();

            string query = " EXEC [dbo].[USP_GETREVENUEBYMONTH] @FROMMONTH , @FROMYEAR , @TOMONTH , @TOYEAR ";
            DataTable data = DataProvider.Instance.ExcuteQuery(query, new object[] { fromMonth, fromYear, toMonth, toYear });
            foreach (DataRow item in data.Rows)
            {
                RevenueDTO rev = new RevenueDTO(item);
                List.Add(rev);
            }
            return List;
        }
    }
}
