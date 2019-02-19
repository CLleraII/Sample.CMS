using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            test_select_list();
            Console.Read();
        }
        public static string connStr = "Data Source=(LOCAL);User ID=sa;Password=123456;Initial Catalog=BPMDB57Y;Pooling=true;Max Pool Size=100;";
        public static void test_insert()
        {
            var content = new Content
            {
                title = "标题1",
                content = "内容1",

            };
            using (var conn = new SqlConnection("Data Source=(LOCAL);User ID=sa;Password=123456;Initial Catalog=BPMDB57Y;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"INSERT INTO [Content]
                (title, [content], status, add_time, modify_time)
VALUES   (@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql_insert, content);
                Console.WriteLine($"test_insert：插入了{result}条数据！");
            }
        }

        public static void test_mult_insert()
        {
            List<Content> contents = new List<Content>() {
               new Content
            {
                title = "批量插入标题1",
                content = "批量插入内容1",

            },
               new Content
            {
                title = "批量插入标题2",
                content = "批量插入内容2",

            },
        };

            using (var conn = new SqlConnection("Data Source=(LOCAL);User ID=sa;Password=123456;Initial Catalog=BPMDB57Y;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"INSERT INTO [Content]
                (title, [content], status, add_time, modify_time)
VALUES   (@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mult_insert：插入了{result}条数据！");
            }
        }

        /// <summary>
        /// 查询单条指定的数据
        /// </summary>
        public static void test_select_one()
        {
            using (var conn = new SqlConnection(connStr))
            {
                string sql_insert = @"select * from [dbo].[content] where id=@id";
                var result = conn.QueryFirstOrDefault<Content>(sql_insert, new { id = 1 });
                Console.WriteLine($"test_select_one：查到的数据为：");
            }
        }

        /// <summary>
        /// 查询多条指定的数据
        /// </summary>
       public  static void test_select_list()
        {
            using (var conn = new SqlConnection(connStr))
            {
                string sql_insert = @"select * from [dbo].[content] where id in @ids";
                var result = conn.Query<Content>(sql_insert, new { ids = new int[] { 2, 3 } });
                Console.WriteLine($"test_select_one：查到的数据为：");
            }
        }
    }
}
