using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using LB_LMAOnline.Managers;

namespace LB_LMAOnline.Forms
{
    public partial class frm_iniEdit : Form
    {
        public frm_iniEdit()
        {
            InitializeComponent();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            if (File.Exists("ini\\recLMAO.ini"))
            {
                switch (MessageBox.Show("Would you like to use the ini located at: \"ini\\recLMAO.ini\"?\n\nYes - Use this INI.\nNo - Open file dialog.\nCancel - Closes this message box with little consequences.", "Oi m8", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        loadINI("ini\\recLMAO.ini");
                        break;
                    case DialogResult.No:
                        loadINI(ofDialog());
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("girl you must be the recently cancelled television show \"enterprise\" because no matter how many fat sweaty nerds profess their love for you, you are still just a piece of shit whore that no respectable man would allow himself to be seen in public with", "Lol u faget.");
                        break;
                    default: break;
                }
            }
            else loadINI(ofDialog());
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string saveLocation = sfDialog();
            saveINI(saveLocation);
            if (MessageBox.Show("Would you like to send this to the server?", "Oi m8", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Thread iniThread = new Thread(new ThreadStart(delegate
                {
                    try {
                        TcpClient client = new TcpClient(Globals.IPAddr, Globals.SvrPort);
                        NetworkStream nStream = client.GetStream();
                        EndianIO readerIO = new EndianIO(nStream, EndianStyle.BigEndian) { Writer = new EndianWriter(nStream, EndianStyle.BigEndian) };
                        EndianIO writerIO = new EndianIO(nStream, EndianStyle.BigEndian);
                        writerIO.Writer.Write("2F9A59018B92AA2C3B375D0D66CF8255");
                        LMAOnlineLBS.sendINI(readerIO, writerIO, saveLocation);
                        readerIO.Close(); writerIO.Close(); nStream.Close();
                    } catch { }
                }));
                iniThread.Start();
                MessageBox.Show("INI sent to server and loaded.", "INI Sent!");
            }
        }

        private string ofDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Find the INI you faggot.";
            ofd.Filter = "INI File (*.ini) | *.ini";
            ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (ofd.ShowDialog() == DialogResult.OK)
                if (File.Exists(ofd.FileName))
                    return ofd.FileName;
            return "";
        }

        private string sfDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save INI Faggot.";
            sfd.Filter = "INI File (*.ini)|*.ini";
            sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (sfd.ShowDialog() == DialogResult.OK)
                return sfd.FileName;
            return "";
        }

        private void loadINI(string fileLocation)
        {
            if (fileLocation == "") return;
            if (File.Exists(fileLocation)) {
                IniFile ini = new IniFile(fileLocation);
                cb_xexChecks.Checked = Convert.ToBoolean(ini.IniReadValue("Server", "Override XEX Checks"));
                cb_startup.Checked = Convert.ToBoolean(ini.IniReadValue("Server", "Add to Startup"));
                cb_anonUsers.Checked = Convert.ToBoolean(ini.IniReadValue("Server", "Allow Anonymous Users"));
                nud_port.Value = Convert.ToInt32(ini.IniReadValue("Network", "Port"));
                cb_parseLogs.Checked = Convert.ToBoolean(ini.IniReadValue("Logging", "Parse Logs"));
                cb_logCon.Checked = Convert.ToBoolean(ini.IniReadValue("Logging", "Log Connection"));
                cb_logCrash.Checked = Convert.ToBoolean(ini.IniReadValue("Logging", "Log Client Crash"));
                tb_serverAddr.Text = Convert.ToString(ini.IniReadValue("MySQL", "Server Address"));
                tb_dbName.Text = Convert.ToString(ini.IniReadValue("MySQL", "Database Name"));
                tb_dbUser.Text = Convert.ToString(ini.IniReadValue("MySQL", "Database Username"));
                tb_dbPass.Text = Convert.ToString(ini.IniReadValue("MySQL", "Database Password"));
            } else MessageBox.Show("Welp. You fucking broke it. Good job. You know, I was just about to say good things about you, then you do this. You faggot.", "God damn it billy!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void saveINI(string location)
        {
            if (location == "") return;
            IniFile ini = new IniFile(location);
            ini.IniWriteValue("Server", "Override XEX Checks", cb_xexChecks.Checked.ToString());
            ini.IniWriteValue("Server", "Add to Startup", cb_startup.Checked.ToString());
            ini.IniWriteValue("Server", "Allow Anonymous Users", cb_anonUsers.Checked.ToString());
            ini.IniWriteValue("Network", "Port", nud_port.Value.ToString());
            ini.IniWriteValue("Logging", "Parse Logs", cb_parseLogs.Checked.ToString());
            ini.IniWriteValue("Logging", "Log Connection", cb_logCon.Checked.ToString());
            ini.IniWriteValue("Logging", "Log Client Crash", cb_logCrash.Checked.ToString());
            ini.IniWriteValue("MySQL", "Server Address", tb_serverAddr.Text);
            ini.IniWriteValue("MySQL", "Database Name", tb_dbName.Text);
            ini.IniWriteValue("MySQL", "Database Username", tb_dbUser.Text);
            ini.IniWriteValue("MySQL", "Database Password", tb_dbPass.Text);
        }
    }
}
