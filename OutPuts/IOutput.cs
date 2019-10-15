using System.Threading.Tasks;

namespace dotnetmysql.OutPuts
{
    public interface IOutput
    {
        Task Output(string context, string fileName, Output output);
    }
}
