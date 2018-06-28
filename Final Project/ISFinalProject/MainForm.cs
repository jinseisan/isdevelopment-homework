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
using System.Configuration;

namespace ISFinalProject
{

    public partial class Main : Form
    {
        protected static DataSet savedata = new DataSet();//保存数据库读取的DataSet为临时变量
        protected static Thread processThread ;//数据处理线程对象
        private int pageSize = 10;//每页显示条目数
        private int totalCount = 0;//该次检索的总条目数
        private int currentPage = 1;//当前页号
        private int pageCount = 1;//总页数
        private string db_sql = ConfigurationManager.AppSettings["Table_SQL"].ToString();//sql表名称
        //private string db_ref = "cssci2014_ref";//ref表名称
        

        public Main()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; 
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //时间状态栏信息初始化
            this.timeStatusLable.Text = "当前时间： " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            dateTimer.Start();
            this.operStatusLabel.Text = tabControl1.TabPages[0].Text;
            //数据处理线程初始化
            Initial_Thread();
            //初始化每页显示选项框条目
            this.pageSizeComboBox.SelectedIndex = 0;
            //数据哭连接信息显示
            initial_SQLInfo();
        }
        //ConnectString信息初始化
        private void initial_SQLInfo()
        {
            SQLDataSourceBox.Text = DataAccess.DataSource;
            SQLInitialCatalogBox.Text = DataAccess.InitialCatalog;
            SQLUserIDBox.Text = DataAccess.UserID;
            SQLPasswordBox.Text = DataAccess.Password;

            SQLDataSourceBox.Enabled = false;
            SQLInitialCatalogBox.Enabled = false;
            SQLUserIDBox.Enabled = false;
            SQLPasswordBox.Enabled = false;

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

            if(this.modelcButton.Checked)
            {
                method.set_model("modelc");
            }
            else
            {
                method.set_model("modelg");
            }
            method.set_processString(this.inputTextBox.Text.ToString().Trim());

            processThread = new Thread(new ThreadStart(method.runMethod));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.progressBar1.Value = 0;
            Initial_Thread();
            processThread.Start();
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
            currentPage = 1;
            DataBind();
        }
        //数据绑定
        private void DataBind()
        {
            //判断已有页数
            if(currentPage == 1)
            {
                this.MoveFirstPageItem.Enabled = false;
                this.MovePreviousPageItem.Enabled = false;
                this.MoveLastPageItem.Enabled = true;
                this.MoveNextPageItem.Enabled = true;         
            }
            else if (currentPage == pageCount)
            {
                this.MoveFirstPageItem.Enabled = true;
                this.MovePreviousPageItem.Enabled = true;
                this.MoveLastPageItem.Enabled = false;
                this.MoveNextPageItem.Enabled = false;
            }
            else
            {
                this.MoveFirstPageItem.Enabled = true;
                this.MovePreviousPageItem.Enabled = true;
                this.MoveLastPageItem.Enabled = true;
                this.MoveNextPageItem.Enabled = true;
            }
            //设置查询条件
            string ti = this.searchInputBox.Text.Trim();
            string cond = "";
            if (ti != "") { cond = "来源篇名 like '%" + ti + "%'"; }

            //查询数据获取和保存
            savedata = DataAccess.sql_prcPageResult(currentPage, "*", db_sql, cond, "文件序号", 0, "文件序号", pageSize);
            totalCount = DataAccess.sql_prcRowsCount(db_sql, "*", cond);

            DataTable table = savedata.Tables[0].DefaultView.ToTable(false, new string[] {"文件序号", "来源篇名", "来源作者", "文章类型" });
            BindingSource bs = new BindingSource();
            bs.DataSource = table;

            //控件绑定
            this.dataGridView1.DataSource = bs;
            this.bindingNavigator1.BindingSource = bs;
            //dataGridView1参数设置
            this.dataGridView1.Columns["文件序号"].Visible = false;
            this.dataGridView1.Columns["来源篇名"].FillWeight = 70;      //第一列的相对宽度为70%
            this.dataGridView1.Columns["来源作者"].FillWeight = 15;      //第二列的相对宽度为15%
            this.dataGridView1.Columns["文章类型"].FillWeight = 15;      //第三列的相对宽度为15%
            this.dataGridView1.RowHeadersWidth = 40 + (currentPage*pageSize).ToString().Length * 5;
            //控件信息参数设置
            this.currentPageNumBox.Text = currentPage.ToString();
            //计算页数
            if (totalCount % pageSize == 0) { pageCount = totalCount / pageSize; }
            else { pageCount = totalCount / pageSize + 1; }
            this.pageInfoLabel.Text = String.Format("共 {0}页；共 {1}条记录",pageCount,totalCount) ;
            //修改页数相关参数
            if (pageCount == 1)
            {
                this.MoveLastPageItem.Enabled = false;
                this.MoveNextPageItem.Enabled = false;
            }
        }

