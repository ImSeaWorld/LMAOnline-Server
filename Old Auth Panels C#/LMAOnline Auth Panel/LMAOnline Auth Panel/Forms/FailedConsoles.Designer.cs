namespace LMAOnline.Forms
{
    partial class FailedConsoles
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
            this.btn_jackkv = new System.Windows.Forms.Button();
            this.btn_listusers = new System.Windows.Forms.Button();
            this.lv_failedusers = new System.Windows.Forms.ListView();
            this.ch_cpukey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_ip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cm_failedusers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stealKVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeCPUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.authCPUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.cm_failedusers.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btn_jackkv);
            this.groupBox1.Controls.Add(this.btn_listusers);
            this.groupBox1.Controls.Add(this.lv_failedusers);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 295);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Failed Consoles";
            // 
            // btn_jackkv
            // 
            this.btn_jackkv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_jackkv.Location = new System.Drawing.Point(188, 21);
            this.btn_jackkv.Name = "btn_jackkv";
            this.btn_jackkv.Size = new System.Drawing.Size(139, 23);
            this.btn_jackkv.TabIndex = 9;
            this.btn_jackkv.Text = "Get KV";
            this.btn_jackkv.UseVisualStyleBackColor = true;
            this.btn_jackkv.Click += new System.EventHandler(this.btn_jackkv_Click);
            // 
            // btn_listusers
            // 
            this.btn_listusers.Location = new System.Drawing.Point(17, 21);
            this.btn_listusers.Name = "btn_listusers";
            this.btn_listusers.Size = new System.Drawing.Size(139, 23);
            this.btn_listusers.TabIndex = 8;
            this.btn_listusers.Text = "List Users";
            this.btn_listusers.UseVisualStyleBackColor = true;
            this.btn_listusers.Click += new System.EventHandler(this.btn_listusers_Click);
            // 
            // lv_failedusers
            // 
            this.lv_failedusers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_failedusers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_cpukey,
            this.ch_ip});
            this.lv_failedusers.FullRowSelect = true;
            this.lv_failedusers.GridLines = true;
            this.lv_failedusers.Location = new System.Drawing.Point(6, 50);
            this.lv_failedusers.Name = "lv_failedusers";
            this.lv_failedusers.Size = new System.Drawing.Size(332, 239);
            this.lv_failedusers.TabIndex = 7;
            this.lv_failedusers.UseCompatibleStateImageBehavior = false;
            this.lv_failedusers.View = System.Windows.Forms.View.Details;
            // 
            // ch_cpukey
            // 
            this.ch_cpukey.Text = "CPUKey";
            this.ch_cpukey.Width = 228;
            // 
            // ch_ip
            // 
            this.ch_ip.Text = "IP";
            this.ch_ip.Width = 100;
            // 
            // cm_failedusers
            // 
            this.cm_failedusers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stealKVToolStripMenuItem,
            this.removeCPUToolStripMenuItem,
            this.authCPUToolStripMenuItem});
            this.cm_failedusers.Name = "cm_failedusers";
            this.cm_failedusers.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cm_failedusers.Size = new System.Drawing.Size(144, 70);
            // 
            // stealKVToolStripMenuItem
            // 
            this.stealKVToolStripMenuItem.Name = "stealKVToolStripMenuItem";
            this.stealKVToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.stealKVToolStripMenuItem.Text = "Steal KV";
            // 
            // removeCPUToolStripMenuItem
            // 
            this.removeCPUToolStripMenuItem.Name = "removeCPUToolStripMenuItem";
            this.removeCPUToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.removeCPUToolStripMenuItem.Text = "Remove CPU";
            // 
            // authCPUToolStripMenuItem
            // 
            this.authCPUToolStripMenuItem.Name = "authCPUToolStripMenuItem";
            this.authCPUToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.authCPUToolStripMenuItem.Text = "Auth CPU";
            // 
            // FailedConsoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 317);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FailedConsoles";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Failed Consoles";
            this.groupBox1.ResumeLayout(false);
            this.cm_failedusers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_jackkv;
        private System.Windows.Forms.Button btn_listusers;
        private System.Windows.Forms.ListView lv_failedusers;
        private System.Windows.Forms.ColumnHeader ch_cpukey;
        private System.Windows.Forms.ColumnHeader ch_ip;
        private System.Windows.Forms.ContextMenuStrip cm_failedusers;
        private System.Windows.Forms.ToolStripMenuItem stealKVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeCPUToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem authCPUToolStripMenuItem;
    }
}