namespace LMAOnline_Tokens
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.lv_tokens = new System.Windows.Forms.ListView();
            this.ch_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_token = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_days = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_creator = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_used = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_colorCoat = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.btn_createToken = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_unused = new System.Windows.Forms.Label();
            this.lbl_used = new System.Windows.Forms.Label();
            this.lbl_total = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_overlay = new System.Windows.Forms.Label();
            this.cms_bawls = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tokenOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.revertUsedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tokenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wholeLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.cms_bawls.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_tokens
            // 
            this.lv_tokens.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lv_tokens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_id,
            this.ch_token,
            this.ch_days,
            this.ch_creator,
            this.ch_used});
            this.lv_tokens.FullRowSelect = true;
            this.lv_tokens.GridLines = true;
            this.lv_tokens.Location = new System.Drawing.Point(0, 0);
            this.lv_tokens.MultiSelect = false;
            this.lv_tokens.Name = "lv_tokens";
            this.lv_tokens.Size = new System.Drawing.Size(524, 275);
            this.lv_tokens.TabIndex = 10;
            this.lv_tokens.UseCompatibleStateImageBehavior = false;
            this.lv_tokens.View = System.Windows.Forms.View.Details;
            this.lv_tokens.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lv_tokens_MouseDown);
            // 
            // ch_id
            // 
            this.ch_id.Text = "ID";
            this.ch_id.Width = 55;
            // 
            // ch_token
            // 
            this.ch_token.Text = "Token";
            this.ch_token.Width = 220;
            // 
            // ch_days
            // 
            this.ch_days.Text = "Days";
            this.ch_days.Width = 55;
            // 
            // ch_creator
            // 
            this.ch_creator.Text = "Creator";
            this.ch_creator.Width = 130;
            // 
            // ch_used
            // 
            this.ch_used.Text = "Used";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_colorCoat);
            this.groupBox1.Controls.Add(this.btn_refresh);
            this.groupBox1.Controls.Add(this.btn_createToken);
            this.groupBox1.Location = new System.Drawing.Point(530, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 112);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Silly Shit";
            // 
            // btn_colorCoat
            // 
            this.btn_colorCoat.Location = new System.Drawing.Point(6, 77);
            this.btn_colorCoat.Name = "btn_colorCoat";
            this.btn_colorCoat.Size = new System.Drawing.Size(188, 23);
            this.btn_colorCoat.TabIndex = 2;
            this.btn_colorCoat.Text = "Color Coat Shit";
            this.btn_colorCoat.UseVisualStyleBackColor = true;
            // 
            // btn_refresh
            // 
            this.btn_refresh.Location = new System.Drawing.Point(6, 48);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(188, 23);
            this.btn_refresh.TabIndex = 1;
            this.btn_refresh.Text = "Refresh List";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btn_createToken
            // 
            this.btn_createToken.Location = new System.Drawing.Point(6, 19);
            this.btn_createToken.Name = "btn_createToken";
            this.btn_createToken.Size = new System.Drawing.Size(188, 23);
            this.btn_createToken.TabIndex = 0;
            this.btn_createToken.Text = "Create Token";
            this.btn_createToken.UseVisualStyleBackColor = true;
            this.btn_createToken.Click += new System.EventHandler(this.btn_createToken_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_unused);
            this.groupBox2.Controls.Add(this.lbl_used);
            this.groupBox2.Controls.Add(this.lbl_total);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(530, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Token Stats";
            // 
            // lbl_unused
            // 
            this.lbl_unused.AutoSize = true;
            this.lbl_unused.Location = new System.Drawing.Point(100, 71);
            this.lbl_unused.Name = "lbl_unused";
            this.lbl_unused.Size = new System.Drawing.Size(13, 13);
            this.lbl_unused.TabIndex = 5;
            this.lbl_unused.Text = "0";
            // 
            // lbl_used
            // 
            this.lbl_used.AutoSize = true;
            this.lbl_used.Location = new System.Drawing.Point(100, 48);
            this.lbl_used.Name = "lbl_used";
            this.lbl_used.Size = new System.Drawing.Size(13, 13);
            this.lbl_used.TabIndex = 4;
            this.lbl_used.Text = "0";
            // 
            // lbl_total
            // 
            this.lbl_total.AutoSize = true;
            this.lbl_total.Location = new System.Drawing.Point(100, 25);
            this.lbl_total.Name = "lbl_total";
            this.lbl_total.Size = new System.Drawing.Size(13, 13);
            this.lbl_total.TabIndex = 3;
            this.lbl_total.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Unused Tokens:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Used Tokens:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Tokens:";
            // 
            // lbl_overlay
            // 
            this.lbl_overlay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_overlay.BackColor = System.Drawing.Color.Transparent;
            this.lbl_overlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_overlay.ForeColor = System.Drawing.Color.Gray;
            this.lbl_overlay.Location = new System.Drawing.Point(49, 116);
            this.lbl_overlay.MaximumSize = new System.Drawing.Size(402, 52);
            this.lbl_overlay.MinimumSize = new System.Drawing.Size(402, 52);
            this.lbl_overlay.Name = "lbl_overlay";
            this.lbl_overlay.Size = new System.Drawing.Size(402, 52);
            this.lbl_overlay.TabIndex = 14;
            this.lbl_overlay.Text = "Right Click For Tokens";
            this.lbl_overlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_overlay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_overlay_MouseClick);
            // 
            // cms_bawls
            // 
            this.cms_bawls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshListToolStripMenuItem,
            this.tokenOptionsToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.cms_bawls.Name = "cms_bawls";
            this.cms_bawls.Size = new System.Drawing.Size(153, 92);
            // 
            // refreshListToolStripMenuItem
            // 
            this.refreshListToolStripMenuItem.Image = global::LMAOnline_Tokens.Properties.Resources.restart;
            this.refreshListToolStripMenuItem.Name = "refreshListToolStripMenuItem";
            this.refreshListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.refreshListToolStripMenuItem.Text = "Refresh List";
            // 
            // tokenOptionsToolStripMenuItem
            // 
            this.tokenOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.revertUsedToolStripMenuItem,
            this.addDaysToolStripMenuItem,
            this.deleteDaysToolStripMenuItem});
            this.tokenOptionsToolStripMenuItem.Image = global::LMAOnline_Tokens.Properties.Resources.resize3;
            this.tokenOptionsToolStripMenuItem.Name = "tokenOptionsToolStripMenuItem";
            this.tokenOptionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tokenOptionsToolStripMenuItem.Text = "Token Options";
            // 
            // revertUsedToolStripMenuItem
            // 
            this.revertUsedToolStripMenuItem.Image = global::LMAOnline_Tokens.Properties.Resources.settings;
            this.revertUsedToolStripMenuItem.Name = "revertUsedToolStripMenuItem";
            this.revertUsedToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.revertUsedToolStripMenuItem.Text = "Revert Used/Unused";
            this.revertUsedToolStripMenuItem.Click += new System.EventHandler(this.revertUsedToolStripMenuItem_Click);
            // 
            // addDaysToolStripMenuItem
            // 
            this.addDaysToolStripMenuItem.Image = global::LMAOnline_Tokens.Properties.Resources.Users_Add_user_icon;
            this.addDaysToolStripMenuItem.Name = "addDaysToolStripMenuItem";
            this.addDaysToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.addDaysToolStripMenuItem.Text = "Add Days";
            // 
            // deleteDaysToolStripMenuItem
            // 
            this.deleteDaysToolStripMenuItem.Image = global::LMAOnline_Tokens.Properties.Resources.remove;
            this.deleteDaysToolStripMenuItem.Name = "deleteDaysToolStripMenuItem";
            this.deleteDaysToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.deleteDaysToolStripMenuItem.Text = "Delete Token";
            this.deleteDaysToolStripMenuItem.Click += new System.EventHandler(this.deleteDaysToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tokenToolStripMenuItem,
            this.creatorToolStripMenuItem,
            this.wholeLineToolStripMenuItem});
            this.copyToolStripMenuItem.Image = global::LMAOnline_Tokens.Properties.Resources.Editing_Copy_icon;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // tokenToolStripMenuItem
            // 
            this.tokenToolStripMenuItem.Image = global::LMAOnline_Tokens.Properties.Resources.Editing_Copy_icon1;
            this.tokenToolStripMenuItem.Name = "tokenToolStripMenuItem";
            this.tokenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tokenToolStripMenuItem.Text = "Token";
            this.tokenToolStripMenuItem.Click += new System.EventHandler(this.tokenToolStripMenuItem_Click);
            // 
            // creatorToolStripMenuItem
            // 
            this.creatorToolStripMenuItem.Image = global::LMAOnline_Tokens.Properties.Resources.Editing_Copy_icon1;
            this.creatorToolStripMenuItem.Name = "creatorToolStripMenuItem";
            this.creatorToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.creatorToolStripMenuItem.Text = "Creator";
            // 
            // wholeLineToolStripMenuItem
            // 
            this.wholeLineToolStripMenuItem.Image = global::LMAOnline_Tokens.Properties.Resources.Editing_Copy_icon1;
            this.wholeLineToolStripMenuItem.Name = "wholeLineToolStripMenuItem";
            this.wholeLineToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.wholeLineToolStripMenuItem.Text = "Whole Line";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 275);
            this.Controls.Add(this.lbl_overlay);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lv_tokens);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(753, 314);
            this.Name = "Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LMAOnline Tokens";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.cms_bawls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lv_tokens;
        private System.Windows.Forms.ColumnHeader ch_id;
        private System.Windows.Forms.ColumnHeader ch_token;
        private System.Windows.Forms.ColumnHeader ch_days;
        private System.Windows.Forms.ColumnHeader ch_creator;
        private System.Windows.Forms.ColumnHeader ch_used;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_colorCoat;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Button btn_createToken;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_unused;
        private System.Windows.Forms.Label lbl_used;
        private System.Windows.Forms.Label lbl_total;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_overlay;
        private System.Windows.Forms.ContextMenuStrip cms_bawls;
        private System.Windows.Forms.ToolStripMenuItem refreshListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tokenOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem revertUsedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tokenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wholeLineToolStripMenuItem;
    }
}

