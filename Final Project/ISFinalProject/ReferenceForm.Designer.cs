namespace ISFinalProject
{
    partial class ReferenceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReferenceForm));
            this.label1 = new System.Windows.Forms.Label();
            this.fileNumBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.keyWordsBox = new System.Windows.Forms.TextBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ExteactButton = new System.Windows.Forms.Button();
            this.pagePanel = new System.Windows.Forms.Panel();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.pageSizeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MoveFirstPageItem = new System.Windows.Forms.ToolStripButton();
            this.MovePreviousPageItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.currentPageNumBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MoveNextPageItem = new System.Windows.Forms.ToolStripButton();
            this.MoveLastPageItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pageInfoLabel = new System.Windows.Forms.ToolStripLabel();
            this.goToPositionLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.inputPosBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.modelgButton = new System.Windows.Forms.RadioButton();
            this.modelcButton = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.operStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(18, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "来源文献:";
            // 
            // fileNumBox
            // 
            this.fileNumBox.Font = new System.Drawing.Font("宋体", 12F);
            this.fileNumBox.Location = new System.Drawing.Point(123, 15);
            this.fileNumBox.Multiline = true;
            this.fileNumBox.Name = "fileNumBox";
            this.fileNumBox.ReadOnly = true;
            this.fileNumBox.Size = new System.Drawing.Size(429, 40);
            this.fileNumBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(18, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "引用文献:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(22, 105);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(650, 252);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridView1_RowStateChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(12, 409);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "来源文献关键词:";
            // 
            // keyWordsBox
            // 
            this.keyWordsBox.Font = new System.Drawing.Font("宋体", 10F);
            this.keyWordsBox.Location = new System.Drawing.Point(15, 428);
            this.keyWordsBox.Multiline = true;
            this.keyWordsBox.Name = "keyWordsBox";
            this.keyWordsBox.ReadOnly = true;
            this.keyWordsBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.keyWordsBox.Size = new System.Drawing.Size(650, 79);
            this.keyWordsBox.TabIndex = 5;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Font = new System.Drawing.Font("宋体", 10F);
            this.outputTextBox.Location = new System.Drawing.Point(12, 546);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTextBox.Size = new System.Drawing.Size(650, 84);
            this.outputTextBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(12, 519);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "引文文献提取关键词:";
            // 
            // ExteactButton
            // 
            this.ExteactButton.Font = new System.Drawing.Font("宋体", 12F);
            this.ExteactButton.Location = new System.Drawing.Point(569, 15);
            this.ExteactButton.Name = "ExteactButton";
            this.ExteactButton.Size = new System.Drawing.Size(107, 26);
            this.ExteactButton.TabIndex = 8;
            this.ExteactButton.Text = "提取关键词";
            this.ExteactButton.UseVisualStyleBackColor = true;
            this.ExteactButton.Click += new System.EventHandler(this.ExteactButton_Click);
            // 
            // pagePanel
            // 
            this.pagePanel.Controls.Add(this.bindingNavigator1);
            this.pagePanel.Location = new System.Drawing.Point(22, 360);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Size = new System.Drawing.Size(650, 32);
            this.pagePanel.TabIndex = 9;
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.CountItem = null;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.pageSizeComboBox,
            this.toolStripSeparator1,
            this.MoveFirstPageItem,
            this.MovePreviousPageItem,
            this.bindingNavigatorSeparator,
            this.toolStripLabel2,
            this.currentPageNumBox,
            this.toolStripLabel3,
            this.bindingNavigatorSeparator1,
            this.MoveNextPageItem,
            this.MoveLastPageItem,
            this.bindingNavigatorSeparator2,
            this.pageInfoLabel,
            this.goToPositionLabel,
            this.toolStripLabel4,
            this.inputPosBox,
            this.toolStripSeparator2});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = null;
            this.bindingNavigator1.MoveLastItem = null;
            this.bindingNavigator1.MoveNextItem = null;
            this.bindingNavigator1.MovePreviousItem = null;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = null;
            this.bindingNavigator1.Size = new System.Drawing.Size(650, 25);
            this.bindingNavigator1.TabIndex = 0;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "每页显示";
            // 
            // pageSizeComboBox
            // 
            this.pageSizeComboBox.Items.AddRange(new object[] {
            "10",
            "20",
            "50"});
            this.pageSizeComboBox.Name = "pageSizeComboBox";
            this.pageSizeComboBox.Size = new System.Drawing.Size(75, 25);
            this.pageSizeComboBox.ToolTipText = "选择每页显示条目数";
            this.pageSizeComboBox.SelectedIndexChanged += new System.EventHandler(this.pageSizeComboBox_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // MoveFirstPageItem
            // 
            this.MoveFirstPageItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveFirstPageItem.Image = ((System.Drawing.Image)(resources.GetObject("MoveFirstPageItem.Image")));
            this.MoveFirstPageItem.Name = "MoveFirstPageItem";
            this.MoveFirstPageItem.RightToLeftAutoMirrorImage = true;
            this.MoveFirstPageItem.Size = new System.Drawing.Size(23, 22);
            this.MoveFirstPageItem.Text = "移到第一条记录";
            this.MoveFirstPageItem.ToolTipText = "移动到第一页";
            this.MoveFirstPageItem.Click += new System.EventHandler(this.MoveFirstPageItem_Click);
            // 
            // MovePreviousPageItem
            // 
            this.MovePreviousPageItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MovePreviousPageItem.Image = ((System.Drawing.Image)(resources.GetObject("MovePreviousPageItem.Image")));
            this.MovePreviousPageItem.Name = "MovePreviousPageItem";
            this.MovePreviousPageItem.RightToLeftAutoMirrorImage = true;
            this.MovePreviousPageItem.Size = new System.Drawing.Size(23, 22);
            this.MovePreviousPageItem.Text = "移到上一条记录";
            this.MovePreviousPageItem.ToolTipText = "移动到上一页";
            this.MovePreviousPageItem.Click += new System.EventHandler(this.MovePreviousPageItem_Click);
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(20, 22);
            this.toolStripLabel2.Text = "第";
            // 
            // currentPageNumBox
            // 
            this.currentPageNumBox.AccessibleName = "位置";
            this.currentPageNumBox.AutoSize = false;
            this.currentPageNumBox.Enabled = false;
            this.currentPageNumBox.Name = "currentPageNumBox";
            this.currentPageNumBox.Size = new System.Drawing.Size(45, 23);
            this.currentPageNumBox.Text = "1";
            this.currentPageNumBox.ToolTipText = "当前位置";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(20, 22);
            this.toolStripLabel3.Text = "页";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // MoveNextPageItem
            // 
            this.MoveNextPageItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveNextPageItem.Image = ((System.Drawing.Image)(resources.GetObject("MoveNextPageItem.Image")));
            this.MoveNextPageItem.Name = "MoveNextPageItem";
            this.MoveNextPageItem.RightToLeftAutoMirrorImage = true;
            this.MoveNextPageItem.Size = new System.Drawing.Size(23, 22);
            this.MoveNextPageItem.Text = "移到下一条记录";
            this.MoveNextPageItem.ToolTipText = "移动到下一页";
            this.MoveNextPageItem.Click += new System.EventHandler(this.MoveNextPageItem_Click);
            // 
            // MoveLastPageItem
            // 
            this.MoveLastPageItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveLastPageItem.Image = ((System.Drawing.Image)(resources.GetObject("MoveLastPageItem.Image")));
            this.MoveLastPageItem.Name = "MoveLastPageItem";
            this.MoveLastPageItem.RightToLeftAutoMirrorImage = true;
            this.MoveLastPageItem.Size = new System.Drawing.Size(23, 22);
            this.MoveLastPageItem.Text = "移到最后一条记录";
            this.MoveLastPageItem.ToolTipText = "移动到最后一页";
            this.MoveLastPageItem.Click += new System.EventHandler(this.MoveLastPageItem_Click);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // pageInfoLabel
            // 
            this.pageInfoLabel.Name = "pageInfoLabel";
            this.pageInfoLabel.Size = new System.Drawing.Size(122, 22);
            this.pageInfoLabel.Text = "共{0}页，共{1}条记录";
            // 
            // goToPositionLabel
            // 
            this.goToPositionLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.goToPositionLabel.IsLink = true;
            this.goToPositionLabel.Name = "goToPositionLabel";
            this.goToPositionLabel.Size = new System.Drawing.Size(32, 22);
            this.goToPositionLabel.Text = "转到";
            this.goToPositionLabel.ToolTipText = "转到跳转页";
            this.goToPositionLabel.Click += new System.EventHandler(this.goToPositionLabel_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(20, 22);
            this.toolStripLabel4.Text = "页";
            // 
            // inputPosBox
            // 
            this.inputPosBox.AccessibleName = "位置";
            this.inputPosBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.inputPosBox.AutoSize = false;
            this.inputPosBox.Name = "inputPosBox";
            this.inputPosBox.Size = new System.Drawing.Size(45, 23);
            this.inputPosBox.Text = "1";
            this.inputPosBox.ToolTipText = "跳转页数";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.modelgButton);
            this.panel2.Controls.Add(this.modelcButton);
            this.panel2.Location = new System.Drawing.Point(558, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(133, 43);
            this.panel2.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "模式选择:";
            // 
            // modelgButton
            // 
            this.modelgButton.AutoSize = true;
            this.modelgButton.Location = new System.Drawing.Point(69, 19);
            this.modelgButton.Name = "modelgButton";
            this.modelgButton.Size = new System.Drawing.Size(59, 16);
            this.modelgButton.TabIndex = 1;
            this.modelgButton.Text = "modelg";
            this.modelgButton.UseVisualStyleBackColor = true;
            // 
            // modelcButton
            // 
            this.modelcButton.AutoSize = true;
            this.modelcButton.Checked = true;
            this.modelcButton.Location = new System.Drawing.Point(5, 19);
            this.modelcButton.Name = "modelcButton";
            this.modelcButton.Size = new System.Drawing.Size(59, 16);
            this.modelcButton.TabIndex = 0;
            this.modelcButton.TabStop = true;
            this.modelcButton.Text = "modelc";
            this.modelcButton.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operStatusLabel,
            this.progressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 640);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(692, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // operStatusLabel
            // 
            this.operStatusLabel.Name = "operStatusLabel";
            this.operStatusLabel.Size = new System.Drawing.Size(375, 17);
            this.operStatusLabel.Spring = true;
            this.operStatusLabel.Text = "参考文献信息";
            // 
            // progressBar1
            // 
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(300, 16);
            // 
            // ReferenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 662);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.ExteactButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.keyWordsBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fileNumBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ReferenceForm";
            this.Text = "引文文献信息";
            this.Load += new System.EventHandler(this.ReferenceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pagePanel.ResumeLayout(false);
            this.pagePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fileNumBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox keyWordsBox;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ExteactButton;
        private System.Windows.Forms.Panel pagePanel;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton MoveFirstPageItem;
        private System.Windows.Forms.ToolStripButton MovePreviousPageItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox currentPageNumBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton MoveNextPageItem;
        private System.Windows.Forms.ToolStripButton MoveLastPageItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripLabel pageInfoLabel;
        private System.Windows.Forms.ToolStripLabel goToPositionLabel;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox inputPosBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton modelgButton;
        private System.Windows.Forms.RadioButton modelcButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel operStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar progressBar1;
        private System.Windows.Forms.ToolStripComboBox pageSizeComboBox;
    }
}