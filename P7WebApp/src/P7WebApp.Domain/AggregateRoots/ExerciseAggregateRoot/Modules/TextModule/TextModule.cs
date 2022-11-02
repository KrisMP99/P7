using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules.TextModule
{
    public class TextModule : Module
    {
        public string Text { get; set; }
        public List<Image> images;

        public void AddImage()
        {
            throw new NotImplementedException();
        }

        public void RemoveImage()
        {
            throw new NotImplementedException();
        }
    }
}
