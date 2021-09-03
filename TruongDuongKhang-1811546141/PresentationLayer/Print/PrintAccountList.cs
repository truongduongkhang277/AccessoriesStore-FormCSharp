using System;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.ReportGenerator.CrystalReports;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class PrintAccountList : Form
    {
        private bool isActive;
        private int roleId;

        public PrintAccountList(bool isActive, int roleId)
        {
            InitializeComponent();
            this.roleId = roleId;
            this.isActive = isActive;
        }

        private void previewArea_Load(object sender, EventArgs e)
        {
            crptAccountList crpt = new crptAccountList();
            crpt.SetDataSource(new BusAccount().getData(isActive, roleId).Tables[0]);
            this.previewArea.ReportSource = crpt;
            this.previewArea.RefreshReport();
        }
    }
}
