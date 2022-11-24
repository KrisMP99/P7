using P7WebApp.Domain.Common;
using P7WebApp.Domain.Exceptions;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate
{
    public class ExerciseLayout : Enumeration
    {
        public ExerciseLayout(int id, string name) : base(id, name)
        {
        }

        public static ExerciseLayout Single = new ExerciseLayout(1, nameof(Single).ToLowerInvariant());
        public static ExerciseLayout TwoVertical = new ExerciseLayout(2, nameof(TwoVertical).ToLowerInvariant());
        public static ExerciseLayout TwoHorizontal = new ExerciseLayout(3, nameof(TwoHorizontal).ToLowerInvariant());
        public static ExerciseLayout TwoLeftOneRight = new ExerciseLayout(4, nameof(TwoLeftOneRight).ToLowerInvariant());
        public static ExerciseLayout OneLeftTwoRight = new ExerciseLayout(5, nameof(OneLeftTwoRight).ToLowerInvariant());
        public static ExerciseLayout TwoLeftTwoRight = new ExerciseLayout(6, nameof(TwoLeftTwoRight).ToLowerInvariant());

        public static IEnumerable<ExerciseLayout> List() =>
            new[] { Single, TwoVertical, TwoHorizontal, TwoLeftOneRight, OneLeftTwoRight, TwoLeftTwoRight };

        public static ExerciseLayout FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ExerciseException($"Possible values for layout of an exercise: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static ExerciseLayout FromId(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ExerciseException($"Possible values for layout of an exercise: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static int GetNumberOfModulesAllowed(int id)
        {
            switch (id)
            {
                case 1:
                    return 1;
                case 2:
                case 3:
                    return 2;
                case 4:
                case 5:
                    return 3;
                case 6:
                    return 4;
                default:
                    throw new ExerciseLayoutException("The id does not correspond to a valid layout.");
            }
        }
    }
}
