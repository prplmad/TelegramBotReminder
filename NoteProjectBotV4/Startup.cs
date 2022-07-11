using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataStore;
using Microsoft.EntityFrameworkCore;
using DataStore.Repository;
using Services;
using Business.Abstract.Services;
using Business.Abstract.Repositories;
using DataStore.Entities;
using Business.Abstract.Wrappers;
using Business;
using Business.Services;

namespace NoteBot
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(_configuration.GetConnectionString("Db")), ServiceLifetime.Singleton);
            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton<TelegramBot>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<INotesRepository, NotesRepository>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<INotesService, NotesService>();
            services.AddTransient<IRemindsService, RemindsService>();
            services.AddTransient<IRemindsRepository, RemindsRepository>();
            services.AddTransient<IStatesService, StatesService>();
            services.AddTransient<IStatesRepository, StatesRepository>();
            services.AddTransient<NoteEntity, NoteEntity>();
            services.AddTransient<IBotControllerService, BotControllerService>();
            services.AddTransient<IBotControllerServiceAdditionalMethods, BotControllerServiceAdditionalMethods>();
            services.AddTransient<ITelegramBotService, TelegramBotService>();
            services.AddTransient<ITelegramBotClientWrapper, TelegramBotClientWrapper>();
            services.AddTransient<IMessageWrapper, MessageWrapper>();
            services.AddTransient<IChooseStateAdditionalMethods, ChooseStateAdditionalMethods>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
