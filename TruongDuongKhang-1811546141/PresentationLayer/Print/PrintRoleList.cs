using System;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.ReportGenerator.CrystalReports;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class PrintRoleList : Form
    {
        public PrintRoleList()
        {
            InitializeComponent();
        }

        private void previewArea_Load(object sender, EventArgs e)
        {
            crptRole crpt = new crptRole();
            crpt.SetDataSource(new BusRole().getData().Tables[0]);
            this.previewArea.ReportSource = crpt;
            this.previewArea.RefreshReport();
        }
    }
}
