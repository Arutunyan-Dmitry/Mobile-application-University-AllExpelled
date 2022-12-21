using UniversityAllExpelledExecutorBusinessLogic.BusinessLogic;
using UniversityAllExpelledExecutorContracts.BusinessLogicContracts;
using UniversityAllExpelledExecutorContracts.StorageContracts;
using UniversityAllExpelledDataBaseImplement.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using M6T.Core.TupleModelBinder;
using UniversityAllExpelledSuretyContracts.StorageContracts;
using UniversityAllExpelledSuretyContracts.BusinessLogicContracts;
using UniversityAllExpelledSuretyBusinessLogic.BusinessLogic;
using UniversityAllExpelledExecutorBusinessLogic.OfficePackage;
using UniversityAllExpelledExecutorBusinessLogic.OfficePackage.Implements;
using UniversityAllExpelledExecutorBusinessLogic.MailWorker;
using UniversityAllExpelledExecutorContracts.BindingModels;
using System;

namespace UniversityAllExpelledExecutorRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITeacherStorage, TeacherStorage>();
            services.AddTransient<IStudentStorage, StudentStorage>();
            services.AddTransient<ITestingStorage, TestingStorage>();
            services.AddTransient<IStatementStorage, StatementStorage>();
            services.AddTransient<ILessonStorage, LessonStorage>();
            services.AddTransient<IDisciplineStorage, DisciplineStorage>();

            services.AddTransient<ITeacherLogic, TeacherLogic>();
            services.AddTransient<IStudentLogic, StudentLogic>();
            services.AddTransient<ITestingLogic, TestingLogic>();
            services.AddTransient<IStatementLogic, StatementLogic>();
            services.AddTransient<ILessonLogic, LessonLogic>();

            services.AddTransient<AbstractSaveToWord, SaveToWord>();
            services.AddTransient<AbstractSaveToExcel, SaveToExcel>();
            services.AddTransient<AbstractSaveToPdf, SaveToPdf>();

            services.AddSingleton<AbstractMailWorker, MailKitWorker>();

            services.AddTransient<UniversityAllExpelledExecutorContracts.BusinessLogicContracts.IReportLogic, 
                UniversityAllExpelledExecutorBusinessLogic.BusinessLogic.ReportLogic>();

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UniversityAllExpelledExecutorRestApi", Version = "v1" });
            });
            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new TupleModelBinderProvider());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniversityAllExpelledExecutorRestApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var mailSender = app.ApplicationServices.GetService<AbstractMailWorker>();
            mailSender.MailConfig(new MailConfigBindingModel
            {
                MailLogin = Configuration?.GetSection("MailLogin")?.Value.ToString(),
                MailPassword = Configuration?.GetSection("MailPassword")?.Value.ToString(),
                SmtpClientHost = Configuration?.GetSection("SmtpClientHost")?.Value.ToString(),
                SmtpClientPort = Convert.ToInt32(Configuration?.GetSection("SmtpClientPort")?.Value.ToString()),
                PopHost = Configuration?.GetSection("PopHost")?.Value.ToString(),
                PopPort = Convert.ToInt32(Configuration?.GetSection("PopPort")?.Value.ToString())
            });
        }
    }
}
