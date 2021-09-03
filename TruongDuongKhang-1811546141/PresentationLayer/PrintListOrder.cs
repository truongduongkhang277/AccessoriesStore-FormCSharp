using System;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.ReportGenerator.CrystalReports;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class PrintListOrder : Form
    {
        private int status;

        public PrintListOrder(int status)
        {
            InitializeComponent();
            this.status = status;
        }

        private void previewArea_Load(object sender, EventArgs e)
        {
            crptOrderList crpt = new crptOrderList();
            crpt.SetDataSource(new BusOrder().getData(this.status, "", "", 0).Tables[0]);
            this.previewArea.ReportSource = crpt;
            this.previewArea.RefreshReport();
        }
    }
}
