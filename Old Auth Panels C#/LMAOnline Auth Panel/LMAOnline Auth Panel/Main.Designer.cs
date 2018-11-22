namespace LMAOnline_Auth_Panel
{
    partial class Main
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
            System.Windows.Forms.ColumnHeader ch_cpukey;
            System.Windows.Forms.ColumnHeader ch_gamertag;
            System.Windows.Forms.ColumnHeader ch_enabled;
            System.Windows.Forms.ColumnHeader ch_name;
            System.Windows.Forms.ColumnHeader ch_email;
            System.Windows.Forms.ColumnHeader ch_titleid;
            System.Windows.Forms.ColumnHeader ch_ip;
            System.Windows.Forms.ColumnHeader ch_time;
            System.Windows.Forms.ColumnHeader ch_lastseen;
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_totalclients = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_bannedConsoles = new System.Windows.Forms.Button();
            this.btn_stealkv = new System.Windows.Forms.Button();
            this.btn_failedlogins = new System.Windows.Forms.Button();
            this.btn_enabledisable = new System.Windows.Forms.Button();
            this.btn_listusers = new System.Windows.Forms.Button();
            this.btn_adduser = new System.Windows.Forms.Button();
            this.cms_clients = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.msi_refreshusers = new System.Windows.Forms.ToolStripMenuItem();
            this.msi_userOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.msi_addUser = new System.Windows.Forms.ToolStripMenuItem();
            this.msi_editUser = new System.Windows.Forms.ToolStripMenuItem();
            this.msi_removeUser = new System.Windows.Forms.ToolStripMenuItem();
            this.msi_banUser = new System.Windows.Forms.ToolStripMenuItem();
            this.msi_beAnAsshole = new System.Windows.Forms.ToolStripMenuItem();
            this.msi_copyShit = new System.Windows.Forms.ToolStripMenuItem();
            this.msi_copyIP2 = new System.Windows.Forms.ToolStripMenuItem();
            this.msi_copyCPU2 = new System.Windows.Forms.ToolStripMenuItem();
            this.msi_copyGT2 = new System.Windows.Forms.ToolStripMenuItem();
            this.msi_copyTime2 = new System.Windows.Forms.ToolStripMenuItem();
            this.msi_copyLine2 = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_overlay = new System.Windows.Forms.Label();
            this.lv_clients = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            ch_cpukey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ch_gamertag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ch_enabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ch_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ch_email = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ch_titleid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ch_ip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ch_time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ch_lastseen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cms_clients.SuspendLayout();
            this.SuspendLayout();
            // 
            // ch_cpukey
            // 
            ch_cpukey.Text = "CPUKey";
            ch_cpukey.Width = 220;
            // 
            // ch_gamertag
            // 
            ch_gamertag.Text = "GamerTag";
            ch_gamertag.Width = 106;
            // 
            // ch_enabled
            // 
            ch_enabled.Text = "Enabled";
            ch_enabled.Width = 55;
            // 
            // ch_name
            // 
            ch_name.Text = "Name";
            ch_name.Width = 106;
            // 
            // ch_email
            // 
            ch_email.Text = "Adder";
            ch_email.Width = 169;
            // 
            // ch_titleid
            // 
            ch_titleid.Text = "TitleID";
            ch_titleid.Width = 68;
            // 
            // ch_ip
            // 
            ch_ip.Text = "IP";
            ch_ip.Width = 85;
            // 
            // ch_time
            // 
            ch_time.Text = "Time Left";
            ch_time.Width = 135;
            // 
            // ch_lastseen
            // 
            ch_lastseen.Text = "Last Seen";
            ch_lastseen.Width = 130;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lbl_totalclients);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(1086, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 60);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server Stats";
            // 
            // lbl_totalclients
            // 
            this.lbl_totalclients.AutoSize = true;
            this.lbl_totalclients.Location = new System.Drawing.Point(84, 29);
            this.lbl_totalclients.Name = "lbl_totalclients";
            this.lbl_totalclients.Size = new System.Drawing.Size(13, 13);
            this.lbl_totalclients.TabIndex = 1;
            this.lbl_totalclients.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Clients:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btn_bannedConsoles);
            this.groupBox1.Controls.Add(this.btn_stealkv);
            this.groupBox1.Controls.Add(this.btn_failedlogins);
            this.groupBox1.Controls.Add(this.btn_enabledisable);
            this.groupBox1.Controls.Add(this.btn_listusers);
            this.groupBox1.Controls.Add(this.btn_adduser);
            this.groupBox1.Location = new System.Drawing.Point(1086, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 196);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Silly Shit";
            // 
            // btn_bannedConsoles
            // 
            this.btn_bannedConsoles.Location = new System.Drawing.Point(6, 19);
            this.btn_bannedConsoles.Name = "btn_bannedConsoles";
            this.btn_bannedConsoles.Size = new System.Drawing.Size(188, 23);
            this.btn_bannedConsoles.TabIndex = 7;
            this.btn_bannedConsoles.Text = "Banned Consoles";
            this.btn_bannedConsoles.UseVisualStyleBackColor = true;
            this.btn_bannedConsoles.Click += new System.EventHandler(this.btn_bannedConsoles_Click);
            // 
            // btn_stealkv
            // 
            this.btn_stealkv.Location = new System.Drawing.Point(6, 164);
            this.btn_stealkv.Name = "btn_stealkv";
            this.btn_stealkv.Size = new System.Drawing.Size(188, 23);
            this.btn_stealkv.TabIndex = 5;
            this.btn_stealkv.Text = "Get KV";
            this.btn_stealkv.UseVisualStyleBackColor = true;
            this.btn_stealkv.Click += new System.EventHandler(this.btn_stealkv_Click);
            // 
            // btn_failedlogins
            // 
            this.btn_failedlogins.Location = new System.Drawing.Point(6, 48);
            this.btn_failedlogins.Name = "btn_failedlogins";
            this.btn_failedlogins.Size = new System.Drawing.Size(188, 23);
            this.btn_failedlogins.TabIndex = 6;
            this.btn_failedlogins.Text = "Failed Logons";
            this.btn_failedlogins.UseVisualStyleBackColor = true;
            this.btn_failedlogins.Click += new System.EventHandler(this.btn_failedlogins_Click);
            // 
            // btn_enabledisable
            // 
            this.btn_enabledisable.Location = new System.Drawing.Point(6, 135);
            this.btn_enabledisable.Name = "btn_enabledisable";
            this.btn_enabledisable.Size = new System.Drawing.Size(188, 23);
            this.btn_enabledisable.TabIndex = 3;
            this.btn_enabledisable.Text = "Enable/Disable User";
            this.btn_enabledisable.UseVisualStyleBackColor = true;
            this.btn_enabledisable.Click += new System.EventHandler(this.btn_enabledisable_Click);
            // 
            // btn_listusers
            // 
            this.btn_listusers.Location = new System.Drawing.Point(6, 77);
            this.btn_listusers.Name = "btn_listusers";
            this.btn_listusers.Size = new System.Drawing.Size(188, 23);
            this.btn_listusers.TabIndex = 2;
            this.btn_listusers.Text = "Refresh List";
            this.btn_listusers.UseVisualStyleBackColor = true;
            this.btn_listusers.Click += new System.EventHandler(this.btn_listusers_Click);
            // 
            // btn_adduser
            // 
            this.btn_adduser.Location = new System.Drawing.Point(6, 106);
            this.btn_adduser.Name = "btn_adduser";
            this.btn_adduser.Size = new System.Drawing.Size(188, 23);
            this.btn_adduser.TabIndex = 1;
            this.btn_adduser.Text = "Add User";
            this.btn_adduser.UseVisualStyleBackColor = true;
            this.btn_adduser.Click += new System.EventHandler(this.btn_adduser_Click);
            // 
            // cms_clients
            // 
            this.cms_clients.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msi_refreshusers,
            this.msi_userOptions,
            this.msi_copyShit});
            this.cms_clients.Name = "listviewCM";
            this.cms_clients.Size = new System.Drawing.Size(143, 70);
            // 
            // msi_refreshusers
            // 
            this.msi_refreshusers.Image = global::LMAOnline_Auth_Panel.Properties.Resources.restart;
            this.msi_refreshusers.Name = "msi_refreshusers";
            this.msi_refreshusers.Size = new System.Drawing.Size(142, 22);
            this.msi_refreshusers.Text = "Refresh List";
            // 
            // msi_userOptions
            // 
            this.msi_userOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msi_addUser,
            this.msi_editUser,
            this.msi_removeUser,
            this.msi_banUser,
            this.msi_beAnAsshole});
            this.msi_userOptions.Image = global::LMAOnline_Auth_Panel.Properties.Resources.group;
            this.msi_userOptions.Name = "msi_userOptions";
            this.msi_userOptions.Size = new System.Drawing.Size(142, 22);
            this.msi_userOptions.Text = "User Options";
            // 
            // msi_addUser
            // 
            this.msi_addUser.Image = global::LMAOnline_Auth_Panel.Properties.Resources.Users_Add_user_icon;
            this.msi_addUser.Name = "msi_addUser";
            this.msi_addUser.Size = new System.Drawing.Size(143, 22);
            this.msi_addUser.Text = "Add User";
            this.msi_addUser.Click += new System.EventHandler(this.msi_addUser_Click);
            // 
            // msi_editUser
            // 
            this.msi_editUser.Image = global::LMAOnline_Auth_Panel.Properties.Resources.settings;
            this.msi_editUser.Name = "msi_editUser";
            this.msi_editUser.Size = new System.Drawing.Size(143, 22);
            this.msi_editUser.Text = "Edit User";
            this.msi_editUser.Click += new System.EventHandler(this.msi_editUser_Click);
            // 
            // msi_removeUser
            // 
            this.msi_removeUser.Image = global::LMAOnline_Auth_Panel.Properties.Resources.delete;
            this.msi_removeUser.Name = "msi_removeUser";
            this.msi_removeUser.Size = new System.Drawing.Size(143, 22);
            this.msi_removeUser.Text = "Remove User";
            this.msi_removeUser.Click += new System.EventHandler(this.msi_removeUser_Click);
            // 
            // msi_banUser
            // 
            this.msi_banUser.Image = global::LMAOnline_Auth_Panel.Properties.Resources.ban;
            this.msi_banUser.Name = "msi_banUser";
            this.msi_banUser.Size = new System.Drawing.Size(143, 22);
            this.msi_banUser.Text = "Ban User";
            this.msi_banUser.Click += new System.EventHandler(this.msi_banUser_Click);
            // 
            // msi_beAnAsshole
            // 
            this.msi_beAnAsshole.Image = global::LMAOnline_Auth_Panel.Properties.Resources.money_bag_2;
            this.msi_beAnAsshole.Name = "msi_beAnAsshole";
            this.msi_beAnAsshole.Size = new System.Drawing.Size(143, 22);
            this.msi_beAnAsshole.Text = "Take KV";
            this.msi_beAnAsshole.Click += new System.EventHandler(this.msi_beAnAsshole_Click);
            // 
            // msi_copyShit
            // 
            this.msi_copyShit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msi_copyIP2,
            this.msi_copyCPU2,
            this.msi_copyGT2,
            this.msi_copyTime2,
            this.msi_copyLine2});
            this.msi_copyShit.Image = global::LMAOnline_Auth_Panel.Properties.Resources.Editing_Copy_icon;
            this.msi_copyShit.Name = "msi_copyShit";
            this.msi_copyShit.Size = new System.Drawing.Size(142, 22);
            this.msi_copyShit.Text = "Copy";
            // 
            // msi_copyIP2
            // 
            this.msi_copyIP2.Image = global::LMAOnline_Auth_Panel.Properties.Resources.Editing_Copy_icon;
            this.msi_copyIP2.Name = "msi_copyIP2";
            this.msi_copyIP2.Size = new System.Drawing.Size(133, 22);
            this.msi_copyIP2.Text = "IP Address";
            this.msi_copyIP2.Click += new System.EventHandler(this.msi_copyIP2_Click);
            // 
            // msi_copyCPU2
            // 
            this.msi_copyCPU2.Image = global::LMAOnline_Auth_Panel.Properties.Resources.Editing_Copy_icon;
            this.msi_copyCPU2.Name = "msi_copyCPU2";
            this.msi_copyCPU2.Size = new System.Drawing.Size(133, 22);
            this.msi_copyCPU2.Text = "CPU Key";
            this.msi_copyCPU2.Click += new System.EventHandler(this.msi_copyCPU2_Click);
            // 
            // msi_copyGT2
            // 
            this.msi_copyGT2.Image = global::LMAOnline_Auth_Panel.Properties.Resources.Editing_Copy_icon;
            this.msi_copyGT2.Name = "msi_copyGT2";
            this.msi_copyGT2.Size = new System.Drawing.Size(133, 22);
            this.msi_copyGT2.Text = "Gamertag";
            this.msi_copyGT2.Click += new System.EventHandler(this.msi_copyGT2_Click);
            // 
            // msi_copyTime2
            // 
            this.msi_copyTime2.Image = global::LMAOnline_Auth_Panel.Properties.Resources.Editing_Copy_icon;
            this.msi_copyTime2.Name = "msi_copyTime2";
            this.msi_copyTime2.Size = new System.Drawing.Size(133, 22);
            this.msi_copyTime2.Text = "Time Left";
            this.msi_copyTime2.Click += new System.EventHandler(this.msi_copyTime2_Click);
            // 
            // msi_copyLine2
            // 
            this.msi_copyLine2.Image = global::LMAOnline_Auth_Panel.Properties.Resources.Editing_Copy_icon;
            this.msi_copyLine2.Name = "msi_copyLine2";
            this.msi_copyLine2.Size = new System.Drawing.Size(133, 22);
            this.msi_copyLine2.Text = "Whole Line";
            this.msi_copyLine2.Click += new System.EventHandler(this.msi_copyLine2_Click);
            // 
            // lbl_overlay
            // 
            this.lbl_overlay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_overlay.BackColor = System.Drawing.Color.Transparent;
            this.lbl_overlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_overlay.ForeColor = System.Drawing.Color.Gray;
            this.lbl_overlay.Location = new System.Drawing.Point(337, 147);
            this.lbl_overlay.MaximumSize = new System.Drawing.Size(402, 52);
            this.lbl_overlay.MinimumSize = new System.Drawing.Size(402, 52);
            this.lbl_overlay.Name = "lbl_overlay";
            this.lbl_overlay.Size = new System.Drawing.Size(402, 52);
            this.lbl_overlay.TabIndex = 13;
            this.lbl_overlay.Text = "Right Click For Clients";
            this.lbl_overlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_overlay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_overlay_MouseDown);
            // 
            // lv_clients
            // 
            this.lv_clients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_clients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            ch_cpukey,
            ch_gamertag,
            ch_enabled,
            ch_name,
            ch_email,
            ch_titleid,
            ch_ip,
            ch_time,
            ch_lastseen});
            this.lv_clients.FullRowSelect = true;
            this.lv_clients.GridLines = true;
            this.lv_clients.Location = new System.Drawing.Point(1, 0);
            this.lv_clients.MultiSelect = false;
            this.lv_clients.Name = "lv_clients";
            this.lv_clients.Size = new System.Drawing.Size(1079, 321);
            this.lv_clients.TabIndex = 9;
            this.lv_clients.UseCompatibleStateImageBehavior = false;
            this.lv_clients.View = System.Windows.Forms.View.Details;
            this.lv_clients.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lv_clients_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1098, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Search:";
            // 
            // tb_search
            // 
            this.tb_search.Location = new System.Drawing.Point(1142, 286);
            this.tb_search.Name = "tb_search";
            this.tb_search.Size = new System.Drawing.Size(106, 20);
            this.tb_search.TabIndex = 15;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(1255, 284);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(24, 23);
            this.btn_search.TabIndex = 16;
            this.btn_search.Text = "..";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 321);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.tb_search);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_overlay);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lv_clients);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1307, 360);
            this.Name = "Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LMAOnline Auth Panel v2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.cms_clients.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_totalclients;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_bannedConsoles;
        private System.Windows.Forms.Button btn_stealkv;
        private System.Windows.Forms.Button btn_failedlogins;
        private System.Windows.Forms.Button btn_enabledisable;
        private System.Windows.Forms.Button btn_listusers;
        private System.Windows.Forms.Button btn_adduser;
        private System.Windows.Forms.ContextMenuStrip cms_clients;
        private System.Windows.Forms.ToolStripMenuItem msi_refreshusers;
        private System.Windows.Forms.Label lbl_overlay;
        private System.Windows.Forms.ToolStripMenuItem msi_copyShit;
        private System.Windows.Forms.ToolStripMenuItem msi_copyIP2;
        private System.Windows.Forms.ToolStripMenuItem msi_copyCPU2;
        private System.Windows.Forms.ToolStripMenuItem msi_copyGT2;
        private System.Windows.Forms.ToolStripMenuItem msi_copyTime2;
        private System.Windows.Forms.ToolStripMenuItem msi_copyLine2;
        private System.Windows.Forms.ToolStripMenuItem msi_userOptions;
        private System.Windows.Forms.ToolStripMenuItem msi_addUser;
        private System.Windows.Forms.ToolStripMenuItem msi_removeUser;
        private System.Windows.Forms.ToolStripMenuItem msi_banUser;
        private System.Windows.Forms.ToolStripMenuItem msi_beAnAsshole;
        private System.Windows.Forms.ToolStripMenuItem msi_editUser;
        private System.Windows.Forms.ListView lv_clients;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_search;
        private System.Windows.Forms.Button btn_search;
    }
}

