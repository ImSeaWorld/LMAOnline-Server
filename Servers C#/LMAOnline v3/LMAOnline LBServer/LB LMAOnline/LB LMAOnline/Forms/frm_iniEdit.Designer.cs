namespace LB_LMAOnline.Forms
{
    partial class frm_iniEdit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cb_parseLogs = new System.Windows.Forms.CheckBox();
            this.cb_logCon = new System.Windows.Forms.CheckBox();
            this.cb_logCrash = new System.Windows.Forms.CheckBox();
            this.cb_anonUsers = new System.Windows.Forms.CheckBox();
            this.cb_startup = new System.Windows.Forms.CheckBox();
            this.cb_xexChecks = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.nud_port = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_serverAddr = new System.Windows.Forms.TextBox();
            this.tb_dbName = new System.Windows.Forms.TextBox();
            this.tb_dbUser = new System.Windows.Forms.TextBox();
            this.tb_dbPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_new = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_port)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 108);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bools";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cb_anonUsers);
            this.groupBox2.Controls.Add(this.cb_startup);
            this.groupBox2.Controls.Add(this.cb_xexChecks);
            this.groupBox2.Location = new System.Drawing.Point(132, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(120, 88);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cb_logCrash);
            this.groupBox3.Controls.Add(this.cb_logCon);
            this.groupBox3.Controls.Add(this.cb_parseLogs);
            this.groupBox3.Location = new System.Drawing.Point(6, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(120, 88);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Logging";
            // 
            // cb_parseLogs
            // 
            this.cb_parseLogs.AutoSize = true;
            this.cb_parseLogs.Location = new System.Drawing.Point(6, 19);
            this.cb_parseLogs.Name = "cb_parseLogs";
            this.cb_parseLogs.Size = new System.Drawing.Size(79, 17);
            this.cb_parseLogs.TabIndex = 1;
            this.cb_parseLogs.Text = "Parse Logs";
            this.cb_parseLogs.UseVisualStyleBackColor = true;
            // 
            // cb_logCon
            // 
            this.cb_logCon.AutoSize = true;
            this.cb_logCon.Location = new System.Drawing.Point(6, 42);
            this.cb_logCon.Name = "cb_logCon";
            this.cb_logCon.Size = new System.Drawing.Size(106, 17);
            this.cb_logCon.TabIndex = 2;
            this.cb_logCon.Text = "Log Connections";
            this.cb_logCon.UseVisualStyleBackColor = true;
            // 
            // cb_logCrash
            // 
            this.cb_logCrash.AutoSize = true;
            this.cb_logCrash.Location = new System.Drawing.Point(6, 65);
            this.cb_logCrash.Name = "cb_logCrash";
            this.cb_logCrash.Size = new System.Drawing.Size(88, 17);
            this.cb_logCrash.TabIndex = 3;
            this.cb_logCrash.Text = "Log Crashing";
            this.cb_logCrash.UseVisualStyleBackColor = true;
            // 
            // cb_anonUsers
            // 
            this.cb_anonUsers.AutoSize = true;
            this.cb_anonUsers.Location = new System.Drawing.Point(6, 65);
            this.cb_anonUsers.Name = "cb_anonUsers";
            this.cb_anonUsers.Size = new System.Drawing.Size(81, 17);
            this.cb_anonUsers.TabIndex = 6;
            this.cb_anonUsers.Text = "Anon Users";
            this.cb_anonUsers.UseVisualStyleBackColor = true;
            // 
            // cb_startup
            // 
            this.cb_startup.AutoSize = true;
            this.cb_startup.Location = new System.Drawing.Point(6, 42);
            this.cb_startup.Name = "cb_startup";
            this.cb_startup.Size = new System.Drawing.Size(60, 17);
            this.cb_startup.TabIndex = 5;
            this.cb_startup.Text = "Startup";
            this.cb_startup.UseVisualStyleBackColor = true;
            // 
            // cb_xexChecks
            // 
            this.cb_xexChecks.AutoSize = true;
            this.cb_xexChecks.Location = new System.Drawing.Point(6, 19);
            this.cb_xexChecks.Name = "cb_xexChecks";
            this.cb_xexChecks.Size = new System.Drawing.Size(101, 17);
            this.cb_xexChecks.TabIndex = 4;
            this.cb_xexChecks.Text = "OR XeXChecks";
            this.cb_xexChecks.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.tb_dbPass);
            this.groupBox4.Controls.Add(this.tb_dbUser);
            this.groupBox4.Controls.Add(this.tb_dbName);
            this.groupBox4.Controls.Add(this.tb_serverAddr);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.nud_port);
            this.groupBox4.Location = new System.Drawing.Point(34, 126);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(216, 153);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Other";
            // 
            // nud_port
            // 
            this.nud_port.Location = new System.Drawing.Point(77, 19);
            this.nud_port.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nud_port.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_port.Name = "nud_port";
            this.nud_port.Size = new System.Drawing.Size(125, 20);
            this.nud_port.TabIndex = 0;
            this.nud_port.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port:";
            // 
            // tb_serverAddr
            // 
            this.tb_serverAddr.Location = new System.Drawing.Point(77, 45);
            this.tb_serverAddr.Name = "tb_serverAddr";
            this.tb_serverAddr.Size = new System.Drawing.Size(125, 20);
            this.tb_serverAddr.TabIndex = 2;
            // 
            // tb_dbName
            // 
            this.tb_dbName.Location = new System.Drawing.Point(77, 71);
            this.tb_dbName.Name = "tb_dbName";
            this.tb_dbName.Size = new System.Drawing.Size(125, 20);
            this.tb_dbName.TabIndex = 3;
            // 
            // tb_dbUser
            // 
            this.tb_dbUser.Location = new System.Drawing.Point(77, 97);
            this.tb_dbUser.Name = "tb_dbUser";
            this.tb_dbUser.Size = new System.Drawing.Size(125, 20);
            this.tb_dbUser.TabIndex = 4;
            // 
            // tb_dbPass
            // 
            this.tb_dbPass.Location = new System.Drawing.Point(77, 123);
            this.tb_dbPass.Name = "tb_dbPass";
            this.tb_dbPass.Size = new System.Drawing.Size(125, 20);
            this.tb_dbPass.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Server Addr:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "DB Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "DB User:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "DB Pass:";
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(23, 285);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 23);
            this.btn_load.TabIndex = 2;
            this.btn_load.Text = "Load";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(104, 285);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 3;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_new
            // 
            this.btn_new.Enabled = false;
            this.btn_new.Location = new System.Drawing.Point(185, 285);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(75, 23);
            this.btn_new.TabIndex = 4;
            this.btn_new.Text = "New";
            this.btn_new.UseVisualStyleBackColor = true;
            // 
            // frm_iniEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 316);
            this.Controls.Add(this.btn_new);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_iniEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit INI";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_port)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cb_logCrash;
        private System.Windows.Forms.CheckBox cb_logCon;
        private System.Windows.Forms.CheckBox cb_parseLogs;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cb_anonUsers;
        private System.Windows.Forms.CheckBox cb_startup;
        private System.Windows.Forms.CheckBox cb_xexChecks;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_dbPass;
        private System.Windows.Forms.TextBox tb_dbUser;
        private System.Windows.Forms.TextBox tb_dbName;
        private System.Windows.Forms.TextBox tb_serverAddr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nud_port;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_new;
    }
}