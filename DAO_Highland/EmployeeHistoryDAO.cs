using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Highland;
using System.Data;
namespace DAO_Highland
{
    public class EmployeeHistoryDAO
    {
        public static bool Add(EmployeeHistoryDTO ac)
        {
            string query = "EXEC INSERTEMPLOYHISTORY @ACCOUNTID , @MONTH , @YEAR , @SALARYBYCA , @CA ";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { ac.AccountID, ac.Month, ac.Year, ac.SalaryByCa, ac.Ca }) == 1)
            {
                return true;
            }

            return false;
        }

        public static List<AccountDetailDTO> GetEmployeeByMonth(int month, int year, bool salaried = false)
        {
            List<AccountDetailDTO> List = new List<AccountDetailDTO>();
            string query = string.Format("SELECT * FROM EMPLOYEEHISTORY e " +
               "RIGHT JOIN ACCOUNT a ON a.ID = e.ACCOUNTID AND e.MONTH = {0} AND e.YEAR = {1} WHERE e.ACCOUNTID IS NULL", month, year);
            if (salaried)
            {
                query = string.Format("SELECT * FROM EMPLOYEEHISTORY e " +
               "RIGHT JOIN ACCOUNT a ON a.ID = e.ACCOUNTID AND e.MONTH = {0} AND e.YEAR = {1} WHERE e.ACCOUNTID IS NOT NULL", month, year);
            }

            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                AccountDetailDTO account = new AccountDetailDTO(item);
                List.Add(account);
            }
            return List;
        }

        public static bool Update(EmployeeHistoryDTO ac)
        {
            string query = "EXEC UPDATEEMPLOYHISTORY @ACCOUNTID , @MONTH , @YEAR , @SALARYBYCA , @CA  ";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { ac.AccountID, ac.Month, ac.Year, ac.SalaryByCa, ac.Ca }) == 1)
            {
                return true;
            }

            return false;
        }
    }
}
