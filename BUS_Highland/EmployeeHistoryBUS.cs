using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Highland;
using DTO_Highland;
namespace BUS_Highland
{
    public class EmployeeHistoryBUS
    {
        public static bool Add(EmployeeHistoryDTO employee) => EmployeeHistoryDAO.Add(employee);

        public static List<AccountDetailDTO> GetEmployeeByMonth(int month, int year, bool salaried = false) => EmployeeHistoryDAO.GetEmployeeByMonth(month, year, salaried);

        public static bool Update(EmployeeHistoryDTO employee) => EmployeeHistoryDAO.Update(employee);
    }
}
