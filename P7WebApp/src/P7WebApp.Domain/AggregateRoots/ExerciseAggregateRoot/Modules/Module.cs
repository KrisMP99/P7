using P7WebApp.SharedKernel;
using P7WebApp.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules
{
    public abstract class Module : EntityBase, IAggregateRoot
    {
        public Module(int moduleId, string description, double height, double width, int position) 
        {
            ModuleId = moduleId;
            Description = description;
            Height = height;
            Width = width;
            Posititon = position;
        }

        public int ModuleId { get; set; }
        public string Description { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int Posititon { get; set; }

        public void UpdateModule()
        {
            throw new NotImplementedException();
        }
    }
}
