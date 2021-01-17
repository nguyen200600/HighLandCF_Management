using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS_Highland;
using DTO_Highland;

namespace HighLandCF_Management
{
    public partial class frm_DangNhap : Form
    {
        public frm_DangNhap()
        {
            InitializeComponent();
        }
        public void XoaTruongDangNhap()
        {
            txtMatKhau.Text = "";
            lblNotification.Text = "";
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            int username = (int)cbxNhanVien.SelectedValue;
            string password = txtMatKhau.Text;
            if (Login(username, password))
            {
                Program.sAccount = AccountBUS.GetAccount(username);
                if (Program.sAccount.Status == 1)
                {
                    Program.sAccount = AccountBUS.GetAccount(username);
                    if (Program.sAccount.Right == 0)
                    {
                        XoaTruongDangNhap();

                        frm_Main n = new frm_Main();
                        Hide();
                        n.ShowDialog();
                        Show();

                        LoadAccount();
                    }
                    else if (Program.sAccount.Right == 1)
                    {
                        XoaTruongDangNhap();
                        frm_YeuCauGoiThucUong y = new frm_YeuCauGoiThucUong();
                        Hide();
                        y.ShowDialog();
                        Show();
                        LoadAccount();
                    }
                    else
                    {
                        Close();
                    }
                }
                else
                {
                    Program.sAccount = null;
                    lblNotification.Text = "Tài khoản của bạn đã bị khóa bởi người quản trị.";
                }
            }
            else
                lblNotification.Text = "Bạn nhập sai tài khoản hoặc mật khẩu. Vui lòng nhập lại!";
        }
        
        private void btnThoat_Click(object sender, EventArgs e) => Close();

        private void cbxNhanVien_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void chkHidePass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHidePass.Checked)
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else
                txtMatKhau.UseSystemPasswordChar = true;
        }

        private void frm_DangNhap_Load(object sender, EventArgs e)
        {
            LoadAccount();
            cbxNhanVien.ContextMenu = new ContextMenu();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ThongTinTaiKhoan frm_ThongTin = new frm_ThongTinTaiKhoan(AccountBUS.GetAccount((int)cbxNhanVien.SelectedValue));
            frm_ThongTin.ShowDialog();
        }
        private void LoadAccount()
        {
            List<AccountDTO> listtype = AccountBUS.GetListAccountOnStatus(1);
            cbxNhanVien.DataSource = listtype;
            cbxNhanVien.DisplayMember = "Name";
            cbxNhanVien.ValueMember = "ID";
        }

        private bool Login(int username, string password) => AccountBUS.IsLogin(username, password);
        private bool Login1(string username, string password) => AccountBUS.IsLogin1(username, password);

        private void txtMatKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 8 || Convert.ToInt32(e.KeyChar) == 64 || (Convert.ToInt32(e.KeyChar) >= 35 && Convert.ToInt32(e.KeyChar) <= 38) || (Convert.ToInt32(e.KeyChar) >= 65 && Convert.ToInt32(e.KeyChar) <= 90) || (Convert.ToInt32(e.KeyChar) >= 97 && Convert.ToInt32(e.KeyChar) <= 122) || (Convert.ToInt32(e.KeyChar) >= 48 && Convert.ToInt32(e.KeyChar) <= 57))
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }

        private void cbxNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string username = txtNhanvien.Text;
            string password = txtMatKhau.Text;
            if (Login1(username, password))
            {
                Program.sAccount = AccountBUS.GetAccount1(username);
                if (Program.sAccount.Status == 1)
                {
                    Program.sAccount = AccountBUS.GetAccount1(username);
                    if (Program.sAccount.Right == 0)
                    {
                        XoaTruongDangNhap();

                        frm_Main n = new frm_Main();
                        Hide();
                        n.ShowDialog();
                        Show();

                        LoadAccount();
                    }
                    else if (Program.sAccount.Right == 1)
                    {
                        XoaTruongDangNhap();
                        frm_YeuCauGoiThucUong y = new frm_YeuCauGoiThucUong();
                        Hide();
                        y.ShowDialog();
                        Show();
                        LoadAccount();
                    }
                    else
                    {
                        Close();
                    }
                }
                else
                {
                    Program.sAccount = null;
                    lblNotification.Text = "Tài khoản của bạn đã bị khóa bởi người quản trị.";
                }
            }
            else
                lblNotification.Text = "Bạn nhập sai tài khoản hoặc mật khẩu. Vui lòng nhập lại!";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ThongTinTaiKhoan frm_ThongTin = new frm_ThongTinTaiKhoan(AccountBUS.GetAccount((int)cbxNhanVien.SelectedValue));
            Hide();
            frm_ThongTin.ShowDialog();

        }
    }
}
    
