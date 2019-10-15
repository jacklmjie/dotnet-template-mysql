# tools-dotnetmysql
## NET Core Global Tools和Razor模板生成文件
sql server的表和存储过程转mysql
(1).安装tool dotnet tool install -g <工具ID>
   dotnet tool install -g dotnetmysql --add-source ./

(2).卸载tool dotnet tool uninstall -g <工具ID>

(3).更新tool dotnet tool update -g <工具ID>

(4).列出本地已安装tool dotnet tool list -g

(5).使用
dotnetmysql table D:\appsettings.json

dotnetmysql proc D:\appsettings.json 未完成
