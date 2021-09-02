using System;
using System.Windows.Forms;
using TruongDuongKhang_1811546141.BussinessLayer.Entity;
using TruongDuongKhang_1811546141.BussinessLayer.Workflow;
using TruongDuongKhang_1811546141.Lib;

namespace TruongDuongKhang_1811546141
{
    public partial class Login : Form
    {
        public Login()
        {

            InitializeComponent();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            this.btnLogin.Enabled = enableSave();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            this.btnLogin.Enabled = enableSave();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(this.txtPassword.Text.Trim().Length > 0)
                {
                    btnLogin_Click(this.btnLogin, e);
                } else
                {
                    this.txtPassword.Focus();
                }
            } else if(e.KeyCode == Keys.Escape)
            {
                this.lblExit_Click(this.lblExit, e);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtUsername.Text.Trim().Length > 0)
                {
                    btnLogin_Click(this.btnLogin, e);
                }
                else
                {
                    this.txtUsername.Focus();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.lblExit_Click(this.lblExit, e);
            }
        }

        // khi có dữ liệu về tên đăng nhập và mật khẩu thì có thể đăng nhập
        private bool enableSave()
        {
            return (
                //thông tin đăng nhập
                this.txtUsername.Text.Trim().Length > 0 &&
                this.txtPassword.Text.Trim().Length > 0 
                );
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // kiểm tra thông tin người dùng sau đăng nhập và lưu lại nếu thành công
            string username = this.txtUsername.Text.Trim();
            string password = this.txtPassword.Text.Trim();

            if(username.Length > 0 && password.Length > 0)
            {
                // mã hóa mật khẩu khi người dùng nhập vào
                password = new Encryption().SHA512_Hashing(password);

                // tìm thông tin tài khoản người dùng nhập trong database
                AccountEntity accountEntity = new BusAccount().getInfo(username);

                //so sánh xem tên đăng nhập và mật khẩu được nhập có trùng với database không
                if (accountEntity.Username.Equals(username) && accountEntity.Password.Equals(password))
                {
                    SecurityObject.accInfo = accountEntity;
                    this.Dispose();
                } else
                {
                    MessageBox.Show("Sai thông tin tài khoản hoặc mật khẩu. Vui lòng thử lại !!"); 
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
        }


        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
