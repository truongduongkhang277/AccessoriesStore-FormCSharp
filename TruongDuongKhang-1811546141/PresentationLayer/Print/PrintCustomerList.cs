using System;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.ReportGenerator.CrystalReports;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class PrintCustomerList : Form
    {
        public PrintCustomerList()
        {
            InitializeComponent();
        }

        private void previewArea_Load(object sender, EventArgs e)
        {
            crptCustomer crpt = new crptCustomer();
            crpt.SetDataSource(new BusCustomer().getData().Tables[0]);
            this.previewArea.ReportSource = crpt;
            this.previewArea.RefreshReport();
        }
    }
}
