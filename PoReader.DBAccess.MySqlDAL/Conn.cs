using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoReader.DBAccess
{
    public static class Conn
    {
        #region 变量
        private static string _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WebSiteConstr"].ConnectionString;
        #endregion

        #region 属性
        /// <summary>
        /// 当前登录用户的数据库连接字符串
        /// 当更改当前数据库连接字符串后，程序会自动关闭、再次打开数据库连接。
        /// </summary>
        public static string ConnectionString
        {
            get { return Conn._ConnectionString; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && _ConnectionString != value)
                {
                    ConnectionClose();
                    Conn._ConnectionString = value;
                }
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 关闭SQLite数据库连接对象
        /// </summary>
        public static void ConnectionClose()
        {
            if (MySqlHelper.MySqlConnection != null)
            {
                if (MySqlHelper.MySqlConnection.State == System.Data.ConnectionState.Open)
                {
                    MySqlHelper.MySqlConnection.Close();
                }
                MySqlHelper.MySqlConnection = null;
            }
        }
        #endregion
    }
}