using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using OrderApi.Data;

namespace OrderApi
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
            services.AddControllers();
            // we changed connection string we took it away and we changed it to the paramaters in the yml file
            //we are defining those here as parameters in startuo so if we run docker then it will take these config
            var server = Configuration["DataBaseServer"];
            //database name
            var database = Configuration["DataBaseName"];
            //username
            var user = Configuration["DataBaseUser"];
            //password
            var password = Configuration["DataBasePassword"];
            //total connection string for sql server
            var connectionstring = $"Server = {server};DataBase = {database};User Id = {user}; Password = {password}";

            //var connectionString = Configuration["ConnectionString"];

            //adding data base context and giving options method to connect my data base through the configuration
            //services.AddDbContext<OrdersContext>(options => options.UseSqlServer(connectionstring));

            //ConfigureAuthService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
