using AutoMapper;
using P7WebApp.Application.ExerciseCQRS.Commands.CreateSolution;
using P7WebApp.Application.ExerciseCQRS.Commands.CreateSubmission;
using P7WebApp.Application.ExerciseCQRS.Commands.DeleteSubmission;
using P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.CodeModule;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.QuizModule;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.TextModule;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule;

namespace P7WebApp.Application.Common.Mappings.Profiles
{
    public class ExerciseMappingProfile : Profile
    {
        public ExerciseMappingProfile() 
        { 
            CreateMap<UpdateExerciseCommand, Exercise>();
            CreateMap<CreateSolutionCommand, Solution>();
            CreateMap<CreateSubmissionCommand, Submission>();
            CreateMap<DeleteSubmissionCommand, Submission>();
            CreateMap<Exercise, ExerciseOverviewResponse>();
            CreateMap<Exercise, ExerciseResponse>();
            CreateMap<CreateExerciseCommand, Exercise>()
                .ForMember(dest => dest.Modules, src => src.MapFrom(src => src.Modules));
            CreateMap<CreateModuleCommand, Module>();
            CreateMap<CreateCodeEditorModuleCommand, CodeEditorModule>();
            CreateMap<CreateTextModuleCommand, TextModule>();
            CreateMap<CreateQuizModuleCommand, QuizModule>();
            CreateMap<CreateModuleCommand, Module>()
                .Include<CreateCodeEditorModuleCommand, CodeEditorModule>()
                .Include<CreateTextModuleCommand, TextModule>()
                .Include<CreateQuizModuleCommand, QuizModule>();
        }
    }
}