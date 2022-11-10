using P7WebApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule
{
    public class Image : EntityBase
    {
        public int TextModuleId { get; private set; }
        public byte[] File { get; private set; }
    }
}
