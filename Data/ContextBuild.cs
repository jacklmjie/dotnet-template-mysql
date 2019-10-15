using System.Collections.Generic;

namespace dotnetmysql.Data
{
    public class ContextBuild
    {
        public ContextBuild(ContextProject project,string tableName, List<ContextColumns> columns, List<BuildDbType> DbTypes)
        {
            this.Project = project;
            this.TableName = tableName;
            this.Columns = columns;
            this.DbTypes = DbTypes;
        }

        public ContextProject Project { get; set; }

        public string TableName { get; set; }

        public List<ContextColumns> Columns { get; set; }

        public List<BuildDbType> DbTypes { get; set; }
    }
}
