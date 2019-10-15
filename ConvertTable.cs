using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using dotnetmysql.Build;
using dotnetmysql.Data;
using dotnetmysql.OutPuts;
using McMaster.Extensions.CommandLineUtils;

namespace dotnetmysql
{
    [Command(Name = "table", Description = "Convert Ms Table To MySql Table.")]
    public class ConvertTable
    {
        private string viewPath = @"CSharp\TableTemplate.cshtml";

        [Argument(0, Description = "Config Path")]
        [FileExists]
        public string ConfigPath { get; set; }

        public async Task<int> OnExecute(CommandLineApplication app)
        {
            if (string.IsNullOrEmpty(ConfigPath))
            {
                app.Out.WriteLine("Please enter the path to build configuration file:");
                return 1;
            }
            try
            {
                var build = new JsonBuilder(ConfigPath);
                if (build.Config.Tables.Count == 0)
                {
                    app.Out.WriteLine($"Process error.Detail message:no table in appsettings.json.");
                    return 1;
                }
                foreach (var p in build.Config.Tables)
                {
                    var tableName = p.Name.ToLower();
                    var columns = GetColumns(build.Config.Project.ConnectionString, tableName);
                    var context = new ContextBuild(build.Config.Project, tableName, columns, build.Config.DbTypeMaps);
                    var bulidContent = await build.TemplateEngine.Render(context, viewPath);
                    await new FileOutput().Output(bulidContent, tableName, build.Config.Output);
                }
            }
            catch (Exception e)
            {
                app.Out.WriteLine($"Process error.Detail message:{e.Message}");
                return 1;
            }
            return 0;
        }

        private List<ContextColumns> GetColumns(string connectionString, string tablename)
        {
            using (var coon = new SqlConnection(connectionString))
            {
                var sql = $@"SELECT tab.name [TableName],            --表名
                                   col.name [ColumnName],            --字段名
                                   typ.name [ColumnType],            --字段类型
                                   col.max_length [ColumnLength],    --字段长度
                                   col.is_identity [IsIdentity],     --是否自增
                                   pk.is_primary_key [IsPrimaryKey], --是否主键
                                   col.is_nullable [IsNullable],     --是否允许为NULL
                                   def.text [Default],               --默认值
                                   ext.value [Explain]               --字段说明
                            FROM sys.objects tab
                            LEFT JOIN sys.extended_properties ext ON ext.major_id = tab.object_id
                                                                     AND ext.class = 1
                            LEFT JOIN sys.columns col ON col.object_id = ext.major_id
                                                             AND col.column_id = ext.minor_id
                            LEFT JOIN sys.types typ ON typ.user_type_id = col.system_type_id
                            LEFT JOIN sys.syscomments def ON def.id = col.default_object_id
                            LEFT JOIN
                            (
                                SELECT t1.is_primary_key,
                                       t2.object_id,
                                       t2.column_id
                                FROM sys.indexes t1
                                INNER JOIN sys.index_columns t2 ON t2.object_id = t1.object_id
                                                                   AND t2.index_id = t1.index_id
                            ) pk ON pk.object_id = tab.object_id
                                    AND pk.column_id = col.column_id
                            WHERE tab.type = 'U'
                                  AND tab.name = '{tablename}'
                            ORDER BY ext.major_id;";
                return coon.Query<ContextColumns>(sql).ToList();
            }
        }
    }
}
