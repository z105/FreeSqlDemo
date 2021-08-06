using System;
using FreeSqlDemo.Model;

namespace FreeSqlDemo
{
    class Program
    {
        private static string dbConn = "Data Source=.;Initial Catalog=DapperTest;Integrated Security=True;";

        static void Main(string[] args)
        {

            // IFreeSql 是 ORM 最顶级对象，所有操作都是使用它的方法或者属性：
            IFreeSql fsql = new FreeSql.FreeSqlBuilder().
                UseConnectionString(FreeSql.DataType.SqlServer, dbConn).
                UseAutoSyncStructure(true). //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
                Build();    // 创建一个 IFreeSql 对象。

            var blog = new Blog { Url = "http://sample.com" };
            var id = blog.BlogId = (int)fsql.Insert<Blog>()
                .AppendData(blog)
                .ExecuteIdentity();

            // 导入表数据
            int affrows = fsql.Select<Blog>().Limit(2).
                InsertInto(null, a => new Blog
                {
                    Url = "123"
                });

            var blogs = fsql.Select<Blog>()
                .Where(b => b.Rating > 3)
                .OrderBy(b => b.Url)
                .Skip(100)
                .Limit(10) // 第100行-110行的记录
                .ToList();

            //UPDATE `Topic` SET `CreateTime` = '2018-12-08 00:04:59' 
            //WHERE (`Id` = 1)
            fsql.Update<Blog>(1).
                Set(a => a.Url, "baidu.com").
                Set(a => a.Rating, 10).
                ExecuteAffrows();

            fsql.Update<Blog>(1).
                Set(a => new Blog 
                { 
                    Url = "123",
                    Rating = 2
                }).
                ExecuteAffrows();


            Console.WriteLine("github test");
            Console.WriteLine("Hello World!");

        }
    }
}
