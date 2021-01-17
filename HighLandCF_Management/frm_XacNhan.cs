using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HighLandCF_Management
{
    public partial class frm_XacNhan : Form
    {
        public frm_XacNhan(string thongbao)
        {
            InitializeComponent();
            lblThongBao.Text = thongbao;
        }

        private void txtXacNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 8 || Convert.ToInt32(e.KeyChar) == 64 || (Convert.ToInt32(e.KeyChar) >= 35 && Convert.ToInt32(e.KeyChar) <= 38) || (Convert.ToInt32(e.KeyChar) >= 65 && Convert.ToInt32(e.KeyChar) <= 90) || (Convert.ToInt32(e.KeyChar) >= 97 && Convert.ToInt32(e.KeyChar) <= 122) || (Convert.ToInt32(e.KeyChar) >= 48 && Convert.ToInt32(e.KeyChar) <= 57))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void lblThongBao_Click(object sender, EventArgs e)
        {

        }
    }
}
