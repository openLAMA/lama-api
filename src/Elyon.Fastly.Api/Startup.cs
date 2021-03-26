#region Copyright
// openLAMA is an open source platform which has been developed by the
// Swiss Kanton Basel Landschaft, with the goal of automating and managing
// large scale Covid testing programs or any other pandemic/viral infections.

// Copyright(C) 2021 Kanton Basel Landschaft, Switzerland
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// See LICENSE.md in the project root for license information.
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see https://www.gnu.org/licenses/.
#endregion

using AutoMapper;
using Elyon.Fastly.Api.DomainServices;
using Elyon.Fastly.Api.Helpers;
using Elyon.Fastly.Api.Settings;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Prime.Sdk.Configuration.AppBuilder;
using Prime.Sdk.Configuration.Services;
using Prime.Sdk.Swagger;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Elyon.Fastly.Api
{
    [UsedImplicitly]
    public class Startup
    {
        private const string AnyOriginCors = "AnyOriginCors";

        private readonly SwaggerOptions _swaggerOptions = new SwaggerOptions
        {
            ApiTitle = "Fastly Api",
            ApiVersion = "v1"
        };

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.UsePrimeServiceConfiguration<AppSettings>((options, appSettings) =>
            {
                options.Swagger = _swaggerOptions;
                options.Swagger.ConfigureSwagger = swagger =>
                {
                    swagger.IgnoreObsoleteActions();

                    swagger.AddSecurityDefinition("jwt_auth", new OpenApiSecurityScheme()
                    {
                        Name = "Bearer",
                        BearerFormat = "JWT",
                        Scheme = "bearer",
                        Description = "Specify the authorization token.",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                    });

                    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme()
                            {
                                Reference = new OpenApiReference()
                                {
                                    Id = "jwt_auth",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                };

                options.AdditionalServicesConfiguration = serv =>
                {
                    serv.AddJWTAuthentication(appSettings.JwtSettings);

                    serv.AddControllers()
                       .AddNewtonsoftJson(options =>
                       {
                           options.SerializerSettings.Converters.Add(new StringEnumConverter());
                       })
                       .AddJsonOptions(options =>
                       {
                           options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                       });

                    serv.AddCors(opts => opts.AddPolicy(AnyOriginCors, 
                        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
                    
                    serv.AddHttpClient();

                    serv.AddHostedService<EpaadHostedService>();
                };

                options.EnableFluentValidation = true;
            });
        }

        [UsedImplicitly]
        [SuppressMessage("Microsoft.Performance", "CA1822", Justification = "Standard Startup method")]
        public void Configure(IApplicationBuilder app, IMapper mapper)
        {
            app.UsePrimeAppBuilderConfiguration(options =>
            {
                options.WithMiddleware = x =>
                {
                    x.UseRouting();
                    x.UseAuthentication();
                    x.UseCors(AnyOriginCors);
                    x.UseAuthorization();
                    x.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                };
            });

            mapper?.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
