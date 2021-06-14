using BookingApi.Messaging.Send.Options.v1;
using BookingApi.Messaging.Send.Sender.v1;
using BookingApi.Service.v1.Command;
using BookingApi.Service.v1.Query;
using CalendarApi.Data.Database;
using CalendarApi.Data.Repository;
using CalendarApi.Domain.Models.DTOs;
using CalendarApi.Domain.Models.Entities;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BookingApi
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
            services.AddHealthChecks();
            services.AddOptions();

            bool.TryParse(Configuration["BaseServiceSettings:UseInMemoryDatabase"], out var useInMemory);

            if (!useInMemory)
            {
                services.AddDbContext<CalendarDBContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("CalendarDbConnection"));
                });
            }
            else
            {
                services.AddDbContext<CalendarDBContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            }

            // DBContext
            services.AddTransient<CalendarDBContext>();

            // unitOfWork layer
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Booking Api",
                    Description = "A simple API to create or update booking",
                    Contact = new OpenApiContact
                    {
                        Name = "Julien Bassin",
                        Email = "julienbassin@outlook.com",
                        Url = new Uri("https://github.com/julienbassin")
                    }
                });
            });

            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.InvalidModelStateResponseFactory = actionContext =>
            //    {
            //        var actionExecutingContext =
            //            actionContext as ActionExecutingContext;

            //        if (actionContext.ModelState.ErrorCount > 0
            //            && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
            //        {
            //            return new UnprocessableEntityObjectResult(actionContext.ModelState);
            //        }

            //        return new BadRequestObjectResult(actionContext.ModelState);
            //    };
            //});

            services.AddMediatR(Assembly.GetExecutingAssembly());
            bool.TryParse(Configuration["BaseServiceSettings:UserabbitMq"], out var useRabbitMq);

            if (useRabbitMq)
            {
                services.AddSingleton<IAppointmentUpdateSender, AppointmentUpdateSender>();
            }

            services.AddTransient<IRequestHandler<CreateAppointmentCommand, AppointmentDTO>, CreateAppointmentCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateAppointmentCommand, AppointmentDTO>, UpdateAppointmentCommandHandler>();
            services.AddTransient<IRequestHandler<GetAppointmentByIdQuery, AppointmentModel>, GetAppointmentByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetAppointmentsQuery, List<AppointmentModel>>, GetAppointmentsQueryHandler>();
            services.AddTransient<IRequestHandler<GetConsultantsQuery, List<ConsultantModel>>, GetConsultantsQueryHandler>();
            services.AddTransient<IRequestHandler<GetConsultantByIdQuery, ConsultantModel>, GetConsultantByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetPatientsQuery, List<PatientModel>>, GetPatientsQueryHandler>();
            services.AddTransient<IRequestHandler<GetPatientByIdQuery, PatientModel>, GetPatientByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetTimeSlotsQuery, List<TimeSlotModel>>, GetTimeSlotsQueryHandler>();
            services.AddTransient<IRequestHandler<GetTimeSlotByIdQuery, TimeSlotModel>, GetTimeSlotByIdQueryHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookingApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
