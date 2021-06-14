using CalendarApi.Data.Config;
using CalendarApi.Data.Database;
using CalendarApi.Data.Interfaces;
using CalendarApi.Data.Repository;
using CalendarApi.Messaging.Receive.Options.v1;
using CalendarApi.Messaging.Receive.Receiver.v1;
using CalendarApi.Service.v1.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarApi
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

            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            var serviceClientSettings = serviceClientSettingsConfig.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

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

            // Data Repository
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IAppointmentUpdateService, AppointmentUpdateService>();
            services.AddTransient<IAppointmentDataHandler, AppointmentDataHandler>();

            services.AddHostedService<AppointmentUpdateReceiver>();

            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Calendar Api",
                    Description = "A simple API to create or update appointment",
                    Contact = new OpenApiContact
                    {
                        Name = "Julien Bassin",
                        Email = "julienbassin@outlook.com",
                        Url = new Uri("https://github.com/julienbassin")
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CalendarApi v1"));
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
