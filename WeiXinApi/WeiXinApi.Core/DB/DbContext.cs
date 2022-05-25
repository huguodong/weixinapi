using Furion;
using SqlSugar;
using System;
using System.IO;

namespace WeiXinApi.Core
{
    public class DbContext
    {
        public static string ConnectionString = Path.Combine(App.WebHostEnvironment.ContentRootPath, "weixin.sqlite");

        public static SqlSugarScope Db = new SqlSugarScope(new ConnectionConfig()
        {
            DbType = SqlSugar.DbType.Sqlite,
            ConnectionString = "DataSource=" + ConnectionString,
            IsAutoCloseConnection = true
        },
       db =>
        {
            //单例参数配置，所有上下文生效
            db.Aop.OnLogExecuting = (s, p) =>
             {
                 var sql = UtilMethods.GetSqlString(DbType.SqlServer, s, p);
                 Console.WriteLine(sql);
             };
        });
    }
}
