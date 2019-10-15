using System;

namespace dotnetmysql.Data
{
    public class ContextProject
    {
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// 数据库地址
        /// </summary>
        public string DbHost { get; set; }

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
