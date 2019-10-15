namespace dotnetmysql.OutPuts
{
    /// <summary>
    /// 文件创建模式
    /// </summary>
    public enum CreateMode
    {
        None = 0,
        /// <summary>
        /// 增量创建，如果存在则忽略
        /// </summary>
        Incre = 1,
        /// <summary>
        /// 全量创建，如果存在则重新创建
        /// </summary>
        Full = 2
    }
}
