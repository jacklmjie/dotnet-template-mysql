using dotnetmysql.Data;
using System.Threading.Tasks;

namespace dotnetmysql.RazorTemplates
{
    public interface ITemplateEngine
    {
        void Initialize();

        Task<string> Render(ContextBuild model, string viewPath);
    }
}
