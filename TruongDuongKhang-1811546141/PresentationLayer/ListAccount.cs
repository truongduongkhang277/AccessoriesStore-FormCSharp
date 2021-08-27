using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;

namespace TruongDuongKhang_1811546141.PresentationLayer
{
    public partial class ListAccount : Form
    {
        private DataSet ds;
        private DataViewManager dsView;
        private bool isActive;

        public ListAccount(bool isActive)
        {
            InitializeComponent();
            // gán giá trị cho isActive bằng giá trị được truyền vào khi gọi hàm tại main
            this.isActive = isActive;
            // khởi chạy truyền dữ liệu cho combobox
            loadDataToCombobox();
            // khởi chạy truyền dữ liệu cho datagridview
            loadDataGridView(this.isActive, 0);
        }

        // truyền dữ liệu cho combobox
        private void loadDataToCombobox()
        {
            this.cbbRole.DataSource = new BusRole().getData().Tables[0];
            this.cbbRole.DisplayMember = "RoleName";
            this.cbbRole.ValueMember = "RoleId";
            this.cbbRole.SelectedIndex = -1;
        }

        private void loadDataGridView(bool isActive, int roleId)
        {
            BusAccount busAccount = new BusAccount();
            ds = busAccount.getData(isActive, roleId);

            // đặt hiển thị mặc định
            if (ds != null)
            {
                dsView = ds.DefaultViewManager;
            }
            else
            {
                dsView = null;
            }
            // chuyển dữ liệu đến dataGridView
            this.dgvAccount.DataSource = dsView;
            this.dgvAccount.DataMember = "TblAccount";


        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            int roleId = int.Parse(this.cbbRole.SelectedValue.ToString());
            this.loadDataGridView(isActive, roleId);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
