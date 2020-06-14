using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuiZone.API.Filters;
using QuiZone.API.Utils.Helpers;
using QuiZone.BusinessLogic.Services;
using QuiZone.BusinessLogic.Services.Base;
using QuiZone.BusinessLogic.Services.Interfaces;
using QuiZone.Common.LoggerService;
using QuiZone.DataAccess;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Models.Mapping;
using QuiZone.DataAccess.Repository.Implemented;
using QuiZone.DataAccess.Repository.Interfaces;
using QuiZone.DataAccess.UnitOfWork;

namespace QuiZone.API.Extensions
{
    public static class ServiceExtension
    {

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<QuiZoneContext>(options =>
             options.UseSqlServer(
                 @ConfigurationManager.GetAppSettingsValue("ConnectionStrings:Local")));
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IQuestionService, QuestionService>();

            services.AddScoped<ICrudService<UserDTO, User>, UserService>(); 


        }

        public static void ConfigureDatabaseManagement(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionCorrectAnswerRepository, QuestionCorrectAnswerRepository>();
            services.AddScoped<IQuestionOptionsAnswerRepository, QuestionOptionsAnswerRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TokenProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new AnswerProfile());
                mc.AddProfile(new QuizProfile());
                mc.AddProfile(new QuizSettingProfile());
                mc.AddProfile(new QuestionProfile());
                mc.AddProfile(new CorrectAnswerProfile());
                mc.AddProfile(new OptionsAnswerProfile());
            }).CreateMapper());
        }

        public static void ConfigureFilters(this IServiceCollection services)
        {
            services.AddScoped<UpdateExceptionFilterAttribute>();
            services.AddScoped<DeleteExceptionFilterAttribute>();
        }

    }
}
