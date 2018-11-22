using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using LMAOnline_Tokens.Config;

namespace LMAOnline_Tokens.SubForms
{
    public partial class frm_createToken : Form
    {
        WebClient wc = new WebClient();
        Functions cm = new Functions();
        Globals entry = new Globals();

        public frm_createToken()
        {
            InitializeComponent();
        }

        private string getToken()
        {
            return wc.DownloadString("http://xblrogers.com/~API/tokenGen.php");
        }

        private string getIP()
        {
            return wc.DownloadString("http://blowmyasshole.net78.net/");
        }

        private void btn_createToken_Click(object sender, EventArgs e)
        {
            string token = getToken().ToUpper();
            if (token.Length == 32)
            {
                for (int i = 0; i < nud_miltipledays.Value; i++)
                {
                    cm.saveToken(getToken().ToUpper(), getIP(), nud_days.Value.ToString());
                }
                //if (cm.validToken(ref entry, token))
                //{
                //    tb_result.Text = String.Format("{0} Day Token - {1}", nud_days.Value, token);
                //    btn_copy.Enabled = true;
                //    MessageBox.Show("Token Generated.");
                //}
            }
        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            if (tb_result.Text.Count() > 32)
            {
                Clipboard.SetText(tb_result.Text.Split(' ')[4]);
                DialogResult = DialogResult.OK;
            }
        }
    }
}
