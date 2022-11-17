using P7WebApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule
{
    public class Image : EntityBase
    {
        public Image(int textModuleId, byte[] file)
        {
            TextModuleId = textModuleId;
            File = file;
        }

        public int TextModuleId { get; private set; }
        public byte[] File { get; private set; }
    }
}
