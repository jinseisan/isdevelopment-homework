using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Data;
using System.Text.RegularExpressions;

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

        //公共变量
        static string Punctuations = "：:，,？?、.。！!“”…‘’—－《》()（）[]·<>";

        //对输入的字符串调用NLPIR分词处理并写入NLPIR.txt
        public static string ParagraphProcess(string paragraph)
        {
            if (!NLPIRTool.NLPIR_Init(@"../../ICTCLAS", 0, ""))//给出Data文件所在的路径，注意根据实际情况修改。
            {
                throw new Exception("Init ICTCLAS failed!");
            }
            NLPIRTool.NLPIR_SetPOSmap(3);//使用北大一级标注
            paragraph = paragraph.Replace("\n","");//将换行符替换为空
            IntPtr intPtr = NLPIRTool.NLPIR_ParagraphProcess(paragraph.Trim());//切分结果保存为IntPtr类型
            String str = Marshal.PtrToStringAnsi(intPtr);//将切分结果转换为string，将空格替换为制表符'\n'
            str = str.Replace(' ','\n');
            StreamWriter sw = new StreamWriter(NLPIRAdress);
            sw.WriteLine(str);
            sw.Close();
            NLPIRTool.NLPIR_Exit();
            return str;
        }

        //处理经过NLPIR分词后的一个词（如“文选/n”），对其每个字进行词性个构词处理
        /* 构词原理：
         * 标点B；英文/数字E；单个字D；词组T开头Z中间W结尾
         */
        public static string NLPIRWordProcess(string input_word)
        {
            //判断字符串是否符合格式“词/词性”
            if (!input_word.Contains('/')) return "";

            //创建正则表达式，用来判断字符串中内容
            Regex cn = new Regex(@"^[\u4e00-\u9fa5]*$");//由中文字符组成
            Regex en_num = new Regex(@"^[A-Za-z0-9]*$");//数字和英文

            string result = "初始化字符\n";

            //避免使用split()方法，因为词语可能本身就自带'/'符号
            string cx = input_word[input_word.Length - 1].ToString().ToUpper();//最后一个字符为词性
            string word = input_word.Remove(input_word.Length - 2, 2);//取去掉最后两个字符的新串为单词

            //进行逻辑判断并进行词性和构词处理
            if (cx[0] == 'W' || Punctuations.Contains(cx[0]))
            {//1.如果为标点（标注为/w）或存在于定义的标点字符串中，则取第一个字符加入result,前为词性标注，后为构词标注
                result = word + "\t" + cx + "\tB";
            }
            else
            {//2.如果不为标点
                if (word.Length == 1)
                {//2.1 如果单词长度为1
                    if (cn.IsMatch(word))
                    {//2.1.1 如果为一个汉字
                        result = word + "\t" + cx + "\tD";
                    }
                    else
                    {//2.1.2 如果是非汉字
                        result = word + "\t" + cx + "\tE";
                    }
                }
                else
                {//2.2 如果单词长度>=2
                    if (!Regex.IsMatch(word, @"[\u4e00-\u9fa5]"))
                    {//2.3.1如果不含中文,需考虑特殊字符的用法：'-'和出现在末尾的':'
                        if (word.Contains('-'))
                        {//2.3.1.1 如果包含-符号而分词未分开
                            string[] words = word.Split('-');
                            if (words.Length == 1)
                            {//如果拆分后只有一个，即'-'出现在首尾
                                result = "";
                                if (word[0] == '-') result += '-' + "\t" + cx + "\tB\n";
                                result += words[0] + "\t" + cx + "\tE\n";
                                if (word[word.Length - 1] == '-') result += '-' + "\t" + cx + "\tB";
                            }
                            else
                            {//拆分后单词大于等于2，即'-'出现在词中
                                result = "";
                                //将除最后一项加入result字符串
                                for (int i = 0; i < words.Length - 1; i++)
                                {
                                    result += words[i] + "\t" + cx + "\tE\n";
                                    result += '-' + "\t" + cx + "\tB\n";
                                }
                                //将最后一项加入result字符串
                                result += words[words.Length - 1] + "\t" + cx + "\tE";
                            }
                        }
                        else if (word[word.Length - 1] == ':')
                        {//2.3.1.2 最后一个字符为':'而分词未分开
                            result = word.Remove(word.Length - 1, 1) + "\t" + cx + "\tE\n";
                            result += ':' + "\t" + cx + "\tB";
                        }
                        else
                        {//2.3.1.3 如果不含标点，是其他字符串
                            result = word + "\t" + cx + "\tE";
                        }
                    }
                    else if (Regex.Matches(word, @"[\u4e00-\u9fa5]").Count == 1)
                    {//2.3.2 如果字符串中含有一个中文字符（多为量词+中文单位）
                        int index = Regex.Match(word, @"[\u4e00-\u9fa5]").Index;
                        if (index == 0)
                        {//中文出现在第一位
                            result = word[0] + "\t" + cx + "\tD\n";
                            result += word.Remove(0, 1) + "\t" + cx + "\tE";
                        }
                        else if (index == word.Length - 1)
                        {//中文出现在末尾
                            char cstr = word[word.Length - 1];
                            result = word.Remove(word.Length - 1, 1) + "\t" + cx + "\tE\n";
                            result += cstr + "\t" + cx + "\tD";
                        }
                        else
                        {//中文出现在词中部
                            string[] words = word.Split(word[index]);
                            for (int i = 0; i < words.Length - 1; i++)
                            {
                                result = words[i] + "\t" + cx + "\tE\n";
                                result += word[index] + "\t" + cx + "\tD\n";
                            }
                            //将最后一项加入result字符串
                            result += words[words.Length - 1] + "\t" + cx + "\tE";
                        }
                    }
                    else
                    {//2.3.3 如果字符串中含有两个及以上中文字符,TZW
                        result = word[0] + "\t" + cx + "\tT\n";
                        for (int i = 1; i < word.Length - 1; i++)
                        {
                            result += word[i] + "\t" + cx + "\tZ\n";
                        }
                        result += word[word.Length - 1] + "\t" + cx + "\tW";
                    }
                }

            }
            return result;
        }

        //对NLPIR.txt文件进行词性和构词处理
        public static void NLPIRProcess()
        {
            StreamWriter sw = new StreamWriter(ClassifyAdress);
            StreamReader sr = new StreamReader(NLPIRAdress);

            string strLine = sr.ReadLine();          
            while (strLine != null)
            {
                sw.WriteLine(NLPIRWordProcess(strLine));
                strLine = sr.ReadLine();
            }  
            sw.Close();
            sr.Close();
        }

        //数据库连接测试
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
