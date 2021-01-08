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
                        Frm_XacNhan frm_XN = new Frm_XacNhan("Bạn vui lòng nhập mật khẩu để xác nhận thao tác này!");
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
                    Frm_XacNhan frm_XN = new Frm_XacNhan("Xóa một sản phẩm rất quan trọng. Bạn vui lòng nhập mật khẩu để xác nhận thao tác này!");
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
                    Frm_XacNhan frm_XN = new Frm_XacNhan("Vui lòng nhập mật khẩu để xác nhận thao tác này!");
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
                            BtnLamMoiNhanVien_Click(sender, e);
                            MessageBox.Show("Tính lương thành công.");
                        }
                        else if (!isAddSalary && EmployeeHistoryBUS.Update(emp))
                        {
                            BtnLamMoiNhanVien_Click(sender, e);
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
                    Frm_XacNhan frm_XN = new Frm_XacNhan("Bạn vui lòng nhập mật khẩu để xác nhận thao tác này!");
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
            RptThongKe rptThongKe = new RptThongKe();
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

            var rptThongKeDoanhThu = new rptThongKeDoanhThu();
            var lstRevenue = RevenueBUS.GetRevenueByMonth(fromMonth, fromYear, toMonth, toYear);
            if (lstRevenue.Count > 0)
            {
                rptThongKeDoanhThu.XuatThongKeTheoThang(lstRevenue, DateTime.Now, String.Format("{0}/{1}", fromMonth, fromYear), String.Format("{0}/{1}", toMonth, toYear), Program.sAccount.Name);
                rptThongKeDoanhThu.ShowDialog();
            }
            else
            {
                MessageBox.Show("Hiện tại trong khoảng thời gian này chưa có hóa đơn nào được tạo.", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void button5_Click(object sender, EventArgs e) => LoadListBill();

        private void btnWatchProduct_Click(object sender, EventArgs e)=> DeleteTextProduct();
       
    }
}
