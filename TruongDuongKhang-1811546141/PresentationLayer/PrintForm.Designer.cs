
namespace TruongDuongKhang_1811546141.PresentationLayer
{
    partial class PrintForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.previewArea = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // previewArea
            // 
            this.previewArea.ActiveViewIndex = -1;
            this.previewArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewArea.Cursor = System.Windows.Forms.Cursors.Default;
            this.previewArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewArea.Location = new System.Drawing.Point(0, 0);
            this.previewArea.Name = "previewArea";
            this.previewArea.Size = new System.Drawing.Size(1200, 550);
            this.previewArea.TabIndex = 0;
            this.previewArea.Load += new System.EventHandler(this.previewArea_Load);
            // 
            // Print
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 550);
            this.Controls.Add(this.previewArea);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Print";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer previewArea;
    }
}