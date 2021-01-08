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
using HighLandCF_Management.Constants;

namespace HighLandCF_Management
{
    public partial class frm_YeuCauGoiThucUong : Form
    {
        private int currentBillId = -1;

        public frm_YeuCauGoiThucUong()
        {
            InitializeComponent();
            LoadTypeProduct();
            LoadListBillNoPayment();
            tssNhanVien.Text = "Nhân viên đang đăng nhập: " + Program.sAccount.Name;
        }
        public void LoadListOrder(int billId)
        {
            lstProductCart.Items.Clear();

            List<MenuDTO> menulist = MenuBUS.GetListMenuByIDBill(billId);
            double totalPrice = 0;
            for (int i = 0; i < menulist.Count; i++)
            {
                ListViewItem listitem = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };

                listitem.SubItems.Add(menulist[i].NameProduct.ToString());
                listitem.SubItems.Add(menulist[i].Quantity.ToString());
                if (menulist[i].PriceBasic == 0)
                {
                    listitem.SubItems.Add("Miễn phí");
                }
                else
                {
                    listitem.SubItems.Add(String.Format("{0:0,0}", menulist[i].PriceBasic) + " VNĐ");
                }
                listitem.SubItems.Add(menulist[i].Size);
                if (menulist[i].TotalPrice == 0)
                {
                    listitem.SubItems.Add("Miễn phí");
                }
                else
                {
                    listitem.SubItems.Add(String.Format("{0:0,0}", menulist[i].TotalPrice) + " VNĐ");
                }

                totalPrice += menulist[i].TotalPrice;

                listitem.Tag = menulist[i];
                lstProductCart.Items.Add(listitem);
            }
            if (totalPrice > 0)
                txttotalPrice.Text = String.Format("{0:0,0}", totalPrice);
        }

        private void frm_YeuCauGoiThucUong_Load(object sender, EventArgs e)
        {
            LoadProductSize();
            LoadProductListByTypeProductID(ProductBUS.GetListProductByIDTypeProduct(0, 1));

            cbLoaiThucUong.ContextMenu = new ContextMenu();
            timer1.Enabled = true;
            btnThanhToan.Enabled = false;
            btnTamTinh.Enabled = false;

            lstBillNoPayment.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstBillNoPayment.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            lstProductCart.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstProductCart.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            lstProduct.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstProduct.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void AddProductToCart(ProductDTO Product, int billId)
        {
            int idProduct = Product.ID;

            var quantity = DetailBillBUS.GetQuantityProduct(billId, idProduct);
            DetailBillBUS.InsertDetailBill(billId, idProduct, quantity + 1);
        }

        private void btnCancelBill_Click(object sender, EventArgs e)
        {
            if (currentBillId <= 0)
            {
                return;
            }

            BillBUS.DeleteBill(currentBillId);

            currentBillId = -1;
            btnCreateBill.Enabled = true;
            btnLamMoi.Enabled = true;
            BtnLamMoi_Click(sender, e);
        }

        private void btnCreateBill_Click(object sender, EventArgs e)
        {
            currentBillId = BillBUS.InsertBill(DateTime.Now, 0, Program.sAccount.ID);
            txtHD.Text = "HD00" + currentBillId;
            //btnCreateBill.Enabled = false;
            btnLamMoi.Enabled = false;

            LoadListBillNoPayment();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            currentBillId = -1;

            txtHD.Text = "";
            lstProductCart.Items.Clear();
            txttotalPrice.Text = "0";
            btnThanhToan.Enabled = false;
            btnTamTinh.Enabled = false;
            txtTuKhoa.Text = "";

            LoadTypeProduct();
            LoadListBillNoPayment();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
             Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadProductListByTypeProductID(ProductBUS.GeProductByName(txtTuKhoa.Text));
        }

        private void cbLoaiThucUong_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbLoaiThucUong_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterProduct();
        }

        private void cbxProductSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterProduct();
        }
        private void FilterProduct()
        {
            if (cbLoaiThucUong.SelectedIndex >= 0 && cbxProductSizes.SelectedIndex >= 0)
            {
                if (cbLoaiThucUong.SelectedItem == null || cbxProductSizes.SelectedItem == null)
                {
                    return;
                }

                var typeProduct = cbLoaiThucUong.SelectedItem as TypeProductDTO;
                var typeId = typeProduct.ID;

                var size = cbxProductSizes.SelectedItem as ProductSize;
                var sizeId = size.Id;

                LoadProductListByTypeProductID(ProductBUS.GetListProductByIDTypeProduct(typeId, 1, sizeId));
            }
        }

        private void LoadListBillNoPayment()
        {
            var list = BillBUS.GetAllBillNoPament();
            lstBillNoPayment.Items.Clear();
            int i = 0;
            foreach (var item in list)
            {
                ListViewItem listitem = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };

                listitem.SubItems.Add(item.ToString());
                listitem.Tag = item;
                lstBillNoPayment.Items.Add(listitem);
                i++;
            }
        }
        private void LoadProductSize()
        {
            var list = ProductSizes.List();
            cbxProductSizes.DataSource = list;
            cbxProductSizes.DisplayMember = "Name";
        }
    }
}
