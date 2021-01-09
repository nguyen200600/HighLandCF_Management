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
using HighLandCF_Management.Constants;
//nguyhh
namespace HighLandCF_Management
{
    public partial class frm_QuanLyHeThong : Form
    {
        private const string PASSWORD_DEFAULT = "1234567";
        private bool isAddNL = true;
        private bool isAddSalary = true;

        public frm_QuanLyHeThong()
        {
            InitializeComponent();
            tssNhanvien.Text = "Nhân viên đang đăng nhập: " + Program.sAccount.Name;
            tssThoiGian.Text = " - Ngày hiện tại: " + DateTime.Now.ToString("dd/MM/yyyy");

            btnEditTypeProduct.Visible = false;
            btnEditProduct.Visible = false;
            btnDeleteAccount.Enabled = false;
            btnAddAccount.Visible = true;
            btnEditAccount.Visible = false;
            radAdminAccount.TabPages.RemoveAt(5);
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtHoTen.Text == "" || txtTelephone.Text == "" ||
                    txtAddress.Text == "" ||
                    txtCMND.Text == "")
                {
                    MessageBox.Show("Bạn vui lòng điền đầy đủ thông tin nhé!\n gồm: Họ tên, Số điện thoại, địa chỉ, số CMND và Lương", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    double salary = string.IsNullOrEmpty(txtSalary.Text) ? 0 : Convert.ToDouble(txtSalary.Text);
                    if (salary > 0)
                    {
                        if (txtCMND.Text.Length >= 9 && txtCMND.Text.Length <= 15)
                        {
                            if (txtTelephone.Text.Length == 10 || txtTelephone.Text.Length == 11)
                            {
                                AccountDTO sp = new AccountDTO
                                {
                                    PassWord = PASSWORD_DEFAULT,
                                    Name = txtHoTen.Text,
                                    PassPort = txtCMND.Text,
                                    PlaceOfBirth = txtNoiSinh.Text,
                                    Telephone = txtTelephone.Text,
                                    Address = txtAddress.Text,
                                    SalaryByCa = Convert.ToDouble(txtSalary.Text)
                                };
                                if (radAd.Checked)
                                {
                                    sp.Right = 0;
                                }
                                else
                                    sp.Right = 1;
                                if (radHienAccount.Checked)
                                {
                                    sp.Status = 1;
                                }
                                else
                                    sp.Status = 0;
                                if (AccountBUS.InsertAccount(sp))
                                {
                                    DeleteTextAccount();
                                    ShowAccount();
                                    MessageBox.Show("Đã thêm thành công", "Thông báo", MessageBoxButtons.OK);
                                    txtNameAcount.ReadOnly = false;
                                }
                                else
                                    MessageBox.Show("Thêm tài khoản thất bại", "Thông báo", MessageBoxButtons.OK);
                            }
                            else MessageBox.Show("Số điện thoại phải có 10 hoặc 11 số");
                        }
                        else
                        {
                            MessageBox.Show("Số chứng minh nhân dân nằm trong khoảng 9 - 15 số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lương nhân viên phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductName.Text == "" || txtPriceProduct.Text == "")
                {
                    MessageBox.Show("Bạn không thể thêm nếu như để trống một trường dữ liệu nào.", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    ProductDTO sp = new ProductDTO
                    {
                        NameProducts = txtProductName.Text,
                        PriceBasic = Convert.ToDouble(txtPriceProduct.Text)
                    };

                    var size = cbxProductSize.SelectedItem as ProductSize;
                    sp.Size = size.Id;

                    sp.SalePrice = sp.PriceBasic;
                    if (radAn.Checked)
                    {
                        sp.Status = 0;
                    }
                    else
                        sp.Status = 1;
                    sp.SalePrice = sp.PriceBasic;

                    TypeProductDTO typeProduct = cbTypeProduct.SelectedItem as TypeProductDTO;
                    sp.IDTypeProduct = typeProduct.ID;

                    if (ProductBUS.InsertProduct(sp))
                    {
                        ShowProduct();
                        DeleteTextProduct();

                        MessageBox.Show("Thêm mới thành công.", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                        MessageBox.Show("Thêm mới sản phẩm thất bại, vui lòng kiểm tra dữ liệu nhập vào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnAddTypeProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTypeProductName.Text == "")
                {
                    MessageBox.Show("Bạn không thể cập nhật nếu như để trống một trường dữ liệu nào.", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    TypeProductDTO sp = new TypeProductDTO
                    {
                        NameType = txtTypeProductName.Text
                    };

                    if (radAnType.Checked)
                    {
                        sp.Status = 0;
                    }
                    else
                        sp.Status = 1;

                    if (TypeProductBUS.InsertTypeProduct(sp) == true)
                    {
                        ShowTypeProduct();
                        LoadTypeProduct(cbTypeProduct);
                        LoadTypeProduct(cbLocLoaiSP);
                        DeleteTextType();

                        MessageBox.Show("Đã thêm mới loại sản phẩm thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                        MessageBox.Show("Bạn đã thêm loại sản phẩm thất bại, vui lòng kiểm tra thông tin nhập vào!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAccount.SelectedItems.Count > 0)
                {
                    ListViewItem lvw = lstAccount.SelectedItems[0];
                    AccountDTO sp = (AccountDTO)lvw.Tag;
                    if (sp.ID.CompareTo(Program.sAccount.ID) == 0)
                    {
                        MessageBox.Show("Bạn không thể sử dụng chức năng này với chính bạn.", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        frm_XacNhan frm_XN = new frm_XacNhan("Bạn vui lòng nhập mật khẩu để xác nhận thao tác này!");
                        if (frm_XN.ShowDialog() == DialogResult.OK)
                        {
                            if (AccountBUS.IsLogin(Program.sAccount.ID, frm_XN.txtXacNhap.Text))
                            {
                                if (!BillBUS.IsExistAccountInBill(sp.ID) || BillBUS.IsExistAccountInBill(sp.ID))
                                {
                                    if (AccountBUS.DeleteAccount(sp) == true)
                                    {
                                        ShowAccount();
                                        DeleteTextAccount();
                                        txtNameAcount.ReadOnly = false;
                                        btnEditAccount.Visible = false;
                                        btnAddAccount.Visible = true;
                                        btnDeleteAccount.Enabled = false;

                                        MessageBox.Show("Bạn đã xóa thành công", "Thông báo", MessageBoxButtons.OK);
                                    }
                                    else
                                        MessageBox.Show("Xóa tài khoản thất bại.", "Thông báo", MessageBoxButtons.OK);
                                }
                                else
                                    MessageBox.Show("Tài khoản này đang hoạt động với hệ thống. Nên bạn không thể xóa tài khoản này khỏi hệ thống.", "Thông báo", MessageBoxButtons.OK);
                            }
                            else
                                MessageBox.Show("Bạn đã nhập sai mật khẩu, vui lòng quay lại sau.", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                }
                else
                    MessageBox.Show("Bạn chưa chọn được tài khoản nào.", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnDeProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstProduct.SelectedItems != null)
                {
                    ListViewItem lvw = lstProduct.SelectedItems[0];
                    ProductDTO sp = (ProductDTO)lvw.Tag;
                    frm_XacNhan frm_XN = new frm_XacNhan("Xóa một sản phẩm rất quan trọng. Bạn vui lòng nhập mật khẩu để xác nhận thao tác này!");
                    if (frm_XN.ShowDialog() == DialogResult.OK)
                    {
                        if (AccountBUS.IsLogin(Program.sAccount.ID, frm_XN.txtXacNhap.Text))
                        {
                            if (DetailBillBUS.IsExistProduct(sp.ID) == -1)
                            {
                                if (lstProduct.SelectedItems.Count > 0)
                                {
                                    if (ProductBUS.DeleteProduct(sp) == true)
                                    {
                                        ShowProduct();
                                        DeleteTextProduct();
                                        btnEditProduct.Visible = false;
                                        btnAddProduct.Visible = true;

                                        MessageBox.Show("Bạn đã xóa thành công sản phẩm SP00" + sp.ID + " khỏi hệ thống!", "Thông báo", MessageBoxButtons.OK);
                                    }
                                    else
                                        MessageBox.Show("Xóa sản phẩm thất bại, vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK);
                                }
                                else
                                {
                                    MessageBox.Show("Chưa chọn thức uống", "Thông báo", MessageBoxButtons.OK);
                                }
                            }
                            else
                                MessageBox.Show("Thức uống này đã được người dùng chọn hoặc mua trong thời gian trước đó, bạn không thể xóa sản phẩm này!", "Thông báo", MessageBoxButtons.OK);
                        }
                        else MessageBox.Show("Bạn nhập sai mật khẩu, vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else MessageBox.Show("Bạn vui lòng chọn sản phẩm trước khi thực hiện chức năng này!", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnDeTypeProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTypeProduct.SelectedItems.Count > 0)
                {
                    TypeProductDTO sp = lstTypeProduct.SelectedItems[0].Tag as TypeProductDTO;
                    frm_XacNhan frm_XN = new frm_XacNhan("Vui lòng nhập mật khẩu để xác nhận thao tác này!");
                    if (frm_XN.ShowDialog() == DialogResult.OK)
                    {
                        if (AccountBUS.IsLogin(Program.sAccount.ID, frm_XN.txtXacNhap.Text))
                        {
                            if ((ProductBUS.GetIDTypeProductByIDProduct(sp.ID)) == -1)
                            {
                                if (TypeProductBUS.DeleteTypeProduct(sp))
                                {
                                    MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK);
                                    ShowTypeProduct();
                                    DeleteTextType();
                                    LoadTypeProduct(cbLocLoaiSP);
                                    LoadTypeProduct(cbTypeProduct);
                                }
                                else
                                    MessageBox.Show("Thực hiện xóa thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK);
                            }
                            else { MessageBox.Show("Bạn vui lòng xóa tất cả sản phẩm đang thuộc loại sản phẩm này, trước khi thực hiện chức năng này", "Thông báo", MessageBoxButtons.OK); }
                        }
                        else MessageBox.Show("Bạn nhập sai mật khẩu, vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                    MessageBox.Show("Bạn chưa chọn loại sản phẩm nào!", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtHoTen.Text == "" || txtTelephone.Text == "" ||
                    txtAddress.Text == "" ||
                    txtCMND.Text == "" || txtSalary.Text == "")
                {
                    MessageBox.Show("Bạn vui lòng điền đầy đủ thông tin nhé!\n gồm: Họ tên, Số điện thoại, địa chỉ, số CMND và Lương", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    double salary = string.IsNullOrEmpty(txtSalary.Text) ? 0 : Convert.ToDouble(txtSalary.Text);
                    if (salary > 0)
                    {
                        if (txtCMND.Text.Length >= 9 && txtCMND.Text.Length <= 15)
                        {
                            if (txtTelephone.Text.Length >= 10 && txtTelephone.Text.Length <= 11)
                            {
                                if (lstAccount.SelectedItems.Count > 0)
                                {
                                    AccountDTO sp = lstAccount.SelectedItems[0].Tag as AccountDTO;
                                    sp.PassWord = PASSWORD_DEFAULT;
                                    sp.Name = txtHoTen.Text;
                                    sp.PlaceOfBirth = txtNoiSinh.Text;
                                    sp.Telephone = txtTelephone.Text;
                                    sp.Address = txtAddress.Text;
                                    sp.PassPort = txtCMND.Text;
                                    sp.SalaryByCa = Convert.ToDouble(txtSalary.Text);
                                    if (radAd.Checked)
                                    {
                                        sp.Right = 0;
                                    }
                                    else
                                        sp.Right = 1;

                                    if (radHienAccount.Checked)
                                    {
                                        sp.Status = 1;
                                    }
                                    else
                                        sp.Status = 0;
                                    if (sp.ID == Program.sAccount.ID && sp.Status == 0)
                                    {
                                        MessageBox.Show("Bạn không thể khóa chính bạn.", "Thông báo", MessageBoxButtons.OK);
                                    }
                                    else
                                    {
                                        if (AccountBUS.UpdateAccount(sp))
                                        {
                                            ShowAccount();
                                            MessageBox.Show("Bạn đã cập nhật thành công", "Thông báo", MessageBoxButtons.OK);
                                            DeleteTextAccount();
                                            txtNameAcount.ReadOnly = false;
                                            btnEditAccount.Visible = false;
                                            btnAddAccount.Visible = true;
                                            btnDeleteAccount.Enabled = false;
                                            if (sp.ID == Program.sAccount.ID && sp.Right == 1)
                                            {
                                                Program.sAccount = sp;
                                                Close();
                                            }
                                        }
                                        else
                                            MessageBox.Show("Chưa cập nhật thành công", "Thông báo", MessageBoxButtons.OK);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Chưa chọn tài khoản", "Thông báo", MessageBoxButtons.OK);
                                }
                            }
                            else MessageBox.Show("Số điện thoại phải có 10 hoặc 11 số");
                        }
                        else
                        {
                            MessageBox.Show("Số chứng minh nhân dân nằm trong khoảng 9 - 15 số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lương nhân viên phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstProduct.SelectedItems.Count > 0)
                {
                    if (txtProductName.Text == "" || txtPriceProduct.Text == "")
                    {
                        MessageBox.Show("Bạn không thể cập nhật nếu như để trống một trường dữ liệu nào.", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        ProductDTO sp = lstProduct.SelectedItems[0].Tag as ProductDTO;
                        sp.NameProducts = txtProductName.Text;
                        sp.PriceBasic = Convert.ToDouble(txtPriceProduct.Text);
                        var size = cbxProductSize.SelectedItem as ProductSize;
                        sp.Size = size.Id;

                        sp.SalePrice = sp.PriceBasic;

                        if (radAn.Checked)
                        {
                            sp.Status = 0;
                        }
                        else
                            sp.Status = 1;

                        TypeProductDTO typeProduct = cbTypeProduct.SelectedItem as TypeProductDTO;
                        sp.IDTypeProduct = typeProduct.ID;

                        if (ProductBUS.UpdateProduct(sp))
                        {
                            DeleteTextProduct();
                            btnAddProduct.Visible = true;
                            btnEditProduct.Visible = false;
                            btnDeProduct.Enabled = false;

                            MessageBox.Show("Bạn đã cập nhật sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK);
                        }
                        else
                            MessageBox.Show("Hiện tại bạn đã cập nhật thông tin sản phẩm thất bại!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn vui lòng chọn sản phẩm trước khi thực hiện chức năng này!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnEditTypeProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTypeProduct.SelectedItems.Count > 0)
                {
                    if (txtTypeProductName.Text == "")
                    {
                        MessageBox.Show("Bạn không thể cập nhật nếu như để trống một trường dữ liệu nào.", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        TypeProductDTO sp = lstTypeProduct.SelectedItems[0].Tag as TypeProductDTO;
                        sp.NameType = txtTypeProductName.Text;
                        if (radAnType.Checked)
                        {
                            sp.Status = 0;
                        }
                        else
                            sp.Status = 1;

                        if (TypeProductBUS.UpdateTypeProduct(sp))
                        {
                            ShowTypeProduct();
                            MessageBox.Show("Đã cập nhật loại sản phẩm thành công", "Thông báo", MessageBoxButtons.OK);
                            DeleteTextType();
                            LoadTypeProduct(cbLocLoaiSP);
                            LoadTypeProduct(cbTypeProduct);
                        }
                        else
                            MessageBox.Show("Bạn đã cập nhật loại sản phẩm thất bại, vui lòng kiểm tra thông tin nhập vào!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn vui lòng chọn sản phẩm trước khi thực hiện chức năng này!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetDimTimeForDoanhThu();
            SetAutoSizeCollumn();
            LoadRevenue();
        }

        private void btnNewAccount_Click(object sender, EventArgs e)
        {
            DeleteTextAccount();
            ShowAccount();
        }

        private void btnLamMoiNhanVien_Click(object sender, EventArgs e)
        {
            SetDateTime();
            SetEnabledComboboxNhanVienAndThang(true);
            LoadListEmployeeToComboboxAndListEmployeeSalary();
            SetAddStatusSalaryEmployee();
            txtCaMonth.Text = "";
            SetAutoSizeCollumn();
        }

        private void btnLamMoiNL_Click(object sender, EventArgs e) => SetClearInventoryView();

        private void button3_Click(object sender, EventArgs e) => LoadRevenue();

        private void btnThemLuong_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCaMonth.Text))
            {
                Double.TryParse(txtCaMonth.Text, out double ca);
                if (ca != 0)
                {
                    int idNhanvien = (cbxNhanVien.SelectedItem as AccountDetailDTO).AccountID;
                    double salary = (cbxNhanVien.SelectedItem as AccountDetailDTO).SalaryByCa;
                    int month = (cbxThang.SelectedItem as DimTime).Month;
                    int year = (cbxThang.SelectedItem as DimTime).Year;

                    if (idNhanvien != 0 &&
                        month != 0 &&
                        year != 0)
                    {
                        EmployeeHistoryDTO emp = new EmployeeHistoryDTO()
                        {
                            Year = year,
                            Month = month,
                            Ca = ca,
                            AccountID = idNhanvien,
                            SalaryByCa = salary
                        };

                        if (isAddSalary && EmployeeHistoryBUS.Add(emp))
                        {
                            btnLamMoiNhanVien_Click(sender, e);
                            MessageBox.Show("Tính lương thành công.");
                        }
                        else if (!isAddSalary && EmployeeHistoryBUS.Update(emp))
                        {
                            btnLamMoiNhanVien_Click(sender, e);
                            MessageBox.Show("Cập nhật lương thành công.");
                        }
                        else
                        {
                            MessageBox.Show("Tính lương thất bại.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số ca trong tháng cho nhân viên");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số ca trong tháng cho nhân viên");
            }
        }

        private void btnLuuNL_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenNL.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nguyên liệu.");
                return;
            }

            if (string.IsNullOrEmpty(txtGiaNL.Text))
            {
                MessageBox.Show("Vui lòng nhập giá nguyên liệu.");
                return;
            }

            Double.TryParse(txtGiaNL.Text, out double giaNL);
            if (giaNL <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá nguyên liệu.");
                return;
            }

            int id = lstNguyenLieu.SelectedItems.Count == 0 ? 0 : (lstNguyenLieu.SelectedItems[0].Tag as InventoryDTO) != null ? (lstNguyenLieu.SelectedItems[0].Tag as InventoryDTO).ID : 0;
            var inventory = new InventoryDTO()
            {
                ID = id,
                PriceBase = giaNL,
                Name = txtTenNL.Text,
                Note = txtGhiChuNL.Text
            };

            if (isAddNL && InventoryBUS.Add(inventory))
            {
                SetClearInventoryView();

                MessageBox.Show("Thêm nguyên vật liệu thành công.");
            }
            else if (!isAddNL && InventoryBUS.Update(inventory))
            {
                SetClearInventoryView();

                MessageBox.Show("Cập nhật nguyên vật liệu thành công.");
            }
            else
            {
                MessageBox.Show("Lưu nguyên vật liệu thất bại.");
            }
        }

        private void btnNewType_Click(object sender, EventArgs e) => DeleteTextType();

        private void btnOutAccount_Click(object sender, EventArgs e) => Close();

        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAccount.SelectedItems.Count > 0)
                {
                    AccountDTO acc = lstAccount.SelectedItems[0].Tag as AccountDTO;
                    frm_XacNhan frm_XN = new frm_XacNhan("Bạn vui lòng nhập mật khẩu để xác nhận thao tác này!");
                    if (frm_XN.ShowDialog() == DialogResult.OK)
                    {
                        if (AccountBUS.IsLogin(Program.sAccount.ID, frm_XN.txtXacNhap.Text))
                        {
                            if (AccountBUS.ResetAccount(acc.ID))
                            {
                                ShowAccount();
                                MessageBox.Show("Đã cập nhật reset thành công", "Thông báo", MessageBoxButtons.OK);
                                DeleteTextAccount();
                                txtNameAcount.ReadOnly = false;
                                btnEditAccount.Visible = false;
                                btnAddAccount.Visible = true;
                                btnDeleteAccount.Enabled = false;
                            }
                            else
                                MessageBox.Show("Reset mật khẩu thất bại", "Thông báo", MessageBoxButtons.OK);
                        }
                        else MessageBox.Show("Bạn đã nhập sai mật khẩu, vui lòng thử lại sau", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn tài khoản để thực hiện chức năng này!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            List<ProductDTO> list = ProductBUS.GeProductByName(txtSearchProduct.Text);
            ListProduct(list);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            rpt_ThongKe rptThongKe = new rpt_ThongKe();
            List<BillUpDTO> lstBill = BillBUS.GetListBillInAboutTime(dtpFromDate.Value, dtpToDate.Value);
            if (lstBill.Count > 0)
            {
                rptThongKe.XuatThongKeTheoThang(lstBill, dtpFromDate.Value, dtpToDate.Value, DateTime.Now, Program.sAccount.Name);
                rptThongKe.ShowDialog();
            }
            else
            {
                MessageBox.Show("Hiện tại trong khoảng thời gian này chưa có hóa đơn nào được tạo.", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int fromMonth = (cbxFromMonth.SelectedItem as DimTime).Month;
            int fromYear = (cbxFromMonth.SelectedItem as DimTime).Year;
            int toMonth = (cbxToMonth.SelectedItem as DimTime).Month;
            int toYear = (cbxToMonth.SelectedItem as DimTime).Year;

            var rpt_ThongKeDoanhThu = new rpt_ThongKeDoanhThu();
            var lstRevenue = RevenueBUS.GetRevenueByMonth(fromMonth, fromYear, toMonth, toYear);
            if (lstRevenue.Count > 0)
            {
                rpt_ThongKeDoanhThu.XuatThongKeTheoThang(lstRevenue, DateTime.Now, String.Format("{0}/{1}", fromMonth, fromYear), String.Format("{0}/{1}", toMonth, toYear), Program.sAccount.Name);
                rpt_ThongKeDoanhThu.ShowDialog();
            }
            else
            {
                MessageBox.Show("Hiện tại trong khoảng thời gian này chưa có hóa đơn nào được tạo.", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void button5_Click(object sender, EventArgs e) => LoadListBill();

        private void btnWatchProduct_Click(object sender, EventArgs e)=> DeleteTextProduct();

        private void btnXoaNL_Click(object sender, EventArgs e)
        {
            if (lstNguyenLieu.SelectedItems.Count > 0)
            {
                var invent = (lstNguyenLieu.SelectedItems[0].Tag as InventoryDTO);
                frm_XacNhan frm_XN = new frm_XacNhan("Bạn vui lòng nhập mật khẩu để xác nhận thao tác này!");
                if (frm_XN.ShowDialog() == DialogResult.OK)
                {
                    if (InventoryBUS.Delete(invent.ID))
                    {
                        SetClearInventoryView();

                        MessageBox.Show("Xóa nguyên vật liệu thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Xóa nguyên vật liệu thất bại.");
                    }
                }
            }
        }

        private void cbLocLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocLoaiSP.SelectedItem == null)
                return;
            TypeProductDTO typeProduct = cbLocLoaiSP.SelectedItem as TypeProductDTO;

            ListProduct(ProductBUS.GetListProductByIDTypeProduct(typeProduct.ID, -1));
        }

        private void txtIDTypeProduct_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTypeProductName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbTypeProduct_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = true;

        private void cbxThang_SelectedIndexChanged(object sender, EventArgs e) => LoadListEmployeeToComboboxAndListEmployeeSalary();

        private void DeleteTextAccount()
        {
            btnDeleteAccount.Enabled = false;
            txtPassword.Enabled = false;
            btnEditAccount.Visible = false;
            btnAddAccount.Visible = true;
            txtNameAcount.ReadOnly = false;
            txtNameAcount.Text = "";
            txtPassword.Text = "";
            txtHoTen.Text = "";
            txtTelephone.Text = "";
            txtCMND.Text = "";
            txtAddress.Text = "";
            radAd.Checked = false;
            radNguoiDung.Checked = true;
            radHienAccount.Checked = true;
            radAnAccount.Checked = false;
            txtNoiSinh.Text = "";
            btnDeleteAccount.Enabled = false;
            txtSalary.Text = "0";

            btnKhoiPhuc.Enabled = false;
        }

        private void DeleteTextProduct()
        {
            btnDeProduct.Enabled = false;
            btnAddProduct.Visible = true;
            btnEditProduct.Visible = false;
            txtProductName.Text = "";
            txtPriceProduct.Text = "";
            txtIDProduct.Text = "";
            radHien.Checked = true;
            ShowProduct();
            LoadListTypeProduct(cbLocLoaiSP);
            LoadListTypeProduct(cbTypeProduct);
        }
        private void DeleteTextType()
        {
            btnDeTypeProduct.Enabled = false;
            btnAddTypeProduct.Visible = true;
            btnEditTypeProduct.Visible = false;
            txtTypeProductName.Text = "";
            txtIDTypeProduct.Text = "";
            radHienType.Checked = true;
            ShowTypeProduct();
        }

        private void frm_QuanLyHeThong_Load(object sender, EventArgs e)
        {
            LoadTypeProduct(cbTypeProduct);
            ShowAccount();
            ShowTypeProduct();

            LoadMonth();
            LoadMonthDoanhThu();

            LoadListEmployeeToComboboxAndListEmployeeSalary();
            LoadListInventory();
            LoadRevenue();

            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpToDate.Value = DateTime.Now;

            cbTypeProduct.ContextMenu = new ContextMenu();
            cbLocLoaiSP.ContextMenu = new ContextMenu();
            txtPassword.Enabled = false;
            btnKhoiPhuc.Enabled = false;

            LoadListBill();
            LoadProductSize();

            List<TypeProductDTO> listtype = TypeProductBUS.GetAllListTypeProduct();
            cbLocLoaiSP.DataSource = listtype;
            cbLocLoaiSP.ValueMember = "ID";
            cbLocLoaiSP.DisplayMember = "NameType";

            if (cbLocLoaiSP.SelectedItem != null)
            {
                lstProduct.Items.Clear();
                ListProduct(ProductBUS.GetListProductByIDTypeProduct(Convert.ToInt32(cbLocLoaiSP.SelectedValue), -1));
            }

            SetAutoSizeCollumn();
        }
        private List<DimTime> GetDimTimes()
        {
            List<DimTime> dimTime = new List<DimTime>();
            int nLen = 12;
            for (int year = 2019; year <= DateTime.Now.Year; year++)
            {
                if (year == DateTime.Now.Year)
                {
                    nLen = DateTime.Now.Month;
                }

                for (int month = 1; month <= nLen; month++)
                {
                    string monthFormat = month < 10 ? "0" + month : month.ToString();
                    DimTime dim = new DimTime()
                    {
                        Year = year,
                        Month = month,
                        Format = string.Format("{0}{1}", year, monthFormat),
                        Text = string.Format("Tháng {0} - Năm {1}", monthFormat, year)
                    };
                    dimTime.Add(dim);
                }
            }

            return dimTime;
        }

        private void ListProduct(List<ProductDTO> menulist)
        {
            lstProduct.Items.Clear();
            for (int i = 0; i < menulist.Count; i++)
            {
                ListViewItem listitem = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };

                listitem.SubItems.Add("SP00" + menulist[i].ID);
                listitem.SubItems.Add(menulist[i].NameProducts);
                listitem.SubItems.Add(menulist[i].Size);
                if (menulist[i].PriceBasic == 0)
                {
                    listitem.SubItems.Add("Miễn phí");
                }
                else listitem.SubItems.Add(menulist[i].PriceBasic.ToString("0,000"));

                if (menulist[i].SalePrice == menulist[i].PriceBasic)
                    listitem.SubItems.Add("Không có chương trình khuyến mãi");
                else
                {
                    int phantram = (int)((menulist[i].SalePrice * 100) / menulist[i].PriceBasic);
                    listitem.SubItems.Add("Đang giảm " + phantram + "%");
                }

                if (menulist[i].Status == 1)
                {
                    listitem.SubItems.Add("Đang họat động");
                }
                else
                {
                    listitem.SubItems.Add("Ngưng bán");
                }

                listitem.Tag = menulist[i];
                lstProduct.Items.Add(listitem);
            }
        }

        private void LoadListBill()
        {
            lstRevenue.Items.Clear();
            double totalReport = 0;
            List<BillUpDTO> list = BillBUS.GetListBillInAboutTime(dtpFromDate.Value, dtpToDate.Value);
            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem listitem = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };
                listitem.SubItems.Add("HD00" + list[i].ID.ToString());
                listitem.SubItems.Add(list[i].Total == 0 ? "-" : list[i].Total.ToString("###,### VNĐ"));
                listitem.SubItems.Add(list[i].PromotionPrice == 0 ? "-" : list[i].PromotionPrice.ToString("###,### VNĐ"));
                listitem.SubItems.Add(list[i].CustomerPrice == 0 ? "-" : list[i].CustomerPrice.ToString("###,### VNĐ"));
                listitem.SubItems.Add(list[i].OutPrice == 0 ? "-" : list[i].OutPrice.ToString("###,### VNĐ"));
                listitem.SubItems.Add(list[i].Revenue == 0 ? "-" : list[i].Revenue.ToString("###,### VNĐ"));
                listitem.SubItems.Add(list[i].CreateDay.ToString("HH:mm:ss dd/MM/yyyy"));
                listitem.SubItems.Add(AccountBUS.GetNameByAccount(list[i].Employ).ToString());
                listitem.Tag = list[i];
                lstRevenue.Items.Add(listitem);

                totalReport += list[i].Revenue;
            }

            txtReportTotal.Text = "0";
            if (totalReport > 0)
            {
                txtReportTotal.Text = totalReport.ToString("###,### VNĐ");
            }
        }

        private void LoadListEmployeeSalaried(int month, int year)
        {
            lstLuongNhanVien.Items.Clear();
            List<AccountDetailDTO> list = EmployeeHistoryBUS.GetEmployeeByMonth(month, year, true);
            double tongTien = 0;
            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem listitem = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };
                listitem.SubItems.Add("03060" + list[i].AccountID.ToString());
                listitem.SubItems.Add(list[i].Name);
                listitem.SubItems.Add(list[i].Telephone);
                listitem.SubItems.Add(list[i].Address);
                listitem.SubItems.Add(list[i].Month.ToString());
                listitem.SubItems.Add(list[i].Year.ToString());
                listitem.SubItems.Add(list[i].SalaryByCa.ToString("###,### VNĐ"));
                listitem.SubItems.Add(list[i].Ca.ToString());
                listitem.SubItems.Add(list[i].Total.ToString("###,### VNĐ"));

                listitem.Tag = list[i];
                tongTien += list[i].Total;

                lstLuongNhanVien.Items.Add(listitem);
            }

            txtTongTienLuong.Text = "0";
            if (tongTien > 0)
            {
                txtTongTienLuong.Text = tongTien.ToString("###,### VNĐ");
            }
        }

        private void LoadListEmployeeToComboboxAndListEmployeeSalary()
        {
            if (cbxThang.SelectedItem == null)
            {
                return;
            }

            int year = (cbxThang.SelectedItem as DimTime).Year;
            int month = (cbxThang.SelectedItem as DimTime).Month;

            List<AccountDetailDTO> listtype = EmployeeHistoryBUS.GetEmployeeByMonth(month, year);
            if (listtype.Count != 0)
            {
                SetComboxNV(listtype);
                SetEnabledComboxNhanVien(true);
            }
            else
            {
                cbxNhanVien.SelectedItem = null;
                SetEnabledComboxNhanVien(false);
            }

            LoadListEmployeeSalaried(month, year);
        }

        private void LoadListInventory()
        {
            lstNguyenLieu.Items.Clear();
            List<InventoryDTO> lst = InventoryBUS.GetInventoryByTimeNow();
            double tongTien = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                ListViewItem listitem = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };
                listitem.SubItems.Add("IN00" + lst[i].ID.ToString());
                listitem.SubItems.Add(lst[i].Name);
                listitem.SubItems.Add(lst[i].CreatedDate.ToString("dd/MM/yyyy"));
                listitem.SubItems.Add(lst[i].PriceBase.ToString("###,### VNĐ"));
                listitem.SubItems.Add(lst[i].Note);

                listitem.Tag = lst[i];
                tongTien += lst[i].PriceBase;
                lstNguyenLieu.Items.Add(listitem);
            }

            txtTongGiaNL.Text = "0";
            if (tongTien > 0)
            {
                txtTongGiaNL.Text = tongTien.ToString("###,### VNĐ");
            }
        }

        private void LoadListTypeProduct(ComboBox cbx)
        {
            List<TypeProductDTO> listtype = TypeProductBUS.GetAllListTypeProduct();
            cbx.DataSource = listtype;
            cbx.ValueMember = "ID";
            cbx.DisplayMember = "NameType";
        }

        private void LoadMonth()
        {
            List<DimTime> dimTime = GetDimTimes();

            cbxThang.DataSource = dimTime;
            cbxThang.DisplayMember = "Text";
            cbxThang.ValueMember = "Format";

            SetDateTime();
        }

        private void LoadMonthDoanhThu()
        {
            List<DimTime> dimTime = GetDimTimes();

            cbxFromMonth.DataSource = dimTime;
            cbxFromMonth.DisplayMember = "Text";
            cbxFromMonth.ValueMember = "Format";

            cbxToMonth.DataSource = dimTime.ToArray();
            cbxToMonth.DisplayMember = "Text";
            cbxToMonth.ValueMember = "Format";

            SetDimTimeForDoanhThu();
        }

        private void LoadProductSize()
        {
            var list = ProductSizes.List();
            cbxProductSize.DataSource = list;
            cbxProductSize.DisplayMember = "Name";
        }

        private void LoadRevenue()
        {
            if (cbxFromMonth.SelectedItem == null || cbxToMonth.SelectedItem == null)
            {
                return;
            }

            int fromMonth = (cbxFromMonth.SelectedItem as DimTime).Month;
            int fromYear = (cbxFromMonth.SelectedItem as DimTime).Year;
            int toMonth = (cbxToMonth.SelectedItem as DimTime).Month;
            int toYear = (cbxToMonth.SelectedItem as DimTime).Year;

            lstDoanhThu.Items.Clear();
            List<RevenueDTO> lst = RevenueBUS.GetRevenueByMonth(fromMonth, fromYear, toMonth, toYear);
            double tongTien = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                ListViewItem listitem = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };
                listitem.SubItems.Add(lst[i].Month == 0 ? "Tổng" : lst[i].Month.ToString());
                listitem.SubItems.Add(lst[i].Year == 0 ? "Tổng" : lst[i].Year.ToString());

                listitem.SubItems.Add(lst[i].TotalBill == 0 ? "-" : lst[i].TotalBill.ToString("###,### VNĐ"));

                listitem.SubItems.Add(lst[i].TotalInventory == 0 ? "-" : lst[i].TotalInventory.ToString("###,### VNĐ"));

                listitem.SubItems.Add(lst[i].TotalSalary == 0 ? "-" : lst[i].TotalSalary.ToString("###,### VNĐ"));

                listitem.SubItems.Add(lst[i].TotalRevenue == 0 ? "-" : lst[i].TotalRevenue.ToString("###,### VNĐ"));

                listitem.Tag = lst[i];
                if (lst[i].Month != 0)
                {
                    tongTien += lst[i].TotalRevenue;
                }

                lstDoanhThu.Items.Add(listitem);
            }

            txtTongDoanhThu.Text = "0";
            if (tongTien != 0)
            {
                txtTongDoanhThu.Text = tongTien.ToString("###,### VNĐ");
            }
        }

        private void LoadTypeProduct(ComboBox cmb)
        {
            List<TypeProductDTO> listtype = TypeProductBUS.GetListTypeProductWithStatusOne(1);
            cmb.DataSource = listtype;
            cmb.ValueMember = "ID";
            cmb.DisplayMember = "NameType";
        }

        private void lstLuongNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstLuongNhanVien.SelectedItems.Count > 0)
            {
                var nv = (lstLuongNhanVien.SelectedItems[0].Tag as AccountDetailDTO);
                var lst = new List<AccountDetailDTO>() { nv };
                SetComboxNV(lst);
                SetNhanVien(nv.AccountID);
                SetDateTime(nv.Month, nv.Year);
                SetEnabledComboboxNhanVienAndThang(false);
                btnThemLuong.Enabled = true;
                txtCaMonth.Text = nv.Ca.ToString();

                SetAddStatusSalaryEmployee(false);
            }
        }

        private void lstAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNameAcount.ReadOnly = true;
            if (lstAccount.SelectedItems.Count > 0)
            {
                btnKhoiPhuc.Enabled = true;
                btnDeleteAccount.Enabled = true;
                txtPassword.ReadOnly = true;
                btnEditAccount.Visible = true;
                btnAddAccount.Visible = false;
                ListViewItem lvw = lstAccount.SelectedItems[0];
                AccountDTO sp = (AccountDTO)lvw.Tag;
                txtNameAcount.Text = "03060" + sp.ID.ToString();
                txtPassword.Text = sp.PassWord;
                txtHoTen.Text = sp.Name;
                txtCMND.Text = sp.PassPort;
                txtNoiSinh.Text = sp.PlaceOfBirth;
                txtTelephone.Text = sp.Telephone;
                txtAddress.Text = sp.Address;
                txtSalary.Text = sp.SalaryByCa.ToString();
                if (sp.Right == 0)
                {
                    radAd.Checked = true;
                }
                else
                    radNguoiDung.Checked = true;
                if (sp.Status == 0)
                {
                    radAnAccount.Checked = true;
                }
                else
                {
                    radHienAccount.Checked = true;
                }
            }
        }

        private void lstNguyenLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNguyenLieu.SelectedItems.Count > 0)
            {
                var invent = (lstNguyenLieu.SelectedItems[0].Tag as InventoryDTO);
                txtMaNL.Text = "IN00" + invent.ID.ToString();
                txtTenNL.Text = invent.Name;
                txtGiaNL.Text = invent.PriceBase.ToString();
                txtGhiChuNL.Text = invent.Note;

                SetEnabledDeleteInventory(true);
                SetAddNguyenLieu(false);
            }
        }

        private void lstProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstProduct.SelectedItems.Count > 0)
            {
                btnDeProduct.Enabled = true;
                btnAddProduct.Visible = false;
                btnEditProduct.Visible = true;
                ListViewItem lvw = lstProduct.SelectedItems[0];
                ProductDTO sp = (ProductDTO)lvw.Tag;
                txtIDProduct.Text = "SP00" + sp.ID.ToString();
                txtProductName.Text = sp.NameProducts;
                txtPriceProduct.Text = sp.PriceBasic.ToString();
                cbTypeProduct.SelectedValue = sp.IDTypeProduct;
                if (sp.Status == 1)
                {
                    radHien.Checked = true;
                }
                else
                    radAn.Checked = true;
            }
        }

        private void lstRevenue_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstRevenue.SelectedItems.Count > 0)
                {
                    BillUpDTO bill = lstRevenue.SelectedItems[0].Tag as BillUpDTO;
                    DialogResult kq = MessageBox.Show("Bạn có muốn xem lại hóa đơn HD00" + bill.ID + " này có gì không?", "Thông báo", MessageBoxButtons.OKCancel);
                    if (kq == DialogResult.OK)
                    {
                        rpt_ThanhToan frm_TToan = new rpt_ThanhToan();
                        frm_TToan.XuatHoaDon(bill.ID, "HÓA ĐƠN ĐÃ THANH TOÁN", bill.ID.ToString(), AccountBUS.GetNameByAccount(bill.Employ), bill.CreateDay, bill.Total.ToString(), bill.PromotionPrice.ToString(), bill.CustomerPrice.ToString(), bill.OutPrice.ToString(), bill.Revenue.ToString(), false);

                        frm_TToan.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lstTypeProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTypeProduct.SelectedItems.Count > 0)
            {
                btnEditTypeProduct.Visible = true;
                btnAddTypeProduct.Visible = false;
                btnDeTypeProduct.Enabled = true;
                ListViewItem lvw = lstTypeProduct.SelectedItems[0];
                TypeProductDTO sp = (TypeProductDTO)lvw.Tag;
                txtIDTypeProduct.Text = "LSP00" + sp.ID.ToString();
                txtTypeProductName.Text = sp.NameType;
                if (sp.Status == 1)
                {
                    radHienType.Checked = true;
                }
                else
                    radAnType.Checked = true;
            }
        }
        private void SetAddNguyenLieu(bool isAdd = true)
        {
            btnLuuNL.Text = isAdd ? "Thêm mới" : "Chỉnh sửa";
            isAddNL = isAdd;
        }

        private void SetAddStatusSalaryEmployee(bool isAdd = true)
        {
            btnThemLuong.Text = isAdd ? "Thêm mới" : "Chỉnh sửa";
            isAddSalary = isAdd;
        }

        private void SetAutoSizeCollumn()
        {
            lstLuongNhanVien.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstLuongNhanVien.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            lstNguyenLieu.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstNguyenLieu.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            lstRevenue.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstRevenue.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            lstAccount.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstAccount.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            lstProduct.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstProduct.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            lstDoanhThu.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstDoanhThu.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void SetClearInventoryView()
        {
            txtTenNL.Text = "";
            txtGiaNL.Text = "";
            txtGhiChuNL.Text = "";

            LoadListInventory();
            SetEnabledDeleteInventory(false);
            SetAddNguyenLieu(true);

            SetAutoSizeCollumn();
        }

        private void SetComboxNV(List<AccountDetailDTO> lst)
        {
            cbxNhanVien.DataSource = lst;
            cbxNhanVien.DisplayMember = "Name";
            cbxNhanVien.ValueMember = "AccountID";
        }

        private void SetDateTime() => cbxThang.SelectedValue = string.Format("{0}{1}", DateTime.Now.Month, DateTime.Now.Year);

        private void SetDateTime(int month, int year) => cbxThang.SelectedValue = string.Format("{0}{1}", month, year);

        private void SetDimTimeForDoanhThu()
        {
            cbxFromMonth.SelectedValue = string.Format("{0}{1}", 1, DateTime.Now.Year);

            cbxToMonth.SelectedValue = string.Format("{0}{1}", DateTime.Now.Month, DateTime.Now.Year);
        }

        private void SetEnabledComboboxNhanVienAndThang(bool value)
        {
            cbxThang.Enabled = value;
            SetEnabledComboxNhanVien(value);
        }

        private void SetEnabledComboxNhanVien(bool value)
        {
            btnThemLuong.Enabled = value;
            cbxNhanVien.Enabled = value;
        }

        private void SetEnabledDeleteInventory(bool isEnabled) => btnXoaNL.Enabled = isEnabled;

        private void SetNhanVien(int id) => cbxNhanVien.SelectedValue = id;

        private void ShowAccount()
        {
            lstAccount.Items.Clear();
            List<AccountDTO> menulist = AccountBUS.GetAllListAccount();
            for (int i = 0; i < menulist.Count; i++)
            {
                ListViewItem listitem = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };
                listitem.SubItems.Add("03060" + menulist[i].ID.ToString());
                listitem.SubItems.Add(menulist[i].Name);
                listitem.SubItems.Add(menulist[i].PlaceOfBirth);
                listitem.SubItems.Add(menulist[i].Telephone);
                listitem.SubItems.Add(menulist[i].Address);
                if (menulist[i].Right == 0)
                {
                    listitem.SubItems.Add("Quản lý");
                }
                else
                {
                    listitem.SubItems.Add("Nhân viên");
                }
                listitem.SubItems.Add(menulist[i].SalaryByCa.ToString("###,### VNĐ"));
                if (menulist[i].Status == 0)
                {
                    listitem.SubItems.Add("Bị khóa");
                }
                else
                    listitem.SubItems.Add("Đã được mở khóa");
                listitem.SubItems.Add(menulist[i].PassPort);
                listitem.Tag = menulist[i];
                lstAccount.Items.Add(listitem);
            }
        }

        private void ShowProduct()
        {
            lstProduct.Items.Clear();
            List<ProductDTO> menulist = ProductBUS.GetAllListProduct();
            ListProduct(menulist);
        }

        private void ShowTypeProduct()
        {
            lstTypeProduct.Items.Clear();
            List<TypeProductDTO> menulist = TypeProductBUS.GetAllListTypeProduct();
            for (int i = 0; i < menulist.Count; i++)
            {
                ListViewItem listitem = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };
                listitem.SubItems.Add(menulist[i].NameType.ToString());
                if (menulist[i].Status == 1)
                {
                    listitem.SubItems.Add("Đang họat động");
                }
                else
                    listitem.SubItems.Add("Khóa");
                listitem.SubItems.Add("LSP00" + menulist[i].ID.ToString());
                listitem.Tag = menulist[i];
                lstTypeProduct.Items.Add(listitem);
            }
        }
        private class DimTime
        {
            public string Format { get; set; }
            public int Month { get; set; }

            public string Text { get; set; }
            public int Year { get; set; }
        }

        private void txtNameAcount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel30_Paint(object sender, PaintEventArgs e)
        {

        }
        private void TxtKeyPress_Number(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt32(e.KeyChar) >= 48 && Convert.ToInt32(e.KeyChar) <= 57) || Convert.ToInt32(e.KeyChar) == 8)
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }


        private void txtNameAcount_KeyPress_1(object sender, KeyPressEventArgs e) => TxtKeyPress_Number(sender, e);
        
    }
}
