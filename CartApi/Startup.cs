using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartApi.Messaging.Consumers;
using CartApi.Models;
using Common.Messaging;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using StackExchange.Redis;

namespace CartApi
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
            //injecting cart servce 
            services.AddTransient<ICartRepository, RedisCartRepository>();

            services.AddSingleton<ConnectionMultiplexer>(cm =>
            {
                //connectionmultiplexeroptions is like dbcontextoptions
                //we need to connect with multiplexer so we are using this line for connectionstring
                var configuration = ConfigurationOptions.Parse(Configuration["ConnectionString"], true);
                //resolve dns is domain name for ip address
                configuration.ResolveDns = true;
                //if something failed we dont want to abort we dont want  that so we are making as false
                configuration.AbortOnConnectFail = false;
                //we are connecting to the redishcche through connectionstring
                return ConnectionMultiplexer.Connect(configuration);
            });



            //we are creating our own private method because everythng should not mess up with services
            ConfigureAuthService(services);

            //adding swagger to generate swagger documentation for us and adding options to the swagger
            services.AddSwaggerGen(options =>
            {
                //swagger document it is we can tell what version our application.This is name of our version (V1)
                options.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    //title of our documentation
                    Title = "EventsOnContainer - Basket Api",
                    //this is the actual version
                    Version = "v1",
                    //description for my documentation
                    Description = "Basket service API",
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
                                {"basket", "Basket Api"}

                            }


                        }
                    }
                });
            });

            //this is for messaging
            //adding configuration to our mass transit
            services.AddMassTransit(cfg =>
            {
                //when I receive a message we need to tell that i am a consumer
                cfg.AddConsumer<OrderCompletedEventConsumer>();
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

                        //eventscartapr20 is my queue name
                        rmq.ReceiveEndpoint("EventscartApr20", e =>
                        {
                            //whenever we receive message we need to call that class
                            e.ConfigureConsumer<OrderCompletedEventConsumer>(provider);
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
                options.Audience = "basket";
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
            //for authentication
            app.UseAuthentication();
            app.UseSwagger().UseSwaggerUI(e =>
            {
                //we will show under this url for swagger, version v1 and name for this file as product catalogapi
                e.SwaggerEndpoint($"/swagger/V1/swagger.json", "EventCatalogAPI V1");
            });



            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
