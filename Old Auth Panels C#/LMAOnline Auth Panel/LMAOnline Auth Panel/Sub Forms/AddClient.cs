using System;
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
    public partial class AddClient : Form
    {
        ConsoleManager cm = new ConsoleManager();
        ConsoleEntry ce = new ConsoleEntry();

        bool setMode = false;
        bool updateconsole = false;
        Dictionary<string, DateTime> expireTimes;

        public AddClient()
        {
            InitializeComponent();
        }

        private int timetodays(int month, int weeks, int days)
        {
            if (month > 0)
                month = month * 30;
            if (weeks > 0)
                weeks = weeks * 7;

            return month + weeks + days;
        }

        private void btn_addclient_Click(object sender, EventArgs e)
        {
            if (!updateconsole)
            {
                if (!cm.CheckIfExists(tb_cpukey.Text))
                {
                    cm.AddConsole(tb_name.Text, tb_adder.Text, tb_cpukey.Text, timetodays((int)nud_months.Value, (int)nud_weeks.Value, (int)nud_days.Value));
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    cm.GetConsole(ref ce, tb_cpukey.Text);
                    if (MessageBox.Show("This CPU key already exists, would you like modify the user's info?", "Client Exists", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (MessageBox.Show("Would you like to load the previous information?", "Client Exists", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            tb_adder.Text = ce.email;
                            tb_name.Text = ce.name;
                            updateconsole = true;
                        }
                        else { updateconsole = true; }
                    }
                }
            }
            else
            {
                cm.update_Console(tb_name.Text, tb_adder.Text, tb_cpukey.Text, timetodays((int)nud_months.Value, (int)nud_weeks.Value, (int)nud_days.Value));
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void AddClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
