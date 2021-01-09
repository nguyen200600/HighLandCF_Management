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
    public partial class rpt_ThongKeDoanhThu : Form
    {
        public rpt_ThongKeDoanhThu()
        {
            InitializeComponent();
        }

        private void rpt_ThongKeDoanhThu_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
        private double TinhTongDoanhThu(List<RevenueDTO> lstRevenue)
        {
            double tong = 0;
            foreach (RevenueDTO rev in lstRevenue)
            {
                if (rev.Month != 0)
                {
                    tong += rev.TotalRevenue;
                }
            }
            return tong;
        }
        public void XuatThongKeTheoThang(List<RevenueDTO> lstRevenue, DateTime timeNow, string fromTime, string toTime, string NhanVien)
        {
            if (lstRevenue.Count > 0)
            {
                double totalRevenue = TinhTongDoanhThu(lstRevenue);

                reportViewer1.LocalReport.ReportEmbeddedResource = "HighLandCF_Management.rptThongKeDoanhThu.rdlc";
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dtThongKe", lstRevenue));
                reportViewer1.LocalReport.SetParameters(new ReportParameter("paraThoiGian", timeNow.ToString(), false));
                reportViewer1.LocalReport.SetParameters(new ReportParameter("paraThoiGianTu", fromTime, false));
                reportViewer1.LocalReport.SetParameters(new ReportParameter("paraThoiGianDen", toTime, false));
                reportViewer1.LocalReport.SetParameters(new ReportParameter("paraNV", NhanVien, false));
                reportViewer1.LocalReport.SetParameters(new ReportParameter("paraTongTien", totalRevenue.ToString("0,0 VNĐ"), false));

                reportViewer1.RefreshReport();
            }
        }
    }
}
