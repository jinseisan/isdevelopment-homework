using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

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
                    outputTextBox.Text = DataProcess.ParagraphProcess(str);
                    
                }
                catch (Exception f) 
                {
                    MessageBox.Show(f.Message);
                }
                
            }
            /*
            try
            {
                outputTextBox.Text = DataProcess.ParagraphProcess(DataProcess.MainProcess());
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }*/
            //DataProcess.NLPIRProcess();
            //DataProcess.ClassifyProcess();
            //DataProcess.LevelProcess();
            //DataProcess.FSProcess();
            //DataProcess.CRFSProcess("modelc");
            ArrayList keyWords = DataProcess.GetKeyWords();
            string keywords = "";
            for (int i = 0; i < keyWords.Count; i++)
            {
                keywords += keyWords[i].ToString() + ";";
            }
            outputTextBox.Text = keywords;


            
        }

        private void dateTimer_Tick(object sender, EventArgs e)
        {
            this.timeStatusLable.Text = "当前时间： "+DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.operStatusLabel.Text = tabControl1.TabPages[tabControl1.SelectedIndex].Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog.FileName, Encoding.Default);
                inputTextBox.Text = sr.ReadToEnd();
                sr.Close();
                this.operStatusLabel.Text = "打开成功";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.inputTextBox.Text.ToString().Trim() == "")
            {
                MessageBox.Show("内容为空！", "警告");
                return;
            }
            SaveFileDialog MySaveFileDialog = new SaveFileDialog();
            MySaveFileDialog.Filter = "文本文档(*.txt)|*.txt";
            MySaveFileDialog.CreatePrompt = true;
            MySaveFileDialog.OverwritePrompt = true;
            MySaveFileDialog.RestoreDirectory = true;
            if (MySaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(MySaveFileDialog.FileName, false);
                sw.WriteLine(this.inputTextBox.Text);
                sw.Close();
                this.operStatusLabel.Text = "保存成功";
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage2;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Text = "";
            outputTextBox.Text = "";
        }

        private void outputCopyButton_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Copy();
            this.operStatusLabel.Text = "复制成功";
        }

        private void outputSaveButton_Click(object sender, EventArgs e)
        {
            if (outputTextBox.Text == "")
            {
                MessageBox.Show("内容为空！", "警告");
                return;
            }
            SaveFileDialog MySaveFileDialog = new SaveFileDialog();
            MySaveFileDialog.Filter = "文本文档(*.txt)|*.txt";
            MySaveFileDialog.CreatePrompt = true;
            MySaveFileDialog.OverwritePrompt = true;
            MySaveFileDialog.RestoreDirectory = true;
            if (MySaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(MySaveFileDialog.FileName, false);
                sw.WriteLine(this.outputTextBox.Text);
                sw.Close();
                this.operStatusLabel.Text = "保存成功";
            }
        }

        private void inputCopyButton_Click(object sender, EventArgs e)
        {
            this.inputTextBox.Copy();
            this.operStatusLabel.Text = "复制成功";
        }
    }
}
