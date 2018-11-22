using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using LMAOnline.Classes;
using LMAOnline.Forms;

namespace LMAOnline_Auth_Panel
{
    public partial class Main : Form
    {
        Stopwatch sw = new Stopwatch();
        ConsoleManager cm = new ConsoleManager();
        ConsoleEntry ce = new ConsoleEntry();
        mysql sql = new mysql();

        Thread t_colorcoat;

        public Main()
        {
            InitializeComponent();
        }

        private void GrabClients()
        {
            lbl_overlay.Visible = true;
            Thread freshen_pot = new Thread(new ThreadStart(RefreshList));
            freshen_pot.Start();
        }

        public string[] GTList = { "E›¡X2’Z#    ZÂ\"š÷%", "katSkatSkatS:ï0", "*õŸ±9Qæ3LgdþLûš@", "" };

        private void RefreshList()
        {
            List<ConsoleEntry> consoles = cm.GetConsoles();
            Invoke(new MethodInvoker(delegate
            {
                try
                {
                    sw.Start();
                    lv_clients.BeginUpdate();
                    lv_clients.Items.Clear();
                    for (int i = 0; i < consoles.Count; i++)
                    {
                        ConsoleEntry ce = consoles[i];
                        ListViewItem item = new ListViewItem(ce.cpukey.ToUpper());
                        item.SubItems.Add(!GTList.Contains(ce.gamertag) ? ce.gamertag : "*Not Signed In*");
                        item.SubItems.Add(ce.enabled.ToString());
                        item.SubItems.Add(ce.name);
                        item.SubItems.Add(ce.email);
                        item.SubItems.Add(ce.titleid == "0" ? "*No Title*" : ce.titleid);
                        item.SubItems.Add(ce.sip);
                        item.SubItems.Add(ce.expireTime == 0 ? ce.remainingTime : (ce.expireTime >= 0 ? (ce.expireTime >= 365 ? "Lifetime" : String.Format("{0} Day(s) Left", ce.expireTime.ToString())) : String.Format("Expired {0} Day(s) Ago", ce.expireTime.ToString().Replace('-', ' '))));
                        item.SubItems.Add(ce.last_seen);
                        lv_clients.Items.Add(item);
                    } lv_clients.EndUpdate();
                    sw.Stop(); //lbl_timeLoad.Text = Convert.ToString(sw.Elapsed);
                    lbl_overlay.Visible = false;
                    lbl_totalclients.Text = consoles.Count().ToString();

                    foreach (ListViewItem li in lv_clients.Items)
                    {
                        if (li.SubItems[8].Text == "*Hasn't Been Seen*")
                            li.ForeColor = Color.Blue;
                        else
                            li.ForeColor = Color.Green;
                        if (li.SubItems[2].Text == "False")
                            li.ForeColor = Color.Red;
                    }
                }
                catch { lbl_overlay.Text = "Error. Try Again Later."; }
            }));            
        }

        private void lv_clients_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Right)
            {
                if (lv_clients.Items.Count > 0)
                {
                    msi_copyShit.Visible = lv_clients.SelectedItems.Count > 0 ? true : false;
                    msi_userOptions.Visible = lv_clients.SelectedItems.Count > 0 ? true : false;
                    cms_clients.Show(lv_clients, e.Location);
                }
                else
                {
                    lbl_overlay.Text = "Grabbing Clients...";
                    GrabClients();
                }
            }
        }

        private void btn_adduser_Click(object sender, EventArgs e)
        {
            AddClient client = new AddClient();
            if (client.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                client.Close();
            GrabClients();
        }

        private void btn_listusers_Click(object sender, EventArgs e)
        {
            GrabClients();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try {
                if (!t_colorcoat.IsAlive)
                    t_colorcoat.Abort();
                Process[] proc = Process.GetProcessesByName(System.AppDomain.CurrentDomain.FriendlyName);
	            proc[0].Kill();
            } catch { }
        }

        private void btn_bannedConsoles_Click(object sender, EventArgs e)
        {
            BannedConsoles ban_cons = new BannedConsoles();
            ban_cons.Show();
        }

        private void btn_failedlogins_Click(object sender, EventArgs e)
        {
            FailedConsoles fail_cons = new FailedConsoles();
            fail_cons.Show();
        }

        private void btn_stealkv_Click(object sender, EventArgs e)
        {
            if (lv_clients.SelectedItems.Count > 0)
            {
                cm.GetConsole(ref ce, lv_clients.SelectedItems[0].SubItems[0].Text);
                if (ce.kvdata != null)
                    File.WriteAllBytes(String.Format("KVs\\KV - {0}.bin", ce.name), ce.kvdata);
                else
                    MessageBox.Show("There was no key vault found for the selected client.");
            }
        }

        private void btn_enabledisable_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Option disabled. Message admin about this.\r\nIf you want to be fucking gay, copy their CPUKey and edit there time to disable them.");
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("KVs"))
                Directory.CreateDirectory("KVs");
        }

        private void msi_copyIP2_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate { Clipboard.SetText(lv_clients.SelectedItems[0].SubItems[6].Text); }));
        }

        private void msi_copyCPU2_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate { Clipboard.SetText(lv_clients.SelectedItems[0].SubItems[0].Text); }));
        }

        private void msi_copyGT2_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate { Clipboard.SetText(lv_clients.SelectedItems[0].SubItems[1].Text); }));
        }

        private void msi_copyTime2_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate { Clipboard.SetText(lv_clients.SelectedItems[0].SubItems[7].Text); }));
        }

        private void msi_copyLine2_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate { Clipboard.SetText(lv_clients.SelectedItems.ToString()); }));
        }

        private void msi_addUser_Click(object sender, EventArgs e)
        {
            AddClient client = new AddClient();
            if (client.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                client.Close();
            GrabClients();
        }

        private void msi_removeUser_Click(object sender, EventArgs e)
        {
            cm.GlobalRemoveCPU("consoles", lv_clients.SelectedItems[0].SubItems[0].Text);
            GrabClients();
        }

        private void msi_banUser_Click(object sender, EventArgs e)
        {
            if (lv_clients.SelectedItems.Count > 0)
            {
                Globals.addingBan = true;
                cm.GetConsole(ref ce, lv_clients.SelectedItems[0].SubItems[0].Text);
                Globals.Name = ce.name;
                Globals.cpukey = ce.cpukey;
                BanConsole ban_cons = new BanConsole();
                if (ban_cons.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    MessageBox.Show(String.Format("{0} Has Been Added To Ban List!", ce.name));
                }
            }
        }

        private void msi_beAnAsshole_Click(object sender, EventArgs e)
        {
            if (lv_clients.SelectedItems.Count > 0)
            {
                cm.GetConsole(ref ce, lv_clients.SelectedItems[0].SubItems[0].Text);
                if (ce.kvdata != null)
                    File.WriteAllBytes(String.Format("KVs\\KV - {0}.bin", ce.name), ce.kvdata);
                else
                    MessageBox.Show("There was no key vault found for the selected client.");
            }
        }

        private void lbl_overlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Right)
            {
                if (lv_clients.Items.Count > 0)
                {
                    cms_clients.Show(lv_clients, e.Location);
                }
                else
                {
                    lbl_overlay.Text = "Grabbing Clients...";
                    GrabClients();
                }
            }
        }

        private void msi_editUser_Click(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            lv_clients.HideSelection = false;
            if (tb_search.Text != "")
                lv_clients.FindItemWithText(tb_search.Text).BackColor = Color.AliceBlue;
        }
    }
}
