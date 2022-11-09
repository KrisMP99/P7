using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules.QuizModule
{
    public class QuizModule : Module
    {
        public string Text { get; private set; }
        public List<Choice> Choices;

        public QuizModule(int exerciseId, string description, double height, double width, int position) : base(exerciseId, description, height, width, position)
        {
        }

        public void UpdateText(string text)
        {
            Text = text;
        }

        public void AddChoice()
        {
            throw new NotImplementedException();
        }

        public void RemoveChoice()
        {
            throw new NotImplementedException();
        }
    }
}
