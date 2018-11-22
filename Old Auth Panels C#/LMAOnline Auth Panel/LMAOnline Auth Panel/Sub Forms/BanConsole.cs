using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LMAOnline.Classes;

namespace LMAOnline.Forms
{
    public partial class BanConsole : Form
    {
        ConsoleManager cm = new ConsoleManager();
        ConsoleEntry ce = new ConsoleEntry();

        public BanConsole()
        {
            InitializeComponent();
        }

        private void BanConsole_Load(object sender, EventArgs e)
        {
            if (Globals.addingBan == true)
            {
                tb_cpukey.Text = Globals.cpukey;
                tb_name.Text = Globals.Name;
                Globals.addingBan = false;
            }
        }

        private void btn_ban_Click(object sender, EventArgs e)
        {
            if (tb_cpukey.Text != "" && rtb_reason.Text != "" && tb_name.Text != "")
            {
                if (tb_cpukey.Text.Count() == 32)
                {
                    cm.GetConsole(ref ce, tb_cpukey.Text);
                    ce.ip = IPAddress.Parse("0.0.0.0");
                    cm.AddBannedConsole(tb_name.Text, tb_cpukey.Text, rtb_reason.Text);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }
    }
}
