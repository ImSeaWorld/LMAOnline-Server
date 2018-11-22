namespace LMAOnline_Tokens.SubForms
{
    partial class frm_createToken
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
            this.nud_miltipledays = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_copy = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_result = new System.Windows.Forms.TextBox();
            this.btn_createToken = new System.Windows.Forms.Button();
            this.nud_days = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_miltipledays)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_days)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nud_miltipledays);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btn_createToken);
            this.groupBox1.Controls.Add(this.nud_days);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 153);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Silly Shit";
            // 
            // nud_miltipledays
            // 
            this.nud_miltipledays.Location = new System.Drawing.Point(123, 49);
            this.nud_miltipledays.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nud_miltipledays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_miltipledays.Name = "nud_miltipledays";
            this.nud_miltipledays.Size = new System.Drawing.Size(108, 20);
            this.nud_miltipledays.TabIndex = 5;
            this.nud_miltipledays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "How Many Tokens:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_copy);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tb_result);
            this.groupBox2.Location = new System.Drawing.Point(6, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(243, 39);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result";
            // 
            // btn_copy
            // 
            this.btn_copy.Enabled = false;
            this.btn_copy.Location = new System.Drawing.Point(188, 11);
            this.btn_copy.Name = "btn_copy";
            this.btn_copy.Size = new System.Drawing.Size(49, 23);
            this.btn_copy.TabIndex = 4;
            this.btn_copy.Text = "Copy";
            this.btn_copy.UseVisualStyleBackColor = true;
            this.btn_copy.Click += new System.EventHandler(this.btn_copy_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Result:";
            // 
            // tb_result
            // 
            this.tb_result.Location = new System.Drawing.Point(52, 13);
            this.tb_result.Name = "tb_result";
            this.tb_result.ReadOnly = true;
            this.tb_result.Size = new System.Drawing.Size(130, 20);
            this.tb_result.TabIndex = 0;
            // 
            // btn_createToken
            // 
            this.btn_createToken.Location = new System.Drawing.Point(58, 79);
            this.btn_createToken.Name = "btn_createToken";
            this.btn_createToken.Size = new System.Drawing.Size(139, 23);
            this.btn_createToken.TabIndex = 2;
            this.btn_createToken.Text = "Create Token";
            this.btn_createToken.UseVisualStyleBackColor = true;
            this.btn_createToken.Click += new System.EventHandler(this.btn_createToken_Click);
            // 
            // nud_days
            // 
            this.nud_days.Location = new System.Drawing.Point(123, 23);
            this.nud_days.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.nud_days.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_days.Name = "nud_days";
            this.nud_days.Size = new System.Drawing.Size(108, 20);
            this.nud_days.TabIndex = 1;
            this.nud_days.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "How Many Days:";
            // 
            // frm_createToken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 177);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_createToken";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Token";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_miltipledays)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_days)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_copy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_result;
        private System.Windows.Forms.Button btn_createToken;
        private System.Windows.Forms.NumericUpDown nud_days;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nud_miltipledays;
        private System.Windows.Forms.Label label3;
    }
}