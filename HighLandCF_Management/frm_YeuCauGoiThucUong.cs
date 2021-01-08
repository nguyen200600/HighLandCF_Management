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
            statusStrip1.Text = "Nhân viên đang đăng nhập: " + Program.sAccount.Name;
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
            btnLamMoi_Click(sender, e);
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
        private void LoadProductListByTypeProductID(List<ProductDTO> listProduct) // Lấy dữ liệu sản phẩm từ csdl
        {
            lstProduct.Items.Clear();
            for (int i = 0; i < listProduct.Count; i++)
            {
                ListViewItem item = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };
                item.SubItems.Add(listProduct[i].NameProducts);

                item.SubItems.Add(listProduct[i].Size);
                if (listProduct[i].SalePrice == 0)
                    item.SubItems.Add("Miễn phí");
                else
                    item.SubItems.Add(listProduct[i].SalePrice.ToString("0,0 VNĐ"));
                item.Tag = listProduct[i];
                lstProduct.Items.Add(item);
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

        private void lstProductCart_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstProductCart.SelectedItems.Count > 0)
            {
                MenuDTO order = lstProductCart.SelectedItems[0].Tag as MenuDTO;

                frm_ThongTinChiTietSanPham de = new frm_ThongTinChiTietSanPham(currentBillId, order.IdProduct, Convert.ToInt32(order.Quantity) + 1);
                de.lblProductName.Text = order.NameProduct;
                if (order.PriceBasic == 0)
                    de.lblPrice.Text = "Miễn phí";
                else
                    de.lblPrice.Text = String.Format("{0:0,0}", order.PriceBasic);
                de.cbQuantity.SelectedIndex = Convert.ToInt32(order.Quantity) - 1;
                if (order.TotalPrice == 0)
                    de.lblTotal.Text = "Miễn phí";
                else
                    de.lblTotal.Text = String.Format("{0:0,0}", order.TotalPrice);
                DialogResult kq = de.ShowDialog();
                if (kq == DialogResult.OK)
                {
                    DetailBillBUS.InsertDetailBill(currentBillId, order.ID, de.cbQuantity.SelectedIndex + 1);
                    LoadListOrder(currentBillId);
                }
                else if (kq == DialogResult.Yes)
                {
                    LoadListOrder(currentBillId);
                    if (!DetailBillBUS.IsEmpty(currentBillId))
                    {
                        btnTamTinh.Enabled = false;
                        btnThanhToan.Enabled = false;
                        txttotalPrice.Text = "";
                        txtHD.Text = "";
                    }
                }
            }

        }

        private void lstBillNoPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBillNoPayment.SelectedItems.Count > 0)
            {
                currentBillId = (int)lstBillNoPayment.SelectedItems[0].Tag;

                LoadListOrder(currentBillId);
                txtHD.Text = "HD00" + currentBillId;
                btnCreateBill.Enabled = true;
                btnLamMoi.Enabled = false;
                btnTamTinh.Enabled = true;
                btnThanhToan.Enabled = true;
            }
        }
        private void LoadTypeProduct()
        {
            List<TypeProductDTO> listtype = TypeProductBUS.GetListTypeProductWithStatusOne(1);
            cbLoaiThucUong.DataSource = listtype;
            cbLoaiThucUong.DisplayMember = "NameType";
        }
        private void lstProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentBillId <= 0)
            {
                MessageBox.Show("Vui lòng tạo hóa đơn trước nhé.");
                return;
            }
            if (lstProduct.SelectedItems.Count <= 0)
            {
                return;
            }

            ProductDTO Product = lstProduct.SelectedItems[0].Tag as ProductDTO;
            try
            {
                btnThanhToan.Enabled = true;
                btnTamTinh.Enabled = true;

                AddProductToCart(Product, currentBillId);

                LoadListOrder(currentBillId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (currentBillId <= 0)
            {
                return;
            }

            try
            {
                var frm_ThanhToan = new frm_ThanhToan("HÓA ĐƠN THANH TOÁN", currentBillId, txttotalPrice.Text);

                frm_ThanhToan.ShowDialog();
                if (frm_ThanhToan._ketQua)
                {
                    lstProductCart.Items.Clear();

                    btnLamMoi_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTamTinh_Click(object sender, EventArgs e)
        {
            if (currentBillId <= 0)
            {
                return;
            }

            try
            {
                RptThanhToan frm_TToan = new RptThanhToan();
                DateTime Time = DateTime.Now;
                frm_TToan.XuatHoaDon(currentBillId, "HÓA ĐƠN TẠM TÍNH", currentBillId.ToString(), Program.sAccount.Name, Time, string.Format("{0:0,0}", txttotalPrice.Text), "0", "0", "0", "0", true);

                frm_TToan.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e) => lblNgayHienTai.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
