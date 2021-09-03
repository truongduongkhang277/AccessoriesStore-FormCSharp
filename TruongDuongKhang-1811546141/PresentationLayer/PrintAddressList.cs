using System;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.ReportGenerator.CrystalReports;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class PrintAddressList : Form
    {
        public PrintAddressList()
        {
            InitializeComponent();
        }

        private void previewArea_Load(object sender, EventArgs e)
        {
            crptAddressList crpt = new crptAddressList();
            crpt.SetDataSource(new BusAddress().getData().Tables[0]);
            this.previewArea.ReportSource = crpt;
            this.previewArea.RefreshReport();
        }
    }
}
