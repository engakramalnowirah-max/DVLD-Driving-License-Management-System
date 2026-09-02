namespace Project_DVLD_PresentaionLayer.Detain_License
{
    partial class frmLManageDetainedDrivingLicense
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
            this.components = new System.ComponentModel.Container();
            this.dgvDetainedDrivingLicense = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPersonInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseDetainedLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblNumberOfRows = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtValueWithFilter = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbIsActive = new System.Windows.Forms.ComboBox();
            this.pbRelease = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbDetain = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedDrivingLicense)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRelease)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDetain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDetainedDrivingLicense
            // 
            this.dgvDetainedDrivingLicense.AllowUserToAddRows = false;
            this.dgvDetainedDrivingLicense.AllowUserToDeleteRows = false;
            this.dgvDetainedDrivingLicense.AllowUserToOrderColumns = true;
            this.dgvDetainedDrivingLicense.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetainedDrivingLicense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetainedDrivingLicense.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvDetainedDrivingLicense.Location = new System.Drawing.Point(10, 293);
            this.dgvDetainedDrivingLicense.Name = "dgvDetainedDrivingLicense";
            this.dgvDetainedDrivingLicense.ReadOnly = true;
            this.dgvDetainedDrivingLicense.RowHeadersWidth = 51;
            this.dgvDetainedDrivingLicense.RowTemplate.Height = 26;
            this.dgvDetainedDrivingLicense.Size = new System.Drawing.Size(1319, 403);
            this.dgvDetainedDrivingLicense.TabIndex = 32;
            this.dgvDetainedDrivingLicense.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDetainedDrivingLicense_CellMouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonInfoToolStripMenuItem,
            this.showLicenseInfoToolStripMenuItem,
            this.showPersonLicenseHistoryToolStripMenuItem,
            this.releaseDetainedLicenseToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(269, 108);
            // 
            // showPersonInfoToolStripMenuItem
            // 
            this.showPersonInfoToolStripMenuItem.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.PersonDetails_32;
            this.showPersonInfoToolStripMenuItem.Name = "showPersonInfoToolStripMenuItem";
            this.showPersonInfoToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            this.showPersonInfoToolStripMenuItem.Text = "Show Person Details";
            this.showPersonInfoToolStripMenuItem.Click += new System.EventHandler(this.showPersonInfoToolStripMenuItem_Click);
            // 
            // showLicenseInfoToolStripMenuItem
            // 
            this.showLicenseInfoToolStripMenuItem.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.License_View_321;
            this.showLicenseInfoToolStripMenuItem.Name = "showLicenseInfoToolStripMenuItem";
            this.showLicenseInfoToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            this.showLicenseInfoToolStripMenuItem.Text = "Show License Details";
            this.showLicenseInfoToolStripMenuItem.Click += new System.EventHandler(this.showLicenseInfoToolStripMenuItem_Click);
            // 
            // showPersonLicenseHistoryToolStripMenuItem
            // 
            this.showPersonLicenseHistoryToolStripMenuItem.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.PersonLicenseHistory_321;
            this.showPersonLicenseHistoryToolStripMenuItem.Name = "showPersonLicenseHistoryToolStripMenuItem";
            this.showPersonLicenseHistoryToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            this.showPersonLicenseHistoryToolStripMenuItem.Text = "Show Person License History";
            this.showPersonLicenseHistoryToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicenseHistoryToolStripMenuItem_Click);
            // 
            // releaseDetainedLicenseToolStripMenuItem
            // 
            this.releaseDetainedLicenseToolStripMenuItem.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.Release_Detained_License_32;
            this.releaseDetainedLicenseToolStripMenuItem.Name = "releaseDetainedLicenseToolStripMenuItem";
            this.releaseDetainedLicenseToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            this.releaseDetainedLicenseToolStripMenuItem.Text = "Release Detained License";
            this.releaseDetainedLicenseToolStripMenuItem.Click += new System.EventHandler(this.releaseDetainedLicenseToolStripMenuItem_Click);
            // 
            // lblNumberOfRows
            // 
            this.lblNumberOfRows.AutoSize = true;
            this.lblNumberOfRows.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRows.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblNumberOfRows.Location = new System.Drawing.Point(120, 708);
            this.lblNumberOfRows.Name = "lblNumberOfRows";
            this.lblNumberOfRows.Size = new System.Drawing.Size(31, 23);
            this.lblNumberOfRows.TabIndex = 31;
            this.lblNumberOfRows.Text = "???";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(12, 708);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 23);
            this.label4.TabIndex = 30;
            this.label4.Text = "# Records:";
            // 
            // txtValueWithFilter
            // 
            this.txtValueWithFilter.Location = new System.Drawing.Point(331, 258);
            this.txtValueWithFilter.Name = "txtValueWithFilter";
            this.txtValueWithFilter.Size = new System.Drawing.Size(202, 24);
            this.txtValueWithFilter.TabIndex = 29;
            this.txtValueWithFilter.Text = "                       ";
            this.txtValueWithFilter.TextChanged += new System.EventHandler(this.txtValueWithFilter_TextChanged);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Detaind ID",
            "Is Released",
            "National No",
            "Full Name",
            "Release Application ID"});
            this.cbFilterBy.Location = new System.Drawing.Point(114, 258);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(202, 24);
            this.cbFilterBy.TabIndex = 28;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(10, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 28);
            this.label3.TabIndex = 27;
            this.label3.Text = "Filter By :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(507, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(325, 41);
            this.label1.TabIndex = 25;
            this.label1.Text = "List Detained Licenses";
            // 
            // cbIsActive
            // 
            this.cbIsActive.FormattingEnabled = true;
            this.cbIsActive.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbIsActive.Location = new System.Drawing.Point(331, 256);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(121, 24);
            this.cbIsActive.TabIndex = 91;
            this.cbIsActive.SelectedIndexChanged += new System.EventHandler(this.cbIsActive_SelectedIndexChanged);
            // 
            // pbRelease
            // 
            this.pbRelease.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbRelease.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.Release_Detained_License_64;
            this.pbRelease.Location = new System.Drawing.Point(1099, 217);
            this.pbRelease.Name = "pbRelease";
            this.pbRelease.Size = new System.Drawing.Size(109, 69);
            this.pbRelease.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbRelease.TabIndex = 90;
            this.pbRelease.TabStop = false;
            this.pbRelease.Click += new System.EventHandler(this.pbRelease_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1156, 701);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(173, 43);
            this.btnClose.TabIndex = 89;
            this.btnClose.Text = "close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbDetain
            // 
            this.pbDetain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbDetain.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.Detain_64;
            this.pbDetain.Location = new System.Drawing.Point(1220, 217);
            this.pbDetain.Name = "pbDetain";
            this.pbDetain.Size = new System.Drawing.Size(109, 69);
            this.pbDetain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbDetain.TabIndex = 33;
            this.pbDetain.TabStop = false;
            this.pbDetain.Click += new System.EventHandler(this.pbDetain_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.Detain_512;
            this.pictureBox1.Location = new System.Drawing.Point(530, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(277, 218);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // frmLManageDetainedDrivingLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1341, 750);
            this.Controls.Add(this.cbIsActive);
            this.Controls.Add(this.pbRelease);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbDetain);
            this.Controls.Add(this.dgvDetainedDrivingLicense);
            this.Controls.Add(this.lblNumberOfRows);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtValueWithFilter);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLManageDetainedDrivingLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLManageDetainedDrivingLicense";
            this.Load += new System.EventHandler(this.frmLManageDetainedDrivingLicense_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedDrivingLicense)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbRelease)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDetain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbDetain;
        private System.Windows.Forms.DataGridView dgvDetainedDrivingLicense;
        private System.Windows.Forms.Label lblNumberOfRows;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtValueWithFilter;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbRelease;
        private System.Windows.Forms.ComboBox cbIsActive;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showPersonInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseDetainedLicenseToolStripMenuItem;
    }
}