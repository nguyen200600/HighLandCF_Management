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
using BUS_Highland;
namespace HighLandCF_Management
{
    public partial class frm_LayLaiMatKhau : Form
    {
        private readonly int _case;
        private AccountDTO _Account;

        public frm_LayLaiMatKhau(AccountDTO account, int @case)
        {
            InitializeComponent();
            _Account = account;
            _case = @case;//0: hiện combobox 1: hiện label
            LoadAccount();
            if (_case == 0)
                _Account = AccountBUS.GetAccount((int)cbxNhanVien.SelectedValue);
        }
        private void LoadAccount()
        {
            //load loại thức uống theo tên
            List<AccountDTO> listtype = AccountBUS.GetAllListAccount();
            cbxNhanVien.DataSource = listtype;
            cbxNhanVien.DisplayMember = "Name";
            cbxNhanVien.ValueMember = "ID";
        }
        private void ClearField()
        {
            txtPassOld.Text = "";
            txtPasswwordNew.Text = "";
            txtRetypePass.Text = "";
            lblThongBao.Text = "";
        }
        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (_case == 0)
                {
                    _Account = AccountBUS.GetAccount((int)cbxNhanVien.SelectedValue);
                }

                if (string.IsNullOrEmpty(txtRetypePass.Text) || string.IsNullOrEmpty(txtPasswwordNew.Text) || string.IsNullOrEmpty(txtPassOld.Text))
                {
                    lblThongBao.Text = "Không được bỏ trống trường dữ liệu nào";
                    return;
                }

                if (!AccountBUS.IsLogin(_Account.ID, txtPassOld.Text))
                {
                    lblThongBao.Text = "Mật khẩu cũ không hợp lệ.";
                    return;
                }

                if (string.Compare(txtPasswwordNew.Text, txtRetypePass.Text) != 0)
                {
                    lblThongBao.Text = "Hai mật khẩu không khớp";
                    return;
                }

                if (txtRetypePass.Text.Length < 6 || txtPasswwordNew.Text.Length < 6)
                {
                    lblThongBao.Text = "Mật khẩu phải ít nhất 6 ký tự";
                    return;
                }

                _Account.PassWord = txtPasswwordNew.Text;
                if (AccountBUS.UpdateAccount(_Account))
                {
                    MessageBox.Show("Thay đổi mật khẩu thành công!", "Thông báo");
                    Close();
                }

                ClearField();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            ClearField();
            Close();
        }

        private void frm_LayLaiMatKhau_Load(object sender, EventArgs e)
        {
            cbxNhanVien.ContextMenu = new ContextMenu();
            if (_case == 1)
            {
                lblNhanVien.Visible = true;
                cbxNhanVien.Visible = false;
                lblNhanVien.Text = _Account.Name;
            }
            else
            {
                lblNhanVien.Visible = false;
                cbxNhanVien.Visible = true;
                cbxNhanVien.SelectedValue = _Account.ID;
            }
        }

        private void txtPassOld_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 8 || Convert.ToInt32(e.KeyChar) == 64 || (Convert.ToInt32(e.KeyChar) >= 35 && Convert.ToInt32(e.KeyChar) <= 38) || (Convert.ToInt32(e.KeyChar) >= 65 && Convert.ToInt32(e.KeyChar) <= 90) || (Convert.ToInt32(e.KeyChar) >= 97 && Convert.ToInt32(e.KeyChar) <= 122) || (Convert.ToInt32(e.KeyChar) >= 48 && Convert.ToInt32(e.KeyChar) <= 57))
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }
    }
}
