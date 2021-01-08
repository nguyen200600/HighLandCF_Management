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
    public partial class frm_ThanhToan : Form
    {
        public bool _ketQua = false;
        private readonly int _maHD;
        private readonly string _tenHD;
        private readonly string _tongTien;
        public frm_ThanhToan()
        {
            InitializeComponent();
            _tenHD = TenHD;
            txtMaHD.Text = "HD00" + MaHD.ToString();
            txtTongTien.Text = TongTien.ToString();

            _maHD = MaHD;
            _tongTien = TongTien.Replace(",", "").ToString();

            timer1.Enabled = true;
        }

        private void btnXuatHD_Click(object sender, EventArgs e)
        {
            var promotion = string.IsNullOrEmpty(txtPromotion.Text) ? 0 : Convert.ToDouble(txtPromotion.Text);
            var tongtien = string.IsNullOrEmpty(_tongTien) ? 0 : Convert.ToDouble(_tongTien);
            var thanhtien = tongtien - promotion;

            if (!txtSTK.Text.Equals(""))
            {
                if (promotion > tongtien)
                {
                    MessageBox.Show("Vui lòng nhập số tiền giảm thấp hơn tổng giá trị hóa đơn.");
                    return;
                }

                if (Convert.ToDouble(txtSTK.Text) < thanhtien)
                    MessageBox.Show("Hệ thống không cho phép khách hàng nợ, mong bạn thông cảm nhắc khách hàng thanh toán đúng số tiền trong hóa đơn.");
                else
                {
                    DialogResult kq = MessageBox.Show("Bạn có muốn thanh toán hay không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq != DialogResult.No)
                    {
                        RptThanhToan rptThanhToan = new RptThanhToan();
                        DateTime Time = DateTime.Now;
                        rptThanhToan.XuatHoaDon(_maHD, _tenHD, _maHD.ToString(), Program.sAccount.Name, Time, _tongTien, txtPromotion.Text, txtSTK.Text, txtTienTon.Text, thanhtien.ToString(), true);

                        rptThanhToan.ShowDialog();
                        BillBUS.UpdatetBill(_maHD, tongtien, promotion, Convert.ToDouble(txtSTK.Text), Convert.ToDouble(txtTienTon.Text), thanhtien, Time, Program.sAccount.ID);

                        _ketQua = true;
                        Close();
                    }
                    else _ketQua = false;
                }
            }
            else MessageBox.Show("Nhập tiền khách cần thanh toán cho hóa đơn này!");
        }

        private void txtHuy_Click(object sender, EventArgs e) => Close();

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frm_ThanhToan_Load(object sender, EventArgs e) => txtSTK.ContextMenu = new ContextMenu();
        private void txtSTK_TextChanged(object sender, EventArgs e)
        {
            double promotion = string.IsNullOrEmpty(txtPromotion.Text) ? 0 : Convert.ToDouble(txtPromotion.Text);
            double tongtien = string.IsNullOrEmpty(_tongTien) ? 0 : Convert.ToDouble(_tongTien);
            double thanhtien = tongtien - promotion;
            if (txtSTK.Text.Equals(""))
            {
                txtSTK.Text = "0";
            }

            if (promotion > tongtien)
            {
                MessageBox.Show("Vui lòng nhập số tiền giảm thấp hơn tổng giá trị hóa đơn.");
                return;
            }

            if (!txtSTK.Text.Equals(""))
            {
                if (txtSTK.Text.Length <= 20)
                {
                    double stk = string.IsNullOrEmpty(txtSTK.Text) ? 0 : Convert.ToDouble(txtSTK.Text);
                    double kt = (stk - thanhtien);

                    txtTienTon.Text = kt != 0 ? string.Format("{0:0,0}", kt) : "0";
                    txtThanhTien.Text = (thanhtien != 0) ? string.Format("{0:0,0}", thanhtien) : "0";
                }
                else
                {
                    txtSTK.Text = "0";
                    txtTienTon.Text = "0";
                    txtPromotion.Text = "0";
                    txtThanhTien.Text = "0";
                    MessageBox.Show("Vui lòng nhập số tiền nằm khoảng chừng số tiền khách hàng phải trả.");
                }
            }
            else
            {
                txtTienTon.Text = "0";
            }
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