        private void refreshInfo()
        {
            string otherinfo = "";
            if (this.dataGridView1.SelectedCells.Count > 1) return;
            int index = this.dataGridView1.CurrentCell.RowIndex;
            if (index >= 0)
            {
                otherinfo += "文件序号：" + savedata.Tables[0].Rows[index]["文件序号"].ToString().Trim() + "\r\n";
                otherinfo += "中文篇名：" + savedata.Tables[0].Rows[index]["来源篇名"].ToString().Trim() + "\r\n";
                otherinfo += "英文篇名：" + savedata.Tables[0].Rows[index]["英文篇名"].ToString().Trim() + "\r\n";
                otherinfo += "来源作者：" + savedata.Tables[0].Rows[index]["来源作者"].ToString().Trim() + "\r\n";
                otherinfo += "文章类型：" + savedata.Tables[0].Rows[index]["文章类型"].ToString().Trim() + "\r\n";
                otherinfo += "期刊：" + savedata.Tables[0].Rows[index]["期刊"].ToString().Trim() + "\n";
                otherinfo += "第一机构：" + savedata.Tables[0].Rows[index]["第一机构"].ToString().Trim() + "\r\n";
                otherinfo += "机构名称：" + savedata.Tables[0].Rows[index]["机构名称"].ToString().Trim() + "\r\n";
                otherinfo += "学科分类：" + savedata.Tables[0].Rows[index]["学科分类"].ToString().Trim() + "\r\n";
                otherinfo += "第一作者：" + savedata.Tables[0].Rows[index]["第一作者"].ToString().Trim() + "\r\n";
                otherinfo += "中图类号：" + savedata.Tables[0].Rows[index]["中图类号"].ToString().Trim() + "\r\n";
                otherinfo += "年代卷期：" + savedata.Tables[0].Rows[index]["年代卷期"].ToString().Trim() + "\r\n";
                otherinfo += "关键词：" + savedata.Tables[0].Rows[index]["关键词"].ToString().Trim().Replace('/', ';') + "\r\n";
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

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //显示在HeaderCell上
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = this.dataGridView1.Rows[i];
                r.HeaderCell.Value = string.Format("{0}", (currentPage - 1) * pageSize + i + 1);
            }
            this.dataGridView1.Refresh();
        }

        private void pageSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int oldSize = pageSize;//之前的页面设置
            int rowCount = (currentPage - 1) * oldSize + 1;//选中行的行数（默认第1行）
            int row = 1;//选中行在该页的行数（默认第一行）
            pageSize = Convert.ToInt32(this.pageSizeComboBox.SelectedItem.ToString());
            //自动跳转到更改每页显示数后的页数和记录
            if (this.dataGridView1.SelectedCells.Count == 1)
            {
                rowCount += this.dataGridView1.CurrentCell.RowIndex;
            }
            if (rowCount % pageSize == 0) 
            { 
                currentPage = rowCount / pageSize;
                row = pageSize;
            }
            else
            {
                if (rowCount <= pageSize)
                {
                    currentPage = 1;
                    row = rowCount;
                }
                else
                {
                    currentPage = rowCount / pageSize + 1;
                    row = rowCount % pageSize;
                }
            }
            DataBind();
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[row - 1].Cells[1];
            this.searchInputBox.Focus();
        }
        
        private void MoveFirstPageItem_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            DataBind();
        }

        private void MovePreviousPageItem_Click(object sender, EventArgs e)
        {
            currentPage--;
            DataBind();
        }

        private void MoveNextPageItem_Click(object sender, EventArgs e)
        {            
            currentPage++;
            DataBind();
        }

        private void MoveLastPageItem_Click(object sender, EventArgs e)
        {
            currentPage = pageCount;
            DataBind();
        }

        private void goToPositionLabel_Click(object sender, EventArgs e)
        {
            string pos_str = this.inputPosBox.Text.ToString().Trim();
            if (pos_str == "") return;
            int pos = Convert.ToInt32(pos_str);
            //检查输入数字大小是否越界
            if(pos < 1)
            {
                pos = 1;//自动转到首页
            }
            else if (pos > pageCount)
            {
                pos = pageCount;//自动转到尾页
            }
            this.inputPosBox.Text = pos.ToString();
            currentPage = pos;
            DataBind();

        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            refreshInfo();
        }

        private void SQLConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                string DS = SQLDataSourceBox.Text.ToString().Trim();
                string IC = SQLInitialCatalogBox.Text.ToString().Trim();
                string UID = SQLUserIDBox.Text.ToString().Trim();
                string PSW = SQLPasswordBox.Text.ToString().Trim();
                if(DS == "" || IC == "" || UID == "" || PSW == "")
                {
                    MessageBox.Show("输入不完整，请输入完整数据库连接信息！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataAccess.set_SQL(DS,IC,UID,PSW);
                initial_SQLInfo();
                DataBind();
                searchButton.Enabled = true;
                MessageBox.Show("数据库连接成功", "成功！", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception f)
            {
                SQLDataSourceBox.Enabled = true;
                SQLInitialCatalogBox.Enabled = true;
                SQLUserIDBox.Enabled = true;
                SQLPasswordBox.Enabled = true;
                MessageBox.Show(f.Message, "数据库连接出错！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SQLDefaultButton_Click(object sender, EventArgs e)
        {
            DataAccess.initial_SQL();
            initial_SQLInfo();
        }

        private void SQLModifyButton_Click(object sender, EventArgs e)
        {
            SQLDataSourceBox.Enabled = true;
            SQLInitialCatalogBox.Enabled = true;
            SQLUserIDBox.Enabled = true;
            SQLPasswordBox.Enabled = true;
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            string info = "编译时间：" + System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString() + "\n作者：达婧玮、高清琪、牟星宇";
            MessageBox.Show(info, "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    } 
}
