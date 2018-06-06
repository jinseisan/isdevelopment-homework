using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Data;

namespace ISFinalProject
{
    //数据处理类
    class DataProcess
    {
        //定义文件路径
        static string NLPIRAdress = @"../../ProcessingFiles/NLPIR.txt";//经过分词后的NLPIR.txt文件路径
        static string ClassifyAdress = @"../../ProcessingFiles/Classify.txt";//经过词性和构词处理后的Classify.txt文件路径
        static string LevelAdress = @"../../ProcessingFiles/Level.txt";//经过分类处理的Level.txt文件路径
        static string FSAdress = @"../../ProcessingFiles/FS.txt";//经过等级处理的FS.txt文件路径
        static string FeatursAdress = @"../../ProcessingFiles/Featurs.txt";//经过fs处理最终包含所有特征的的Featurs.txt文件路径
        static string ResultAdress = @"../../ProcessingFiles/Result.txt";//经过CRFS处理得到结果的的Result.txt文件路径

        //对输入的字符串调用NLPIR分词处理并写入NLPIR.txt
        public static string ParagraphProcess(string paragraph)
        {
            if (!NLPIRTool.NLPIR_Init(@"../../ICTCLAS", 0, ""))//给出Data文件所在的路径，注意根据实际情况修改。
            {
                throw new Exception("Init ICTCLAS failed!");
            }
            NLPIRTool.NLPIR_SetPOSmap(3);//使用北大一级标注
            paragraph.Replace('\n', ' ');
            IntPtr intPtr = NLPIRTool.NLPIR_ParagraphProcess(paragraph);//切分结果保存为IntPtr类型
            String str = Marshal.PtrToStringAnsi(intPtr).Replace(' ', '\n');//将切分结果转换为string，将空格替换为'\n'
            StreamWriter sw = new StreamWriter(NLPIRAdress);
            sw.WriteLine(str);
            sw.Close();
            NLPIRTool.NLPIR_Exit();
            return str;
        }

        //对NLPIR.txt文件进行词性和构词处理
        public static void NLPIRProcess()
        {
            StreamWriter sw = new StreamWriter(ClassifyAdress);
            StreamReader sr = new StreamReader(NLPIRAdress);

            string str = sr.ReadLine().ToString();
            
            
            sw.WriteLine(str);

            sw.Close();
            sr.Close();
            
        }

        //流程测试
        public static string MainProcess()
        {
            string title_str = "";

            DataSet ds =  DataAccess.GetTitleById("'11A0012014010001'");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                title_str += ds.Tables[0].Rows[i]["篇名"].ToString().Trim() + "\n";
            }
            return title_str;
        }

    }
}
