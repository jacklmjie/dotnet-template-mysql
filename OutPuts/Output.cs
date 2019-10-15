namespace dotnetmysql.OutPuts
{
    public class Output
    {
        /// <summary>
        /// 输出目录
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件创建模式
        /// </summary>
        public CreateMode Mode { get; set; } = CreateMode.None;

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string Extension { get; set; }
    }
}
