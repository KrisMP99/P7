using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules.CodeModule
{
    public class CodeModule : Module
    {
        public string Code { get; set; }
        public List<TestCase> TestCases { get; private set; }

        public void RunCode()
        {
            throw new NotImplementedException();
        }

        public void RunTestCase()
        {
            throw new NotImplementedException();
        }

        public void CreateTestCase()
        {
            throw new NotImplementedException();
        }

        public void DeleteTestCase()
        {
            throw new NotImplementedException();
        }
    }
}
