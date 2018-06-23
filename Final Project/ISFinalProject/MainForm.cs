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
using System.Threading;

namespace ISFinalProject
{

    public partial class Main : Form
    {
        public static string inputstr = "";
        protected static DataSet savedata = new DataSet();//保存数据库读取的DataSet为临时变量
        protected static Thread processThread ;//数据处理线程对象
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.timeStatusLable.Text = "当前时间： " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            dateTimer.Start();
            this.operStatusLabel.Text = tabControl1.TabPages[0].Text;
            button3_Click(null, EventArgs.Empty);
            Initial_Thread();

        }
        private void Initial_Thread()
        {
            if (processThread==null){}
            else if(processThread.IsAlive) { processThread.Abort(); }
            //将线程绑定
            ThreadMethod method = new ThreadMethod();
            //先订阅一下事件  
            method.threadStartEvent += new EventHandler(method_threadStartEvent);
            method.threadEvent += new EventHandler(method_threadEvent);
            method.threadEndEvent += new EventHandler(method_threadEndEvent);

            processThread = new Thread(new ThreadStart(method.runMethod));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.progressBar1.Value = 0;
            Initial_Thread();
            processThread.Start();
            /*
            try
            {
                Initial_Thread();
                processThread.Start();
            }
            catch(Exception f)
            {
                MessageBox.Show(f.Message);
            }*/
              
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
       
        private void button3_Click(object sender, EventArgs e)
        {
            string ti = this.textBox1.Text;
            savedata =DataAccess.GetTitleByTi(ti);
            DataTable table = savedata.Tables[0].DefaultView .ToTable (false,new string[]{"来源篇名","来源作者","第一机构","年代卷期","文章类型"});
            DataView dv = new DataView(table);
            this.dataGridView1.DataSource =dv;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string otherinfo="";
            if (e.RowIndex >= 0)
            {
                otherinfo +="文件序号："+ savedata.Tables[0].Rows[e.RowIndex]["文件序号"].ToString ().Trim ()+"\r\n";
                otherinfo += "英文篇名：" + savedata.Tables[0].Rows[e.RowIndex]["英文篇名"].ToString().Trim() + "\r\n";
                otherinfo += "来源作者：" + savedata.Tables[0].Rows[e.RowIndex]["来源作者"].ToString().Trim() + "\r\n";
                otherinfo +="期刊："+ savedata.Tables[0].Rows[e.RowIndex]["期刊"].ToString().Trim() + "\n";
                otherinfo +="机构名称："+ savedata.Tables[0].Rows[e.RowIndex]["机构名称"].ToString().Trim() + "\r\n";
                otherinfo += "学科分类：" + savedata.Tables[0].Rows[e.RowIndex]["学科分类"].ToString().Trim() + "\r\n";
                otherinfo +="第一作者："+ savedata.Tables[0].Rows[e.RowIndex]["第一作者"].ToString().Trim() + "\r\n";
                otherinfo += "中图类号：" + savedata.Tables[0].Rows[e.RowIndex]["中图类号"].ToString().Trim() + "\r\n";
                otherinfo += "关键词：" + savedata.Tables[0].Rows[e.RowIndex]["关键词"].ToString().Trim().Replace('/', ';') + "\r\n";
                this.textBox2.Text = otherinfo;
            }
        }

        //ProgressBar相关设定

        //线程开始的时候调用的委托  
        private delegate void maxValueDelegate(int maxValue);
        //线程执行中调用的委托  
        private delegate void nowValueDelegate(int nowValue);
        //线程执行中调用的委托  
        private delegate void outputValueDelegate(string keywords);

        /// <summary>  
        /// 线程开始事件,设置进度条最大值  
        /// 但是我不能直接操作进度条,需要一个委托来替我完成  
        /// </summary>  
        /// <param name="sender">ThreadMethod函数中传过来的最大值</param>  
        /// <param name="e"></param>  
        void method_threadStartEvent(object sender, EventArgs e)
        {
            int maxValue = Convert.ToInt32(sender);
            maxValueDelegate max = new maxValueDelegate(setMax);
            this.Invoke(max, maxValue);
        }

        /// <summary>  
        /// 线程执行中的事件,设置进度条当前进度  
        /// 但是我不能直接操作进度条,需要一个委托来替我完成  
        /// </summary>  
        /// <param name="sender">ThreadMethod函数中传过来的当前值</param>  
        /// <param name="e"></param>  
        void method_threadEvent(object sender, EventArgs e)
        {
            int nowValue = Convert.ToInt32(sender);
            nowValueDelegate now = new nowValueDelegate(setNow);
            this.Invoke(now, nowValue);
        }

        /// <summary>  
        /// 线程完成事件  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        void method_threadEndEvent(object sender, EventArgs e)
        {
            string keywords = Convert.ToString(sender);
            outputValueDelegate output = new outputValueDelegate(setKeywords);
            this.Invoke(output, keywords);
            MessageBox.Show("执行已经完成!");
        }

        /// <summary>  
        /// 我被委托调用,专门设置进度条最大值的  
        /// </summary>  
        /// <param name="maxValue"></param>  
        private void setMax(int maxValue)
        {
            this.progressBar1.Maximum = maxValue;
        }

        /// <summary>  
        /// 我被委托调用,专门设置进度条当前值的  
        /// </summary>  
        /// <param name="nowValue"></param>  
        private void setNow(int nowValue)
        {
            this.progressBar1.Value = nowValue;
        }
        /// <summary>  
        /// 我被委托调用,专门设置关键词文本框当前值的  
        /// </summary>  
        /// <param name="keywords"></param>  
        private void setKeywords(string keywords)
        {
            this.outputTextBox.Text = keywords; 
        }

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            inputstr = inputTextBox.Text.ToString().Trim();
        } 
 

    }

    //执行数据处理的线程类
    public class ThreadMethod
    {
        /// <summary>  
        /// 线程开始事件  
        /// </summary>  
        public event EventHandler threadStartEvent;
        /// <summary>  
        /// 线程执行时事件  
        /// </summary>  
        public event EventHandler threadEvent;
        /// <summary>  
        /// 线程结束事件  
        /// </summary>  
        public event EventHandler threadEndEvent;

        public void runMethod()
        {
            int count = 350;      //执行多少次  
            threadStartEvent.Invoke(count, new EventArgs());//通知主界面,我开始了,count用来设置进度条的最大值  
            string str = Main.inputstr;
            if (str != "")
            {
                //try
                {
                    DataProcess.ParagraphProcess(str);
                    threadEvent.Invoke(50, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度  
                    DataProcess.NLPIRProcess();
                    threadEvent.Invoke(100, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度  
                    DataProcess.ClassifyProcess();
                    threadEvent.Invoke(150, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度  
                    DataProcess.LevelProcess();
                    threadEvent.Invoke(200, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度  
                    DataProcess.FSProcess();
                    threadEvent.Invoke(250, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度  
                    DataProcess.CRFSProcess("modelc");
                    threadEvent.Invoke(300, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度
                    ArrayList keyWords = DataProcess.GetKeyWords();
                    string keywords = "";
                    for (int i = 0; i < keyWords.Count; i++)
                    {
                        keywords += keyWords[i].ToString() + ";";
                        threadEvent.Invoke(300 + (Convert.ToDouble(i) / keyWords.Count)*50, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度
                    }

                    threadEndEvent.Invoke(keywords, new EventArgs());//通知主界面我已经完成了           
                }
                //catch (Exception f)
                {
                //    MessageBox.Show(f.StackTrace);
                }
            }
        }
    }  
}
