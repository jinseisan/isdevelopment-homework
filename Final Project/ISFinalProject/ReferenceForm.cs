using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISFinalProject
{
    public partial class ReferenceForm : Form
    {
        protected static DataSet savedata = new DataSet();//保存数据库读取的DataSet为临时变量
        protected static Thread processThread;//数据处理线程对象
        private int pageSize = 10;//每页显示条目数
        private int totalCount = 0;//该次检索的总条目数
        private int currentPage = 1;//当前页号
        private int pageCount = 1;//总页数
        private string db_ref = ConfigurationManager.AppSettings["Table_REF"].ToString();//ref表名称
        private string db_sql = ConfigurationManager.AppSettings["Table_SQL"].ToString();//sql表名称

        private string fileNum = "";//文件序号

        public ReferenceForm()
        {
            InitializeComponent();
        }

        public ReferenceForm(string fileNum)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; 
            InitializeComponent();
            this.fileNum = fileNum.Trim();
        }

        private void ReferenceForm_Load(object sender, EventArgs e)
        {
            
            string sql = "select 来源篇名,关键词 from " + db_sql + " where 文件序号='" + fileNum.Trim()+"';";
            DataSet ds = DataAccess.Select_SQL(sql);
            this.fileNumBox.Text = ds.Tables[0].Rows[0]["来源篇名"].ToString().Trim();
            this.keyWordsBox.Text = ds.Tables[0].Rows[0]["关键词"].ToString().Trim().Replace('/', '；');
            //初始化每页显示选项框条目
            this.pageSizeComboBox.SelectedIndex = 0;
            //数据绑定
            DataBind();
            
            
        }

        private void Initial_Thread(string processData)
        {
            if (processThread == null) { }
            else if (processThread.IsAlive) { processThread.Abort(); }
            //将线程绑定
            ThreadMethod method = new ThreadMethod();
            //先订阅一下事件  
            method.threadStartEvent += new EventHandler(method_threadStartEvent);
            method.threadEvent += new EventHandler(method_threadEvent);
            method.threadEndEvent += new EventHandler(method_threadEndEvent);
            method.threadStatusEvent += new EventHandler(method_threadStatusEvent);

            if (this.modelcButton.Checked)
            {
                method.set_model("modelc");
            }
            else
            {
                method.set_model("modelg");
            }
            method.set_processString(processData);

            processThread = new Thread(new ThreadStart(method.runMethod));
        }
        //ProgressBar相关设定

        //线程开始的时候调用的委托  
        private delegate void maxValueDelegate(int maxValue);
        //线程执行中调用的委托  
        private delegate void nowValueDelegate(int nowValue);
        //线程执行中调用的委托  
        private delegate void outputValueDelegate(string keywords);
        //线程执行中调用的委托：显示状态信息
        private delegate void nowStatusDelegate(string keywords);

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
        void method_threadStatusEvent(object sender, EventArgs e)
        {
            string nowStatus = Convert.ToString(sender);
            nowStatusDelegate status = new nowStatusDelegate(setStatusLabel);
            this.Invoke(status, nowStatus);
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
        /// <summary>  
        /// 我被委托调用,专门设置状态文本框当前值的  
        /// </summary>  
        /// <param name="keywords"></param>  
        private void setStatusLabel(string status)
        {
            this.operStatusLabel.Text = status;
        }
        private void DataBind()
        {
            //判断已有页数
            if (currentPage == 1)
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
            
            string cond = "文件序号='"+fileNum+"'";
        
            //查询数据获取和保存
            savedata = DataAccess.sql_prcPageResult(currentPage, "*", db_ref, cond, "id", 0, "id", pageSize);
            totalCount = DataAccess.sql_prcRowsCount(db_ref, "*", cond);

            DataTable table = savedata.Tables[0].DefaultView.ToTable(false, new string[] { "文件序号", "篇名"});
            BindingSource bs = new BindingSource();
            bs.DataSource = table;

            //控件绑定
            this.dataGridView1.DataSource = bs;
            this.bindingNavigator1.BindingSource = bs;
            //dataGridView1参数设置
            this.dataGridView1.Columns["文件序号"].Visible = false;
            
            this.dataGridView1.RowHeadersWidth = 40 + (currentPage * pageSize).ToString().Length * 5;
            //控件信息参数设置
            this.currentPageNumBox.Text = currentPage.ToString();
            //计算页数
            if (totalCount % pageSize == 0) { pageCount = totalCount / pageSize; }
            else { pageCount = totalCount / pageSize + 1; }
            this.pageInfoLabel.Text = String.Format("共 {0}页；共 {1}条记录", pageCount, totalCount);
            //修改页数相关参数
            if (pageCount == 1 || pageCount == 0)
            {
                this.MoveFirstPageItem.Enabled = false;
                this.MovePreviousPageItem.Enabled = false;
                this.MoveLastPageItem.Enabled = false;
                this.MoveNextPageItem.Enabled = false;
            }
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

        private void ExteactButton_Click(object sender, EventArgs e)
        {
            string sql = "select * from " + db_ref + " where 文件序号='" + fileNum.Trim() + "'";
            savedata = DataAccess.Select_SQL(sql);

            if (totalCount == 0)
            {
                MessageBox.Show("数据库中未保存该文献的参考文献信息！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string processData = "";
            for (int i = 0; i < savedata.Tables[0].Rows.Count; i++)
            {
                processData += savedata.Tables[0].Rows[i]["篇名"].ToString().Trim()+'\n';
            }
            Initial_Thread(processData);
            this.progressBar1.Value = 0;
            processThread.Start();
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
            //this.dataGridView1.CurrentCell = this.dataGridView1.Rows[row - 1].Cells[1];
            
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
            if (pos < 1)
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

    }
}
