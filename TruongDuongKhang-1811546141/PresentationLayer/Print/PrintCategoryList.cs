using System;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.ReportGenerator.CrystalReports;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class PrintCategoryList : Form
    {
        public PrintCategoryList()
        {
            InitializeComponent();
        }

        private void previewArea_Load(object sender, EventArgs e)
        {
            crptCategory crpt = new crptCategory();
            crpt.SetDataSource(new BusCategory().getData().Tables[0]);
            this.previewArea.ReportSource = crpt;
            this.previewArea.RefreshReport();
        }
    }
}
