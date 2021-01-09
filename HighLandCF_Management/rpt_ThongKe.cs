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
    public partial class rpt_ThongKe : Form
    {
        public rpt_ThongKe()
        {
            InitializeComponent();
        }

        private void rpt_ThongKe_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
        private double TinhTongDoanhThu(List<BillUpDTO> lstBill)
        {
            double Tong = 0;
            foreach (BillUpDTO bill in lstBill)
            {
                Tong += bill.Revenue;
            }
            return Tong;
        }
        public void XuatThongKeTheoThang(List<BillUpDTO> lstBill, DateTime ThoiGianTu, DateTime ThoiGianDen, DateTime ThoiGianLap, string NhanVien)
        {
            if (lstBill.Count > 0)
            {
                double TongDoanhThuThang = TinhTongDoanhThu(lstBill);

                reportViewer1.LocalReport.ReportEmbeddedResource = "HighLandCF_Management.rptThongKe.rdlc";
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dtThongKe", lstBill));
                reportViewer1.LocalReport.SetParameters(new ReportParameter("paraThoiGian", ThoiGianLap.ToString(), false));
                reportViewer1.LocalReport.SetParameters(new ReportParameter("paraThoiGianTu", ThoiGianTu.ToString("dd/MM/yyyy"), false));
                reportViewer1.LocalReport.SetParameters(new ReportParameter("paraThoiGianDen", ThoiGianDen.ToString("dd/MM/yyyy"), false));
                reportViewer1.LocalReport.SetParameters(new ReportParameter("paraNV", NhanVien, false));
                reportViewer1.LocalReport.SetParameters(new ReportParameter("paraTongTien", TongDoanhThuThang.ToString("0,0 VNĐ"), false));

                reportViewer1.RefreshReport();
            }
        }
    }
}
