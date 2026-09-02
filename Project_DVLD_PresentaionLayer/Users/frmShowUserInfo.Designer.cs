namespace Project_DVLD_PresentaionLayer.Users
{
    partial class frmShowUserInfo
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
            this.strlUserCard1 = new Project_DVLD_PresentaionLayer.Users.Controls.strlUserCard();
            this.SuspendLayout();
            // 
            // lblTitil
            // 
            this.lblTitil.AutoSize = true;
            this.lblTitil.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitil.ForeColor = System.Drawing.Color.Red;
            this.lblTitil.Location = new System.Drawing.Point(511, 45);
            this.lblTitil.Name = "lblTitil";
            this.lblTitil.Size = new System.Drawing.Size(186, 41);
            this.lblTitil.TabIndex = 3;
            this.lblTitil.Text = "User Details";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1032, 593);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(141, 41);
            this.btnClose.TabIndex = 31;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // strlUserCard1
            // 
            this.strlUserCard1.BackColor = System.Drawing.Color.White;
            this.strlUserCard1.Location = new System.Drawing.Point(12, 121);
            this.strlUserCard1.Name = "strlUserCard1";
            this.strlUserCard1.Size = new System.Drawing.Size(1178, 471);
            this.strlUserCard1.TabIndex = 32;
            // 
            // frmShowUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1203, 650);
            this.Controls.Add(this.strlUserCard1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitil);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowUserInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Info";
            this.Load += new System.EventHandler(this.frmShowUserInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitil;
        private System.Windows.Forms.Button btnClose;
        private Controls.strlUserCard strlUserCard1;
    }
}