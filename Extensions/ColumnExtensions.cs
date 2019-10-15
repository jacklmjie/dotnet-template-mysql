using dotnetmysql.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnetmysql.Extensions
{
    public static class ColumnExtensions
    {
        public static string ConvertColumn(this List<ContextColumns> columns, List<BuildDbType> maps)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var column in columns.Where(x => x.ColumnName != null))
            {
                var mapcolumn = Mapcolumn(maps, column);
                stringBuilder.AppendLine($"`{column.ColumnName}` {mapcolumn} {(column.ColumnType == "varchar" ? "CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci" : "")} {(column.IsNullable ? "NULL" : "NOT NULL")} {(column.IsIdentity ? "AUTO_INCREMENT" : (string.IsNullOrEmpty(column.Default) ? "DEFAULT NULL" : $"DEFAULT {column.Default}"))} {(string.IsNullOrEmpty(column.Explain) ? "" : $"COMMENT '{column.Explain}'")},");
            }
            return stringBuilder.ToString();
        }

        public static string ConvertPrimaryKey(this List<ContextColumns> columns)
        {
            var column = columns.FirstOrDefault(x => x.IsIdentity);
            return $"{(column == null ? "" : $"PRIMARY KEY (`{column.ColumnName}`) USING BTREE")}";
        }

        public static string ConvertTableEnd(this List<ContextColumns> columns)
        {
            var column = columns.FirstOrDefault(x => x.ColumnName == null);
            return $@"ENGINE = InnoDB AUTO_INCREMENT = 0 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci {(column == null ? "" : $"COMMENT = '{column.Explain}'")} ROW_FORMAT = Dynamic";
        }

        private static string Mapcolumn(List<BuildDbType> maps, ContextColumns column)
        {
            var mapcolumn = $"{column.ColumnType}({column.ColumnLength})";
            var map = maps.FirstOrDefault(x => x.Type == column.ColumnType);
            if (map != null)
            {
                if (!string.IsNullOrEmpty(map.Default))
                {
                    column.ColumnType = map.To;
                    column.Default = map.Default;
                }
                mapcolumn = $"{(map.To.Contains(")") ? $"{map.To}" : $"{map.To}({column.ColumnLength})")}";
            }
            return mapcolumn;
        }
    }
}
