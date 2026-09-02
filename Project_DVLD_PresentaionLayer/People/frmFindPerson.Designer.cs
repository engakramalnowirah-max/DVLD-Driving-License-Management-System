namespace Project_DVLD_PresentaionLayer.People
{
    partial class frmFindPerson
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
            this.strlPersonCardWithFilter1 = new Project_DVLD_PresentaionLayer.People.strlPersonCardWithFilter();
            this.SuspendLayout();
            // 
            // lblTitil
            // 
            this.lblTitil.AutoSize = true;
            this.lblTitil.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitil.ForeColor = System.Drawing.Color.Red;
            this.lblTitil.Location = new System.Drawing.Point(469, 21);
            this.lblTitil.Name = "lblTitil";
            this.lblTitil.Size = new System.Drawing.Size(182, 41);
            this.lblTitil.TabIndex = 3;
            this.lblTitil.Text = "Find Person";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1024, 568);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(141, 41);
            this.btnClose.TabIndex = 30;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // strlPersonCardWithFilter1
            // 
            this.strlPersonCardWithFilter1.AddNowPerson = true;
            this.strlPersonCardWithFilter1.BackColor = System.Drawing.Color.White;
            this.strlPersonCardWithFilter1.FillterEnabled = true;
            this.strlPersonCardWithFilter1.Location = new System.Drawing.Point(7, 65);
            this.strlPersonCardWithFilter1.Name = "strlPersonCardWithFilter1";
            this.strlPersonCardWithFilter1.Size = new System.Drawing.Size(1174, 497);
            this.strlPersonCardWithFilter1.TabIndex = 4;
            // 
            // frmFindPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1202, 621);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.strlPersonCardWithFilter1);
            this.Controls.Add(this.lblTitil);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmFindPerson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Person";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitil;
        private strlPersonCardWithFilter strlPersonCardWithFilter1;
        private System.Windows.Forms.Button btnClose;
    }
}