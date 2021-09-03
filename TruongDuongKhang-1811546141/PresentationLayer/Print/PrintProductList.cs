using System;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.ReportGenerator.CrystalReports;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class PrintProductList : Form
    {
        private bool isActive;
        private int cateId;

        public PrintProductList(bool isActive, int cateId)
        {
            InitializeComponent();

            this.cateId = cateId;
            this.isActive = isActive;
        }

        private void previewArea_Load(object sender, EventArgs e)
        {
            crptProduct crpt = new crptProduct();
            crpt.SetDataSource(new BusProduct().getData(isActive, cateId).Tables[0]);
            this.previewArea.ReportSource = crpt;
            this.previewArea.RefreshReport();
        }
    }
}
