using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISFinalProject
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.timeStatusLable.Text = "当前时间： " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            dateTimer.Start();
            this.operStatusLabel.Text = tabControl1.TabPages[0].Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = inputTextBox.Text.ToString().Trim();
            if (str != "")
            {
                try
                {
                    outputTextBox.Text = NLPIRTool.ParagraphProcess(str);
                }
                catch (Exception f) 
                {
                    MessageBox.Show(f.Message);
                }
                
            }
        }

        private void dateTimer_Tick(object sender, EventArgs e)
        {
            this.timeStatusLable.Text = "当前时间： "+DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.operStatusLabel.Text = tabControl1.TabPages[tabControl1.SelectedIndex].Text;
        }
    }
}
