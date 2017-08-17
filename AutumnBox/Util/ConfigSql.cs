using AutumnBox.Debug;
using System;
using System.Data.SQLite;
using System.IO;

namespace AutumnBox.Util
{
    internal class ConfigSql
    {
        private readonly string[] initSqls = {
                "create table boolValues (key char(20), value boolean) ",
                "create table intValues (key char(20), value int) ",
                "create table stringValues (key char(20), value char(20)) ",
                "insert into stringValues (key,value) values ('language','not_set')",
                "insert into intValues (key,value) values ('passwordKey',1210626737)",
                $"insert into intValues (key,value) values ('skipBuild',{StaticData.nowVersion.build.ToString()})",
                "insert into boolValues (key,value) values ('isShowTur',1)",
                "insert into boolValues (key,value) values ('isFristLaunch',1)",
        };
        private const string SQL_PATH = "atbdata.sqlite";
        private SQLiteConnection dbConnection;
        private const string TAG = "Config";
        public ConfigSql()
        {
#if SQLTEST
            Console.WriteLine(TAG, "发生错误,初始化数据库");
            InitSql();
#else
            if (!File.Exists(SQL_PATH))
            {
                InitSql();
            }
            else
            {
                dbConnection = new SQLiteConnection($"Data Source={SQL_PATH};Version=3;");
                dbConnection.Open();
            }
#endif

        }
        /// <summary>
        /// 初始化数据库
        /// </summary>
        private void InitSql()
        {
            try { dbConnection.Close(); } catch { }
            SQLiteConnection.CreateFile(SQL_PATH);
            dbConnection = new SQLiteConnection($"Data Source={SQL_PATH};Version=3;");
            dbConnection.Open();
            foreach (string sql in initSqls)
            {
                ExecuteSqlCommand(sql);
            }
        }
        /// <summary>
        /// 读取并返回数据库中的值
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public object Read(string table, string key)
        {
            string sql = $"select * from {table} where key='{key}'";
            try
            {
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                return reader["value"];
            }
            catch
            {
                InitSql();
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                return reader["value"];
            }
        }
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="key">要设置的数据的key</param>
        /// <param name="value">要设置的值</param>
        public void Set(string table, string key, object value)
        {
            string sql;
            if (value is int)
            {
                sql = $"update {table} set value={value} where key='{key}'";
            }
            else if (value is bool)
            {
                Log.d("ConfigSql Set", Convert.ToInt32(value).ToString());
                sql = $"update {table} set value={Convert.ToInt32(value).ToString()} where key='{key}'";
            }
            else if (value is string)
            {
                sql = $"update {table} set value='{value}' where key='{key}'";
            }
            else if (value is null)
            {
                sql = $"update {table} set value=null where key='{key}'";
            }
            else { throw new Exception(); }
            ExecuteSqlCommand(sql);
        }
        /// <summary>
        /// 执行一条sql指令,并且无返回
        /// </summary>
        /// <param name="c"></param>
        private void ExecuteSqlCommand(string c)
        {
            Log.d("ConfigSql", "ExecuteSql " + c);
            SQLiteCommand command = new SQLiteCommand(c, dbConnection);
            command.ExecuteNonQuery();
            Log.d("ConfigSql", "ExecuteSqlFinish" + c);
        }
    }
}
