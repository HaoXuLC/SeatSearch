using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace SeatSearch.Controllers {
    [ApiController]
    [Route("Seat")]
    public class SeatController : Controller {
        [HttpGet("{name}")]
        public ActionResult<string> Read(string name) {
            var sqlString = SqliteGo(name);
            if (sqlString.Contains(",")) {
                return Ok(sqlString);
            }
            else {
                return BadRequest(sqlString);
            }
        }

        private string SqliteGo(string name) {
            string dbPath = AppContext.BaseDirectory + "11-6Seat.db";
            CSQLiteHelper cSQLiteHelper = new CSQLiteHelper(dbPath);
            string sqlString = cSQLiteHelper.SelectStars(name)[0];
            if (sqlString.Contains(",")) {
                var _sqlString = string.Format("{{\"seat\":\"{0}\",\"name\":\"{1}\"}}", sqlString.Split(',')[0], sqlString.Split(',')[1]);
                sqlString = _sqlString;
            }
            return sqlString;
        }
    }

    class CSQLiteHelper {
        private string _dbName = "";
        private string _SQLiteConnString = null; //连接字符串

        public CSQLiteHelper(string dbPath) {
            this._dbName = dbPath;
            this._SQLiteConnString = "Data Source=" + dbPath;
        }//获取db文件的路径
        public string[] SelectStars(string name) {
            var cmd = new SQLiteCommand();//获取命令的函数
            SQLiteConnection sqliteConn = new SQLiteConnection(_SQLiteConnString);//连接数据库
            var strs = new List<string>();//实际案例用不到数组哈
            try {
                sqliteConn.Open();//打开数据库
                string commandStr = string.Format("SELECT * FROM 'Seat' WHERE name IN('{0}')", name);//选择Seat这个表单，再用字符串格式填入name值
                cmd.CommandText = commandStr;
                cmd.Connection = sqliteConn;
                var reader = cmd.ExecuteReader();//读出符合条件的字符串
                if (reader.Read()) {
                    string str = null;//循环中字符串清null
                    if (reader.HasRows) {
                        for (int i = 0; i < reader.FieldCount; i++) {
                            str += (reader[i].ToString()) + ',';//把数字变成字符串|进行区分
                        }

                    }
                    strs.Add(str);
                }
                else {
                    strs.Add("未查到数据!请检查姓名或者去签到台确认！");
                }
                return strs.ToArray(); //此处不用数组也行，查询的指向的是单值
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            finally {
                cmd.Dispose();// 释放command
                sqliteConn.Close();//结合命令关闭才能关闭数据库
                sqliteConn.Dispose();//先关闭数据连接，再释放
            }
        }
    }
}
