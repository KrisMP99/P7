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
        public List<Image> Images;

        public TextModule(int exerciseId, string description, double height, double width, int position) : base(exerciseId, description, height, width, position)
        {
        }

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
