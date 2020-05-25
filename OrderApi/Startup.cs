using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.MultiBus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OrderApi.Data;
using RabbitMQ.Client;

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
            //this our db portion
            //for post api's we need to use this newtonsjson nuget package and if we did not use this error will appear http was not found
            services.AddControllers().AddNewtonsoftJson();
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
            services.AddDbContext<OrdersContext>(options => options.UseSqlServer(connectionstring));

            //this is for authentication
            ConfigureAuthService(services);


            //this is fo swagger
            //adding swagger to generate swagger documentation for us and adding options to the swagger
            services.AddSwaggerGen(options =>
            {
                //swagger document it is we can tell what version our application.This is name of our version (V1)
                options.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    //title of our documentation
                    Title = "JewelsonContainer - Order Api",
                    //this is the actual version
                    Version = "v1",
                    //description for my documentation
                    Description = "Order service API",
                });
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                //for security authentication
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    //type of flow of our identity.implicit is the way to talk to identity server to confirm the token
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            //this is the url if you need to authorize
                            AuthorizationUrl = new Uri($"{Configuration["IdentityUrl"]}/connect/authorize"),
                            //this is the url to identify the token
                            TokenUrl = new Uri($"{Configuration["IdentityUrl"]}/connect/token"),
                            //who is going to be calling this
                            Scopes = new Dictionary<string, string>
                            {
                                //basket name should match in the token server config file left side name
                                {"order", "Order Api"}

                            }


                        }
                    }

                });
            });

            //this is for messaging
            //adding configuration to our mass transit
            services.AddMassTransit(cfg =>
            {
                //addinga bus to configuration
                cfg.AddBus(provider =>
                {
                    //creating the rabbit mq. Factory is inbuilt class we are using that to create rabbitmq
                    return Bus.Factory.CreateUsingRabbitMq(rmq =>
                    {
                        //rabbit mq is hosted in "rabbitmq://rabbitmq"
                        rmq.Host(new Uri("rabbitmq://rabbitmq"), "/", h =>
                        {
                            //rabbit mq is created using below username and password
                           
                            h.Username("guest");
                            h.Password("guest");
                        });
                        //here we are going to tell our mesage is fanout msg like publish subscribe model
                        //fanout to send the messages in distribution manner
                        rmq.ExchangeType = ExchangeType.Fanout;
                        //etratiometolive is to stay there our message for a day.
                        MessageDataDefaults.ExtraTimeToLive = TimeSpan.FromDays(1);
                    });

                });
            });
            //this is the one kicks start our bus. it gets started and running
            services.AddMassTransitHostedService();
        }

        private void ConfigureAuthService(IServiceCollection services)
        {
            //how we integrate identity with an application
            //we want to know identity server is, it located in configuration
            var identityUrl = Configuration["identityUrl"];
            //we need to add authentication
            services.AddAuthentication(options =>
            {
                // jwtbearer is a token askng about identity
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // challenge is for askng for user name and password for identity
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //telling where the identity server is located
            }).AddJwtBearer(options =>
            {
                //identityurl is issue the token
                options.Authority = identityUrl;
                //for security
                options.RequireHttpsMetadata = false;
                //we need to tell who you are. the name of the auience should match in our identity server
                options.Audience = "order";
            });
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
