using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO_Highland;
namespace DAO_Highland
{
    public class AccountDAO
    {
        private const string PASSWORD_DEFAULT = "1234567";

        public static bool DeleteAccount(AccountDTO ac)
        {
            var query = "EXEC DELETEACCOUNT @user";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { ac.ID }) == 1)
            {
                return true;
            }

            return false;
        }

        public static AccountDTO GetAccount(int user)
        {
            var query = "SELECT [SALARY_BY_CA],[ID],[PASS],[NAME],[PASSPORT],[PLACEOFBIRTH],[TELEPHONE],[ADDRESS],[RIGHTS],[STATUS] FROM ACCOUNT WHERE  ID = @user ";
            var data = DataProvider.Instance.ExcuteQuery(query, new object[] { user });

            if (data.Rows.Count > 0)
            {
                return new AccountDTO(data.Rows[0]);
            }

            return null;
        }

        public static List<AccountDTO> GetAllListAccount()
        {
            var listaccount = new List<AccountDTO>();
            var query = "SELECT [SALARY_BY_CA],[ID],[PASS],[NAME],[PASSPORT],[PLACEOFBIRTH],[TELEPHONE],[ADDRESS],[RIGHTS],[STATUS] FROM ACCOUNT";
            var data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                var account = new AccountDTO(item);
                listaccount.Add(account);
            }
            return listaccount;
        }

        public static List<AccountDTO> GetListAccountOnStatus(int status)
        {
            var listaccount = new List<AccountDTO>();
            var query = "SELECT [SALARY_BY_CA],[ID],[PASS],[NAME],[PASSPORT],[PLACEOFBIRTH],[TELEPHONE],[ADDRESS],[RIGHTS],[STATUS] FROM ACCOUNT WHERE STATUS = " + status;
            var data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                var account = new AccountDTO(item);
                listaccount.Add(account);
            }
            return listaccount;
        }

        public static string GetNameByAccount(int user)
        {
            var data = DataProvider.Instance.ExcuteQuery("SELECT AC.NAME FROM DBO.ACCOUNT AS AC WHERE ID = @user ", new object[] { user });//chưa thanh toán
            if (data.Rows.Count > 0)
            {
                return data.Rows[0]["Name"].ToString();
            }
            return "Chưa có nhân viên";
        }

        public static bool InsertAccount(AccountDTO ac)
        {
            var query = "EXEC INSERTACCOUNT @pass , @name , @passport , @placeofbirth , @telephone , @address , @right , @status , @salary_by_ca ";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { ac.PassWord, ac.Name, ac.PassPort, ac.PlaceOfBirth, ac.Telephone, ac.Address, ac.Right, ac.Status, ac.SalaryByCa }) == 1)
            {
                return true;
            }

            return false;
        }

        public static bool IsLogin(int username, string password)
        {
            var query = "SELECT [SALARY_BY_CA],[ID],[PASS],[NAME],[PASSPORT],[PLACEOFBIRTH],[TELEPHONE],[ADDRESS],[RIGHTS],[STATUS] FROM DBO.ACCOUNT WHERE ID = @user and Pass = @pass ";
            var resuft = DataProvider.Instance.ExcuteQuery(query, new object[] { username, password });
            return resuft.Rows.Count > 0;
        }

        public static bool ResetAccount(int user)
        {
            var query = String.Format("UPDATE ACCOUNT SET PASS = {0} WHERE ID = @username ", PASSWORD_DEFAULT);
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { user }) == 1)
            {
                return true;
            }
            return false;
        }

        public static bool UpdateAccount(AccountDTO ac)
        {
            var query = "EXEC UPDATEACCOUNT @user , @pass , @name , @place , @telephone , @address , @right , @status  , @passport , @salary_by_ca ";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { ac.ID, ac.PassWord, ac.Name, ac.PlaceOfBirth, ac.Telephone, ac.Address, ac.Right, ac.Status, ac.PassPort, ac.SalaryByCa }) == 1)
            {
                return true;
            }

            return false;
        }
    }
}

