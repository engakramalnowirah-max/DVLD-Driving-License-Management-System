namespace Project_DVLD_PresentaionLayer.People
{
    partial class strlPersonCardWithFilter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnAddNewPerson = new System.Windows.Forms.Button();
            this.btnFindLoclDrivingLicenseApplicationBythisID = new System.Windows.Forms.Button();
            this.txtValueToFilter = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.strlDetails1 = new Project_DVLD_PresentaionLayer.ManagePeople.strlDetails();
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnAddNewPerson);
            this.gbFilter.Controls.Add(this.btnFindLoclDrivingLicenseApplicationBythisID);
            this.gbFilter.Controls.Add(this.txtValueToFilter);
            this.gbFilter.Controls.Add(this.cbFilterBy);
            this.gbFilter.Controls.Add(this.label1);
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(15, 7);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(1143, 100);
            this.gbFilter.TabIndex = 1;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.AddPerson_32;
            this.btnAddNewPerson.Location = new System.Drawing.Point(659, 29);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.Size = new System.Drawing.Size(92, 51);
            this.btnAddNewPerson.TabIndex = 4;
            this.btnAddNewPerson.UseVisualStyleBackColor = true;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnAddNewPerson_Click);
            // 
            // btnFindLoclDrivingLicenseApplicationBythisID
            // 
            this.btnFindLoclDrivingLicenseApplicationBythisID.Image = global::Project_DVLD_PresentaionLayer.Properties.Resources.SearchPerson;
            this.btnFindLoclDrivingLicenseApplicationBythisID.Location = new System.Drawing.Point(552, 29);
            this.btnFindLoclDrivingLicenseApplicationBythisID.Name = "btnFindLoclDrivingLicenseApplicationBythisID";
            this.btnFindLoclDrivingLicenseApplicationBythisID.Size = new System.Drawing.Size(92, 51);
            this.btnFindLoclDrivingLicenseApplicationBythisID.TabIndex = 3;
            this.btnFindLoclDrivingLicenseApplicationBythisID.UseVisualStyleBackColor = true;
            this.btnFindLoclDrivingLicenseApplicationBythisID.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtValueToFilter
            // 
            this.txtValueToFilter.Location = new System.Drawing.Point(339, 38);
            this.txtValueToFilter.Name = "txtValueToFilter";
            this.txtValueToFilter.Size = new System.Drawing.Size(189, 30);
            this.txtValueToFilter.TabIndex = 2;
            this.txtValueToFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValueToFilter_KeyPress);
            this.txtValueToFilter.Validating += new System.ComponentModel.CancelEventHandler(this.txtValueToFilter_Validating);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "Person ID",
            "National No"});
            this.cbFilterBy.Location = new System.Drawing.Point(131, 38);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(184, 31);
            this.cbFilterBy.TabIndex = 1;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter By:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // strlDetails1
            // 
            this.strlDetails1.BackColor = System.Drawing.Color.White;
            this.strlDetails1.Location = new System.Drawing.Point(15, 112);
            this.strlDetails1.Name = "strlDetails1";
            this.strlDetails1.Size = new System.Drawing.Size(1143, 322);
            this.strlDetails1.TabIndex = 0;
            // 
            // strlPersonCardWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.strlDetails1);
            this.Name = "strlPersonCardWithFilter";
            this.Size = new System.Drawing.Size(1174, 438);
            this.Load += new System.EventHandler(this.strlPersonCardWithFilter_Load);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ManagePeople.strlDetails strlDetails1;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Button btnAddNewPerson;
        private System.Windows.Forms.Button btnFindLoclDrivingLicenseApplicationBythisID;
        private System.Windows.Forms.TextBox txtValueToFilter;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
