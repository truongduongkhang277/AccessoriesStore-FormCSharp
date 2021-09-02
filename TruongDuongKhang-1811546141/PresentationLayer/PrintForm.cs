using System;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.Report;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class PrintForm : Form
    {
        private int status;
        public PrintForm(int status)
        {
            InitializeComponent();
            this.status = status;
        }

        private void previewArea_Load(object sender, EventArgs e)
        {
            //Report.OrderList report = new Report.OrderList();
            //report.SetDataSource(new BusOrder().getData().Tables[0]);
            //this.previewArea.ReportSource = report;
            //this.previewArea.RefreshReport();
        }
    }
}
