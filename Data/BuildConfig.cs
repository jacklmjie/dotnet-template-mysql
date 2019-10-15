using dotnetmysql.OutPuts;
using System.Collections.Generic;

namespace dotnetmysql.Data
{
    public class BuildConfig
    {
        public ContextProject Project { get; set; }

        public Output Output { get; set; }

        public List<BuildDbType> DbTypeMaps { get; set; }

        public List<BuilderConfigTable> Tables { get; set; }
    }
}
