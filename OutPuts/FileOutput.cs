using System.IO;
using System.Threading.Tasks;

namespace dotnetmysql.OutPuts
{
    public class FileOutput : IOutput
    {
        public async Task Output(string context, string fileName, Output output)
        {
            var filePath = Path.Combine(output.Path, fileName, output.Extension);
            var fileExists = File.Exists(filePath);
            if (fileExists)
            {
                switch (output.Mode)
                {
                    case CreateMode.None:
                    case CreateMode.Incre:
                        {
                            break;
                        }
                    case CreateMode.Full:
                        {
                            File.Delete(output.Path);
                            break;
                        }
                }
            }
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                await streamWriter.WriteAsync(context);
            }
        }
    }
}
