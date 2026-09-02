namespace Project_DVLD_PresentaionLayer.Applications.Local_Driving_License
{
    partial class frmShowLocalDrivingLicenseApplicationInfo
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
            this.lblTitil = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.strlDetailsLoclDrivingLicenseApplication1 = new Project_DVLD_PresentaionLayer.Applications.Local_Driving_License.strlDetailsLoclDrivingLicenseApplication();
            this.SuspendLayout();
            // 
            // lblTitil
            // 
            this.lblTitil.AutoSize = true;
            this.lblTitil.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitil.ForeColor = System.Drawing.Color.Red;
            this.lblTitil.Location = new System.Drawing.Point(261, 39);
            this.lblTitil.Name = "lblTitil";
            this.lblTitil.Size = new System.Drawing.Size(468, 41);
            this.lblTitil.TabIndex = 3;
            this.lblTitil.Text = "Locl Driving License Application";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(862, 534);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(155, 47);
            this.btnClose.TabIndex = 31;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // strlDetailsLoclDrivingLicenseApplication1
            // 
            this.strlDetailsLoclDrivingLicenseApplication1.BackColor = System.Drawing.Color.White;
            this.strlDetailsLoclDrivingLicenseApplication1.Location = new System.Drawing.Point(11, 110);
            this.strlDetailsLoclDrivingLicenseApplication1.Name = "strlDetailsLoclDrivingLicenseApplication1";
            this.strlDetailsLoclDrivingLicenseApplication1.Size = new System.Drawing.Size(1014, 418);
            this.strlDetailsLoclDrivingLicenseApplication1.TabIndex = 0;
            // 
            // frmShowLocalDrivingLicenseApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1033, 593);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitil);
            this.Controls.Add(this.strlDetailsLoclDrivingLicenseApplication1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowLocalDrivingLicenseApplicationInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show Local Driving License Application Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private strlDetailsLoclDrivingLicenseApplication strlDetailsLoclDrivingLicenseApplication1;
        private System.Windows.Forms.Label lblTitil;
        private System.Windows.Forms.Button btnClose;
    }
}