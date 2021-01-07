using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO_Highland;

namespace HighLandCF_Management
{
    public partial class frm_ThongTinTaiKhoan : Form
    {
        private readonly AccountDTO _account;
        public frm_ThongTinTaiKhoan(AccountDTO acc)
        {
            InitializeComponent();
            _account = acc;
        }
        private void btnDMK_Click(object sender, EventArgs e)
        {
            frm_LayLaiMatKhau frm = new frm_LayLaiMatKhau(_account, 1);
            frm.ShowDialog();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_ThongTinTaiKhoan_Load(object sender, EventArgs e)
        {
            lblName.Text = _account.Name;
            lblNoiSinh.Text = _account.PlaceOfBirth;
            lblSDT.Text = _account.Telephone;
            lblDiaChi.Text = _account.Address;
        }
    }
}
