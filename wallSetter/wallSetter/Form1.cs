using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Text;
using System.IO;

namespace sampleWall
{
    public partial class Form1 : Form
    {
        String myPath = System.Windows.Forms.Application.StartupPath;

        const uint SPI_SETDESKWALLPAPER = 20;
        const uint SPIF_UPDATEINIFILE = 1;
        const uint SPIF_SENDWININICHANGE = 2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, StringBuilder pvParam, uint fWinIni);


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setWall();
        }

        private void setWall()
        {
            StringBuilder sb = new StringBuilder(myPath + "\\wallpaper.png");

            SystemParametersInfo(SPI_SETDESKWALLPAPER, (uint)sb.Length, sb, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(myPath+"\\auto.txt"))
            {
                setWall();
                Application.Exit();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chkAutoChange.Checked)
            {
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                StreamWriter writer =
                  new StreamWriter(myPath + "\\auto.txt", true, sjisEnc);
                writer.WriteLine("このファイルが存在すると、自動的に壁紙を設定して終了します。\r\n"+
                "自動で壁紙を設定させたくないときは、このファイルを削除してください。");
                writer.Close();
            }
        }
    }
}
