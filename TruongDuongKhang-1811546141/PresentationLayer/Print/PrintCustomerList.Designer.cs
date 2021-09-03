
namespace TruongDuongKhang_1811546141.PresentationLayer
{
    partial class PrintCustomerList
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
            this.previewArea.Size = new System.Drawing.Size(800, 450);
            this.previewArea.TabIndex = 2;
            this.previewArea.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.previewArea.Load += new System.EventHandler(this.previewArea_Load);
            // 
            // PrintCustomerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.previewArea);
            this.Name = "PrintCustomerList";
            this.Text = "PrintCustomerList";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer previewArea;
    }
}