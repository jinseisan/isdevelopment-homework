using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ISFinalProject
{
    class DataProcess
    {
        //定义文件路径
        static string NLPIRAdress = "../../ProcessingFiles/NLPIR.txt";//经过分词后的NLPIR.txt文件路径
        static string ClassifyAdress = "../../ProcessingFiles/Classify.txt";//经过词性和构词处理后的Classify.txt文件路径
        static string LevelAdress = "../../ProcessingFiles/Level.txt";//经过分类处理的Level.txt文件路径
        static string FSAdress = "../../ProcessingFiles/FS.txt";//经过等级处理的FS.txt文件路径
        static string FeatursAdress = "../../ProcessingFiles/Featurs.txt";//经过fs处理最终包含所有特征的的Featurs.txt文件路径
        static string ResultAdress = "../../ProcessingFiles/Result.txt";//经过CRFS处理得到结果的的Result.txt文件路径

        //对NLPIR.txt文件进行词性和构词处理
        public static void NLPIRProcess()
        {
            StreamWriter sw = new StreamWriter(ClassifyAdress);
            StreamReader sr = new StreamReader(NLPIRAdress);

            sw.Close();
            sr.Close();
            
        }

        //
    }
}
