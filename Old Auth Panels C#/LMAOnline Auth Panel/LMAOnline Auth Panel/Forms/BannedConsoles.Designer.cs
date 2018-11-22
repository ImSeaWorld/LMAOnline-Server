namespace LMAOnline.Forms
{
    partial class BannedConsoles
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_haskv = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rtb_banreason = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_ip = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_cpukey = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lv_bannedusers = new System.Windows.Forms.ListView();
            this.ch_cpukey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_ip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_kv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cms_bannedusers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stealKVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeBannedUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmi_copyIP = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.cms_bannedusers.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lbl_haskv);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.rtb_banreason);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lbl_ip);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lbl_name);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbl_cpukey);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(61, 387);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 121);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Information";
            // 
            // lbl_haskv
            // 
            this.lbl_haskv.AutoSize = true;
            this.lbl_haskv.Location = new System.Drawing.Point(68, 95);
            this.lbl_haskv.Name = "lbl_haskv";
            this.lbl_haskv.Size = new System.Drawing.Size(99, 13);
            this.lbl_haskv.TabIndex = 9;
            this.lbl_haskv.Text = "*No User Selected*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Has KV:";
            // 
            // rtb_banreason
            // 
            this.rtb_banreason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_banreason.Location = new System.Drawing.Point(226, 46);
            this.rtb_banreason.Name = "rtb_banreason";
            this.rtb_banreason.ReadOnly = true;
            this.rtb_banreason.Size = new System.Drawing.Size(163, 69);
            this.rtb_banreason.TabIndex = 7;
            this.rtb_banreason.Text = "*No User Selected*";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(284, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Reason";
            // 
            // lbl_ip
            // 
            this.lbl_ip.AutoSize = true;
            this.lbl_ip.Location = new System.Drawing.Point(68, 72);
            this.lbl_ip.Name = "lbl_ip";
            this.lbl_ip.Size = new System.Drawing.Size(99, 13);
            this.lbl_ip.TabIndex = 5;
            this.lbl_ip.Text = "*No User Selected*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "IP:";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(68, 49);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(99, 13);
            this.lbl_name.TabIndex = 3;
            this.lbl_name.Text = "*No User Selected*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Name:";
            // 
            // lbl_cpukey
            // 
            this.lbl_cpukey.AutoSize = true;
            this.lbl_cpukey.Location = new System.Drawing.Point(68, 26);
            this.lbl_cpukey.Name = "lbl_cpukey";
            this.lbl_cpukey.Size = new System.Drawing.Size(99, 13);
            this.lbl_cpukey.TabIndex = 1;
            this.lbl_cpukey.Text = "*No User Selected*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CPUKey:";
            // 
            // lv_bannedusers
            // 
            this.lv_bannedusers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_bannedusers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_cpukey,
            this.ch_name,
            this.ch_ip,
            this.ch_kv});
            this.lv_bannedusers.FullRowSelect = true;
            this.lv_bannedusers.GridLines = true;
            this.lv_bannedusers.Location = new System.Drawing.Point(6, 19);
            this.lv_bannedusers.Name = "lv_bannedusers";
            this.lv_bannedusers.Size = new System.Drawing.Size(482, 343);
            this.lv_bannedusers.TabIndex = 6;
            this.lv_bannedusers.UseCompatibleStateImageBehavior = false;
            this.lv_bannedusers.View = System.Windows.Forms.View.Details;
            this.lv_bannedusers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lv_bannedusers_MouseDown);
            // 
            // ch_cpukey
            // 
            this.ch_cpukey.Text = "CPUKey";
            this.ch_cpukey.Width = 228;
            // 
            // ch_name
            // 
            this.ch_name.Text = "Name";
            this.ch_name.Width = 85;
            // 
            // ch_ip
            // 
            this.ch_ip.Text = "IP";
            this.ch_ip.Width = 85;
            // 
            // ch_kv
            // 
            this.ch_kv.Text = "Has KV";
            this.ch_kv.Width = 80;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lv_bannedusers);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(495, 369);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Banned Consoles";
            // 
            // cms_bannedusers
            // 
            this.cms_bannedusers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stealKVToolStripMenuItem,
            this.refreshUsersToolStripMenuItem,
            this.banUserToolStripMenuItem,
            this.removeBannedUserToolStripMenuItem,
            this.cmi_copyIP});
            this.cms_bannedusers.Name = "cms_bannedusers";
            this.cms_bannedusers.Size = new System.Drawing.Size(187, 114);
            // 
            // stealKVToolStripMenuItem
            // 
            this.stealKVToolStripMenuItem.Name = "stealKVToolStripMenuItem";
            this.stealKVToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.stealKVToolStripMenuItem.Text = "Steal KV";
            this.stealKVToolStripMenuItem.Click += new System.EventHandler(this.stealKVToolStripMenuItem_Click);
            // 
            // refreshUsersToolStripMenuItem
            // 
            this.refreshUsersToolStripMenuItem.Name = "refreshUsersToolStripMenuItem";
            this.refreshUsersToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.refreshUsersToolStripMenuItem.Text = "Refresh Users";
            this.refreshUsersToolStripMenuItem.Click += new System.EventHandler(this.refreshUsersToolStripMenuItem_Click);
            // 
            // banUserToolStripMenuItem
            // 
            this.banUserToolStripMenuItem.Name = "banUserToolStripMenuItem";
            this.banUserToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.banUserToolStripMenuItem.Text = "Ban User";
            // 
            // removeBannedUserToolStripMenuItem
            // 
            this.removeBannedUserToolStripMenuItem.Name = "removeBannedUserToolStripMenuItem";
            this.removeBannedUserToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.removeBannedUserToolStripMenuItem.Text = "Remove Banned User";
            this.removeBannedUserToolStripMenuItem.Click += new System.EventHandler(this.removeBannedUserToolStripMenuItem_Click);
            // 
            // cmi_copyIP
            // 
            this.cmi_copyIP.Name = "cmi_copyIP";
            this.cmi_copyIP.Size = new System.Drawing.Size(186, 22);
            this.cmi_copyIP.Text = "Copy IP Address";
            this.cmi_copyIP.Click += new System.EventHandler(this.cmi_copyIP_Click);
            // 
            // BannedConsoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 521);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BannedConsoles";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Banned Consoles";
            this.Load += new System.EventHandler(this.BannedConsoles_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.cms_bannedusers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_haskv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtb_banreason;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_ip;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_cpukey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lv_bannedusers;
        private System.Windows.Forms.ColumnHeader ch_cpukey;
        private System.Windows.Forms.ColumnHeader ch_name;
        private System.Windows.Forms.ColumnHeader ch_ip;
        private System.Windows.Forms.ColumnHeader ch_kv;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ContextMenuStrip cms_bannedusers;
        private System.Windows.Forms.ToolStripMenuItem stealKVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem banUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeBannedUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmi_copyIP;
    }
}