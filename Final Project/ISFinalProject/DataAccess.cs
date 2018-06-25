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

        //Select查询，返回DataSet
        public static DataSet Select_SQL(string select_sql)
        {
            if (select_sql == "") { return null; }
            OpenConnection();
            SqlDataAdapter da = new SqlDataAdapter(select_sql, Connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CloseConnection();
            return ds;
        }

        //无返回查询语句，Insert/Delete/Update
        public static bool NonQuery_SQL(string nonquery_sql)
        {
            if (nonquery_sql == "") { return false; }
            OpenConnection();
            SqlCommand command = new SqlCommand(nonquery_sql, Connection);
            try
            {
                command.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            catch (Exception f)
            {
                //MessageBox.Show(f.Message);
                CloseConnection();
                return false;
            }              
        }
        /*调用prcPageResult存储过程
         * 参数信息：
         * @current_page int                          --当前页页码 (即Top currPage)
         * @show_column varchar(2000)                 --需要得到的字段 (即 column1,column2,......)
         * @table_name varchar(2000)                  --需要查看的表名 (即 from table_name)
         * @sql_condition varchar(2000)               --查询条件 (即 where condition......) 不用加where关键字
         * @asc_column varchar(100)                   --排序的字段名 (即 order by column asc/desc)
         * @order_type bit                            --排序的类型 (0为升序,1为降序)
         * @pk_column varchar(50)                     --主键名称
         * @page_size int                             --分页大小
         */
        public static DataSet sql_prcPageResult(int current_page, string show_column, string table_name,
                                                string sql_condition, string asc_column, int order_type,
                                                string pk_column, int page_size)
        {
            string sql_process = "prcPageResult";//存储过程名
            //创建SqlCommand
            SqlCommand comm = new SqlCommand(sql_process, Connection);
            comm.CommandType = CommandType.StoredProcedure;
            //设置参数
            comm.Parameters.Add("@currPage", SqlDbType.Int).Value = current_page;
            comm.Parameters.Add("@showColumn", SqlDbType.VarChar).Value = show_column;
            comm.Parameters.Add("@tabName", SqlDbType.VarChar).Value = table_name;
            comm.Parameters.Add("@strCondition", SqlDbType.VarChar).Value = sql_condition;
            comm.Parameters.Add("@ascColumn", SqlDbType.VarChar).Value = asc_column;
            comm.Parameters.Add("@bitOrderType", SqlDbType.Bit).Value = order_type;
            comm.Parameters.Add("@pkColumn", SqlDbType.VarChar).Value = pk_column;
            comm.Parameters.Add("@pageSize", SqlDbType.Int).Value = page_size;
            //打开连接
            OpenConnection();
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CloseConnection();
            return ds;
        }
        /*调用prcRowsCount存储过程
         * 参数信息：
         * @tabName varchar(200),            --需要查询的表名
         * @colName varchar(200)='*',        --需要查询的列名
         * @condition varchar(200)=''        --查询条件
         */
        public static int sql_prcRowsCount(string tabName, string colName, string condition)
        {
            string sql_process = "prcRowsCount";//存储过程名
            //创建SqlCommand
            SqlCommand comm = new SqlCommand(sql_process, Connection);
            comm.CommandType = CommandType.StoredProcedure;
            //设置参数
            comm.Parameters.Add("@tabName", SqlDbType.VarChar).Value = tabName;
            comm.Parameters.Add("@colName", SqlDbType.VarChar).Value = colName;
            comm.Parameters.Add("@condition", SqlDbType.VarChar).Value = condition;
            //打开连接
            OpenConnection();
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CloseConnection();
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }
        /*
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
         * 
         */

    }
}
