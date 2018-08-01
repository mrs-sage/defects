namespace x3OnSiteUsers
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtX3Number = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCaseSSO = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtActualUser = new System.Windows.Forms.TextBox();
            this.txtActualDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CrmSsoNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblOnSiteNumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMachine = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.checkFiscalYear = new System.Windows.Forms.CheckBox();
            this.checkCurrentYear = new System.Windows.Forms.CheckBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.SystemColors.Control;
            this.btnNew.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnNew.Location = new System.Drawing.Point(343, 6);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(96, 23);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "Novo";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSave.Enabled = false;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(229, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Gravar";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.BackColor = System.Drawing.Color.White;
            this.pictureBox4.Location = new System.Drawing.Point(229, 35);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(432, 327);
            this.pictureBox4.TabIndex = 78;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(-5, 164);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(214, 198);
            this.pictureBox1.TabIndex = 81;
            this.pictureBox1.TabStop = false;
            // 
            // txtX3Number
            // 
            this.txtX3Number.Location = new System.Drawing.Point(109, 8);
            this.txtX3Number.Name = "txtX3Number";
            this.txtX3Number.Size = new System.Drawing.Size(100, 20);
            this.txtX3Number.TabIndex = 1;
            this.txtX3Number.TextChanged += new System.EventHandler(this.txtX3Number_TextChanged);
            this.txtX3Number.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtX3Number_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 83;
            this.label1.Text = "Indique o nº X3";
            // 
            // txtCaseSSO
            // 
            this.txtCaseSSO.Enabled = false;
            this.txtCaseSSO.Location = new System.Drawing.Point(36, 118);
            this.txtCaseSSO.Name = "txtCaseSSO";
            this.txtCaseSSO.Size = new System.Drawing.Size(140, 20);
            this.txtCaseSSO.TabIndex = 2;
            this.txtCaseSSO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCaseSSO_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 85;
            this.label2.Text = "Case nº / SSO nº";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(11, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 87;
            this.label3.Text = "Data atual:";
            // 
            // txtActualUser
            // 
            this.txtActualUser.Enabled = false;
            this.txtActualUser.Location = new System.Drawing.Point(36, 254);
            this.txtActualUser.Name = "txtActualUser";
            this.txtActualUser.Size = new System.Drawing.Size(140, 20);
            this.txtActualUser.TabIndex = 88;
            // 
            // txtActualDate
            // 
            this.txtActualDate.Enabled = false;
            this.txtActualDate.Location = new System.Drawing.Point(36, 200);
            this.txtActualDate.Name = "txtActualDate";
            this.txtActualDate.Size = new System.Drawing.Size(140, 20);
            this.txtActualDate.TabIndex = 90;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(11, 233);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 89;
            this.label4.Text = "User:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CrmSsoNumber,
            this.User,
            this.Date});
            this.dataGridView1.Location = new System.Drawing.Point(241, 67);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(410, 283);
            this.dataGridView1.TabIndex = 91;
            // 
            // CrmSsoNumber
            // 
            this.CrmSsoNumber.HeaderText = "CRM / SSO";
            this.CrmSsoNumber.Name = "CrmSsoNumber";
            this.CrmSsoNumber.ReadOnly = true;
            this.CrmSsoNumber.Width = 150;
            // 
            // User
            // 
            this.User.HeaderText = "Utilizador";
            this.User.Name = "User";
            this.User.ReadOnly = true;
            this.User.Width = 150;
            // 
            // Date
            // 
            this.Date.HeaderText = "Data";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // lblOnSiteNumber
            // 
            this.lblOnSiteNumber.AutoSize = true;
            this.lblOnSiteNumber.BackColor = System.Drawing.Color.White;
            this.lblOnSiteNumber.Location = new System.Drawing.Point(238, 45);
            this.lblOnSiteNumber.Name = "lblOnSiteNumber";
            this.lblOnSiteNumber.Size = new System.Drawing.Size(161, 13);
            this.lblOnSiteNumber.TabIndex = 92;
            this.lblOnSiteNumber.Text = "Nº de assistências já efetuadas: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(11, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 94;
            this.label5.Text = "Machine:";
            // 
            // txtMachine
            // 
            this.txtMachine.Enabled = false;
            this.txtMachine.Location = new System.Drawing.Point(36, 316);
            this.txtMachine.Name = "txtMachine";
            this.txtMachine.Size = new System.Drawing.Size(140, 20);
            this.txtMachine.TabIndex = 93;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Location = new System.Drawing.Point(-5, 35);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(214, 34);
            this.pictureBox2.TabIndex = 96;
            this.pictureBox2.TabStop = false;
            // 
            // checkFiscalYear
            // 
            this.checkFiscalYear.AutoSize = true;
            this.checkFiscalYear.BackColor = System.Drawing.Color.White;
            this.checkFiscalYear.Checked = true;
            this.checkFiscalYear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkFiscalYear.Location = new System.Drawing.Point(13, 45);
            this.checkFiscalYear.Name = "checkFiscalYear";
            this.checkFiscalYear.Size = new System.Drawing.Size(78, 17);
            this.checkFiscalYear.TabIndex = 97;
            this.checkFiscalYear.Text = "Fiscal Year";
            this.checkFiscalYear.UseVisualStyleBackColor = false;
            this.checkFiscalYear.CheckedChanged += new System.EventHandler(this.checkFiscalYear_CheckedChanged);
            // 
            // checkCurrentYear
            // 
            this.checkCurrentYear.AutoSize = true;
            this.checkCurrentYear.BackColor = System.Drawing.Color.White;
            this.checkCurrentYear.Location = new System.Drawing.Point(109, 45);
            this.checkCurrentYear.Name = "checkCurrentYear";
            this.checkCurrentYear.Size = new System.Drawing.Size(85, 17);
            this.checkCurrentYear.TabIndex = 98;
            this.checkCurrentYear.Text = "Current Year";
            this.checkCurrentYear.UseVisualStyleBackColor = false;
            this.checkCurrentYear.CheckedChanged += new System.EventHandler(this.checkCurrentYear_CheckedChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.Location = new System.Drawing.Point(-5, 82);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(214, 70);
            this.pictureBox3.TabIndex = 99;
            this.pictureBox3.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(658, 356);
            this.Controls.Add(this.txtCaseSSO);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.checkCurrentYear);
            this.Controls.Add(this.checkFiscalYear);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMachine);
            this.Controls.Add(this.lblOnSiteNumber);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtActualDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtActualUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtX3Number);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pictureBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "X3 OnSite Control";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtX3Number;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCaseSSO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtActualUser;
        private System.Windows.Forms.TextBox txtActualDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CrmSsoNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.Label lblOnSiteNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMachine;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox checkFiscalYear;
        private System.Windows.Forms.CheckBox checkCurrentYear;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}

