namespace Project_DVLD_PresentaionLayer.Licenses.International_Licnese
{
    partial class frmShowInternationalLicense
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
            this.strlInternationalLicenseInfo1 = new Project_DVLD_PresentaionLayer.Licenses.International_Licnese.Controls.strlInternationalLicenseInfo();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // strlInternationalLicenseInfo1
            // 
            this.strlInternationalLicenseInfo1.Location = new System.Drawing.Point(10, 135);
            this.strlInternationalLicenseInfo1.Name = "strlInternationalLicenseInfo1";
            this.strlInternationalLicenseInfo1.Size = new System.Drawing.Size(1045, 313);
            this.strlInternationalLicenseInfo1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(879, 455);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(173, 43);
            this.btnClose.TabIndex = 96;
            this.btnClose.Text = "close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(321, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(377, 41);
            this.label1.TabIndex = 97;
            this.label1.Text = "International License Info";
            // 
            // frmShowInternationalLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1068, 508);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.strlInternationalLicenseInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowInternationalLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "International License Info";
            this.Load += new System.EventHandler(this.frmShowInternationalLicense_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.strlInternationalLicenseInfo strlInternationalLicenseInfo1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
    }
}