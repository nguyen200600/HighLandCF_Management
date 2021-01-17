
namespace HighLandCF_Management
{
    partial class rpt_ThanhToan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rptXuatHD = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rptXuatHD
            // 
            this.rptXuatHD.Location = new System.Drawing.Point(-1, -2);
            this.rptXuatHD.Name = "rptXuatHD";
            this.rptXuatHD.ServerReport.BearerToken = null;
            this.rptXuatHD.Size = new System.Drawing.Size(603, 750);
            this.rptXuatHD.TabIndex = 0;
            // 
            // rpt_ThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 745);
            this.Controls.Add(this.rptXuatHD);
            this.Name = "rpt_ThanhToan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "rpt_ThanhToan";
            this.Load += new System.EventHandler(this.rpt_ThanhToan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rptXuatHD;
    }
}