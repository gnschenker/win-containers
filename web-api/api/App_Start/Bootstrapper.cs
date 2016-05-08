using System;
using System.Data.SqlClient;
using Dapper;

namespace api
{
    public static class Bootstrapper
    {
        public static void Init()
        {
            var server = Db.ConnectionString = Environment.GetEnvironmentVariable("DB_SERVER") ?? @".\SQLEXPRESS2012";
            var database = Db.ConnectionString = Environment.GetEnvironmentVariable("DB_DATABASE") ?? "Test";
            var user = Db.ConnectionString = Environment.GetEnvironmentVariable("DB_USER_NAME") ?? "sa";
            var pass = Db.ConnectionString = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "sa";
            Db.ConnectionString = $"server={server};database=master;User Id={user};Password={pass};";
            using (var conn = new SqlConnection(Db.ConnectionString))
            {
                var sql = $"IF db_id('{database}') is null create database [{database}];";
                conn.Execute(sql);
            }
            Db.ConnectionString = $"server={server};database={database};User Id={user};Password={pass};";
            using (var conn = new SqlConnection(Db.ConnectionString))
            {
                const string sql = @"
                    if not exists (select * from sysobjects where name='Projects' and xtype='U')
                    create table Projects(
                      Id int identity not null,
                      Name nvarchar(50) not null,
                      constraint PK_Projects primary key(id)
                    )";
                conn.Execute(sql);
            }
        }
    }

    public static class Db
    {
        public static string ConnectionString { get; set; }
    }
}