using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFinalProject
{
    //数据库访问类
    class DataAccess
    {
       // public static DataSet savedata=new DataSet ();
        //连接字符串
        public static string connstr = ConfigurationManager.AppSettings["ConnectionString"].ToString();

        //连接对象
        public static SqlConnection Connection = new SqlConnection(connstr);

        //打开连接
        public static void OpenConnection()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }

        //关闭连接
        public static void CloseConnection()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        //通过序号查找参考文献篇名
        public static DataSet GetTitleById(string id)
        {
            string SQLstr = @"select 篇名 from cssci2014_ref where 文件序号 =" + id.Trim() + ";";

            OpenConnection();
            SqlDataAdapter da = new SqlDataAdapter(SQLstr, Connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CloseConnection();
            return ds;
        }
        public static DataSet GetTitleByTi(string ti)
        {

            string SQLstr = "select * from cssci2014_sql where 来源篇名 like '%" + ti + "%' order by 来源篇名 desc";

            OpenConnection();
            SqlDataAdapter da = new SqlDataAdapter(SQLstr, Connection);
          
             DataSet ds = new DataSet();
            da.Fill(ds);
            CloseConnection();
            return ds ;

        }
    }
}
