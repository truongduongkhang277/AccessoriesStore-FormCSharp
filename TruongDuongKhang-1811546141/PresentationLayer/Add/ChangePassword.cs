using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;
using TruongDuongKhang_1811546141.Lib;

namespace TruongDuongKhang_1811546141.PresentationLayer.Add
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string username = SecurityObject.accInfo.Username;
            string password = this.txtPasswordNew.Text.Trim();

            if (this.txtPasswordNew.Text.Trim().Equals(this.txtPasswordConfirm.Text.Trim()))
            {
                password = new Encryption().SHA512_Hashing(password);
                BusAccount busAccount = new BusAccount();

                // gọi hàm addAccount từ busAccount để lưu dữ liệu vào database
                int result = busAccount.changePassword(username, password);
                if (result == 1)
                {
                    MessageBox.Show(string.Format("Cập nhật mật khẩu mới thành công cho thành viên {0} {1}",
                        SecurityObject.accInfo.FirstName, SecurityObject.accInfo.LastName));
                    this.txtPasswordNew.Clear();
                    this.txtPasswordConfirm.Clear();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại");
                }

            }
            else
            {
                this.ErrorMessage.Show("Mật khẩu mới không khớp !! Vui lòng thử lại !!", this.txtPasswordNew, 0, -70, 5000);
                this.txtPasswordNew.Clear();
                this.txtPasswordConfirm.Clear();
                this.txtPasswordNew.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
