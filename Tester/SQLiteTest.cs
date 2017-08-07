using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tester
{
    class SQLiteTest
    {
        //数据库连接
        private string sqlFilePath = "test.sqlite";
        private SQLiteConnection m_dbConnection;
        public SQLiteTest() {
            //InitDatabase();
            m_dbConnection = new SQLiteConnection($"Data Source={sqlFilePath};Version=3;");
            m_dbConnection.Open();
            //CreateTable();
            //Insert("test", "t", "3");
            //Insert("test", "x", "3");
            //Insert("test", "c", "3");
            //Insert("test", "a", "3");
            Read("test", "t");
        }
        private void InitDatabase() {
            SQLiteConnection.CreateFile(sqlFilePath);
        }
        private void Insert(string table, string key, string value) {
            string sql = $"insert into {table} (key,value) values ('{key}',{value})";
            SQLiteCommand command = new SQLiteCommand(sql,m_dbConnection);
            command.ExecuteNonQuery();
        }
        private void Read(string table, string key) {
            string sql = $"select * from {table} where key='{key}'";
            SQLiteCommand command = new SQLiteCommand(sql,m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                Console.WriteLine(reader["value"].ToString());
            }
        }
        private void CreateTable() {
            string sqlCommand = "create table test (key char(20) , value int) ";
            SQLiteCommand command = new SQLiteCommand(sqlCommand, m_dbConnection);
            command.ExecuteNonQuery();
        }
    }
}
