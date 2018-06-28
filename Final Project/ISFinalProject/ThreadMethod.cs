using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int count = 350;      //执行多少次  
            threadStartEvent.Invoke(count, new EventArgs());//通知主界面,我开始了,count用来设置进度条的最大值  
            if (this.process_str != "")
            {
                //try
                {
                    DataProcess.ParagraphProcess(this.process_str);
                    threadEvent.Invoke(50, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度  
                    DataProcess.NLPIRProcess();
                    threadEvent.Invoke(100, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度  
                    DataProcess.ClassifyProcess();
                    threadEvent.Invoke(150, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度  
                    DataProcess.LevelProcess();
                    threadEvent.Invoke(200, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度  
                    DataProcess.FSProcess();
                    threadEvent.Invoke(250, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度  
                    DataProcess.CRFSProcess(this.model);
                    threadEvent.Invoke(300, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度
                    ArrayList keyWords = DataProcess.GetKeyWords();
                    string keywords = "";
                    for (int i = 0; i < keyWords.Count; i++)
                    {
                        keywords += keyWords[i].ToString() + ";";
                        threadEvent.Invoke(300 + (Convert.ToDouble(i) / keyWords.Count) * 50, new EventArgs());//通知主界面我正在执行,数字表示进度条当前进度
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
