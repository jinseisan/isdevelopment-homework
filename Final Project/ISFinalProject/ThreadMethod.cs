using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISFinalProject
{
    //数据处理中提供委托和线程方法
    class ThreadMethod
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
        /// 线程状态改变事件  
        /// </summary>  
        public event EventHandler threadStatusEvent;
        /// <summary>  
        /// 线程结束事件  
        /// </summary>  
        public event EventHandler threadEndEvent;

        private string model = "";
        private string process_str = "";

        public void set_model(string model)
        {
            this.model = model;
        }
        public void set_processString(string str)
        {
            this.process_str = str;
        }

        public void runMethod()
        {
            int count = 350;      //进度条最大值 
            threadStartEvent.Invoke(count, new EventArgs());//通知主界面,我开始了,count用来设置进度条的最大值  
            if (this.process_str != "")
            {
                try
                {
                        //1.NLPIR分词处理
                        threadStatusEvent.Invoke("开始分词处理……", new EventArgs());
                        DataProcess.ParagraphProcess(this.process_str);
                        threadEvent.Invoke(50, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度
                        //2.分词完毕，进行词性和构词特征处理
                        threadStatusEvent.Invoke("分词完毕，开始词性构词特征处理……", new EventArgs());
                        DataProcess.NLPIRProcess();
                        threadEvent.Invoke(100, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度 
                        //3.词性和构词处理完毕，进行分类特征处理
                        threadStatusEvent.Invoke("词性和构词处理完毕，开始分类特征处理……", new EventArgs());
                        DataProcess.ClassifyProcess();
                        threadEvent.Invoke(150, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度
                        //4.分类特征处理完毕，进行级别特征处理
                        threadStatusEvent.Invoke("分类处理完毕，开始级别特征处理……", new EventArgs());
                        DataProcess.LevelProcess();
                        threadEvent.Invoke(200, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度
                        //5.级别特征处理完毕，进行FS特征处理
                        threadStatusEvent.Invoke("级别特征处理完毕，开始FS特征处理……", new EventArgs());
                        DataProcess.FSProcess();
                        threadEvent.Invoke(250, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度
                        //6.FS处理完毕，调用模型获得结果
                        threadStatusEvent.Invoke("FS处理完毕，开始调用模型提取关键词……", new EventArgs());
                        DataProcess.CRFSProcess(this.model);
                        threadEvent.Invoke(300, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度
                        //7.模型处理完毕，整合抽取关键词
                        threadStatusEvent.Invoke("模型处理完毕，开始提取关键词……", new EventArgs());
                        ArrayList keyWords = DataProcess.GetKeyWords();
                        string keywords = "";
                        for (int i = 0; i < keyWords.Count; i++)
                        {
                            keywords += keyWords[i].ToString() + "；";
                            threadEvent.Invoke(300 + (Convert.ToDouble(i) / keyWords.Count) * 50, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度
                        }
                        keywords = keywords.Remove(keywords.Length - 1);
                        Thread.Sleep(1000);
                        threadStatusEvent.Invoke("处理完毕！", new EventArgs());
                        threadEndEvent.Invoke(keywords, new EventArgs());//通知主界面我已经完成了
                    
                }
                catch (Exception f)
                {
                    MessageBox.Show(f.Message, "出错！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
