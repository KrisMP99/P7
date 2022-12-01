using AutoMapper;
using P7WebApp.Application.ExerciseCQRS.Commands.CreateSolution;
using P7WebApp.Application.ExerciseCQRS.Commands.CreateSubmission;
using P7WebApp.Application.ExerciseCQRS.Commands.DeleteSubmission;
using P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise;
using P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.CodeModule;
using P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.Module;
using P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.QuizModule;
using P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.TextModule;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.CodeModule;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.QuizModule;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.TextModule;
using P7WebApp.Application.Responses;
using P7WebApp.Application.Responses.Modules;
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

            // creation of exercise
            CreateMap<CreateExerciseCommand, Exercise>()
                .ForMember(dest => dest.Modules, src => src.MapFrom(src => src.Modules));
            CreateMap<CreateCodeEditorModuleCommand, CodeEditorModule>();
            CreateMap<CreateTextModuleCommand, TextModule>();
            CreateMap<CreateQuizModuleCommand, QuizModule>();
            CreateMap<CreateModuleCommand, Module>()
                .Include<CreateCodeEditorModuleCommand, CodeEditorModule>()
                .Include<CreateTextModuleCommand, TextModule>()
                .Include<CreateQuizModuleCommand, QuizModule>();

            // update of exercises
            CreateMap<UpdateCodeEditorModuleCommand, CodeEditorModule>();
            CreateMap<UpdateTextModuleCommand, TextModule>();
            CreateMap<UpdateQuizModuleCommand, QuizModule>();
            CreateMap<UpdateModuleCommand, Module>()
                .Include<UpdateCodeEditorModuleCommand, CodeEditorModule>()
                .Include<UpdateTextModuleCommand, TextModule>()
                .Include<UpdateQuizModuleCommand, QuizModule>();

            // get exercise
            CreateMap<Exercise, ExerciseResponse>()
                .ForMember(dest => dest.Modules, src => src.MapFrom(src => src.Modules));
            CreateMap<CodeEditorModule, CodeEditorModuleResponse>();
            CreateMap<TextModule, TextModuleResponse>();
            CreateMap<QuizModule, QuizModuleResponse>();
            CreateMap<Module, ModuleResponse>()
                .Include<CodeEditorModule, CodeEditorModuleResponse>()
                .Include<TextModule, TextModuleResponse>()
                .Include<QuizModule, QuizModuleResponse>();
        }
    }
}