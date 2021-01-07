using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Highland;
using DTO_Highland;
namespace BUS_Highland
{
    public class AccountBUS
    {
        public static bool DeleteAccount(AccountDTO ac) => AccountDAO.DeleteAccount(ac);

        public static AccountDTO GetAccount(int user) => AccountDAO.GetAccount(user);

        public static List<AccountDTO> GetAllListAccount() => AccountDAO.GetAllListAccount();

        public static List<AccountDTO> GetListAccountOnStatus(int status) => AccountDAO.GetListAccountOnStatus(status);

        public static string GetNameByAccount(int user) => AccountDAO.GetNameByAccount(user);

        public static bool InsertAccount(AccountDTO ac) => AccountDAO.InsertAccount(ac);

        public static bool IsLogin(int id, string password) => AccountDAO.IsLogin(id, password);

        public static bool ResetAccount(int user) => AccountDAO.ResetAccount(user);

        public static bool UpdateAccount(AccountDTO ac) => AccountDAO.UpdateAccount(ac);
    }
}
