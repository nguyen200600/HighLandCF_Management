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

namespace HighLandCF_Management
{
    public partial class frm_ThongTinChiTietSanPham : Form
    {
        private readonly int idBill;

        private readonly int idProduct;
        public frm_ThongTinChiTietSanPham(int idBill, int idProduct, int k)
        {
            InitializeComponent();
            this.idBill = idBill;
            this.idProduct = idProduct;
            int n = k - 1;
            if (n <= 50)
            {
                n = 50;
            }

            for (int i = 1; i <= n; i++)
            {
                cbQuantity.Items.Add(i);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có muốn xóa sản phẩm này không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (kq != DialogResult.Cancel)
            {
                DetailBillBUS.DeleteOneProduct(idBill, idProduct);

                if (DetailBillBUS.IsEmpty(idBill))
                {
                    return;
                }

                BillBUS.DeleteBill(idBill);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void frm_ThongTinChiTietSanPham_Load(object sender, EventArgs e)
        {
            cbQuantity.ContextMenu = new ContextMenu();
        }
    }
}
