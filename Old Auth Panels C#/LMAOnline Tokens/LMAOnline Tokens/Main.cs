using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LMAOnline_Tokens.Config;

namespace LMAOnline_Tokens
{
    public partial class Main : Form
    {
        Functions cm = new Functions();
        int unused = 0;
        int used = 0;

        public Main()
        {
            InitializeComponent();
        }

        private void RefreshTokens()
        {
            lbl_overlay.Visible = true;
            lbl_overlay.Text = "Grabbing Tokens...";
            List<Globals> tokens = cm.getTokens();
            Invoke(new MethodInvoker(delegate
            {
                lv_tokens.Items.Clear();
                for (int i = 0; i < tokens.Count; i++)
                {
                    Globals ce = tokens[i];
                    ListViewItem item = new ListViewItem(ce.id.ToString());
                    item.SubItems.Add(ce.token);
                    item.SubItems.Add(ce.days.ToString());
                    item.SubItems.Add(ce.creator);
                    item.SubItems.Add(ce.used ? "Yes" : "No");
                    lv_tokens.Items.Add(item);
                }
                lbl_overlay.Visible = false;
                lbl_total.Text = tokens.Count.ToString();
                unused = 0; used = 0;
                foreach (ListViewItem li in lv_tokens.Items)
                {
                    if (li.SubItems[4].Text == "No") {
                        unused++;
                        li.ForeColor = Color.Green;
                    } else {
                        used++;
                        li.ForeColor = Color.OrangeRed;
                    }
                }

                lbl_unused.Text = unused.ToString();
                lbl_used.Text = used.ToString();
            }));
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            RefreshTokens();            
        }

        private void lbl_overlay_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                if (lv_tokens.Items.Count == 0)
                    RefreshTokens();
        }

        private void btn_createToken_Click(object sender, EventArgs e)
        {
            SubForms.frm_createToken token = new SubForms.frm_createToken();
            if (token.ShowDialog() == DialogResult.OK)
                token.Close();
            RefreshTokens();
        }

        private void lv_tokens_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Right)
            {
                if (lv_tokens.Items.Count == 0)
                    RefreshTokens();
                else
                    cms_bawls.Show(lv_tokens, e.X, e.Y);
            }
        }

        private void revertUsedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cm.toggleUsedToken(lv_tokens.SelectedItems[0].SubItems[1].Text, Convert.ToBoolean(lv_tokens.SelectedItems[0].SubItems[4].Text == "No" ? true : false));
            RefreshTokens();
        }

        private void tokenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate { Clipboard.SetText(lv_tokens.SelectedItems[0].SubItems[1].Text); }));
        }

        private void deleteDaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cm.delToken(lv_tokens.SelectedItems[0].SubItems[1].Text);
            RefreshTokens();
        }
    }
}
