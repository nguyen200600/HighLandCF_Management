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
using Microsoft.Reporting.WinForms;

namespace HighLandCF_Management
{
    public partial class rpt_ThanhToan : Form
    {
        public rpt_ThanhToan()
        {
            InitializeComponent();
        }
        public void XuatHoaDon(int MaHD, string TenHD, string Ban, string nhanvien, DateTime thoigian, string tongtien, string khuyenmai, string tienkhachhang, string tienton, string thanhtien, bool xemlaihoadon)
        {
            List<MenuDTO> lstProduct = BUS_Highland.MenuBUS.GetListMenuByIDBill(MaHD);
            if (!xemlaihoadon)
            {
                lstProduct = BUS_Highland.MenuBUS.GetReviewBill(MaHD);
            }

            rptXuatHD.LocalReport.ReportEmbeddedResource = "HighLandCF_Management.rpt_ThanhToan.rdlc";
            if (tienton != "0")
            {
                tienton = string.Format("{0:0,0}", Convert.ToDouble(tienton).ToString("0,0"));
            }

            if (tienkhachhang != "0")
            {
                tienkhachhang = string.Format("{0:0,0}", "" + Convert.ToDouble(tienkhachhang).ToString("0,0") + "");
            }

            if (tongtien != "0")
            {
                tongtien = string.Format("{0:0,0}", Convert.ToDouble(tongtien).ToString("0,0"));
            }

            if (khuyenmai != "0")
            {
                khuyenmai = string.Format("{0:0,0}", Convert.ToDouble(khuyenmai).ToString("0,0"));
            }

            if (thanhtien != "0")
            {
                thanhtien = string.Format("{0:0,0}", Convert.ToDouble(thanhtien).ToString("0,0"));
            }

            rptXuatHD.LocalReport.DataSources.Add(new ReportDataSource("dtThanhToan", lstProduct));
            rptXuatHD.LocalReport.SetParameters(new ReportParameter("paraHD", "HD00" + MaHD.ToString(), false));
            rptXuatHD.LocalReport.SetParameters(new ReportParameter("paraNV", nhanvien, false));
            rptXuatHD.LocalReport.SetParameters(new ReportParameter("paraThoiGian", thoigian.ToString(), false));
            rptXuatHD.LocalReport.SetParameters(new ReportParameter("paraTongTien", tongtien, false));
            rptXuatHD.LocalReport.SetParameters(new ReportParameter("paraKhuyenMai", khuyenmai, false));
            rptXuatHD.LocalReport.SetParameters(new ReportParameter("paraTienKhachTra", tienkhachhang, false));
            rptXuatHD.LocalReport.SetParameters(new ReportParameter("paraTienTon", tienton, false));
            rptXuatHD.LocalReport.SetParameters(new ReportParameter("paraThanhTien", thanhtien, false));
            rptXuatHD.LocalReport.SetParameters(new ReportParameter("paraNameBill", TenHD, false));
            rptXuatHD.LocalReport.SetParameters(new ReportParameter("paraBan", Ban, false));

            rptXuatHD.RefreshReport();
        }
        private void rpt_ThanhToan_Load(object sender, EventArgs e)
        {

            this.rptXuatHD.RefreshReport();
        }
    }
}
