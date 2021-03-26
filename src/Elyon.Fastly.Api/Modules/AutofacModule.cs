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

using Autofac;
using AutoMapper;
using Elyon.Fastly.Api.Settings;
using Elyon.Fastly.EmailJob.RestClient;
using Prime.Sdk.ConfigReader.ReloadingManager;
using Prime.Sdk.Logging;
using Prime.Sdk.RestClientFactory.Caching;

namespace Elyon.Fastly.Api.Modules
{
    public class AutofacModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public AutofacModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => 
                    ctx.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterModule(new DomainServices.AutofacModule(_appSettings.CurrentValue.JwtSettings.Key,
                _appSettings.CurrentValue.JwtSettings.Issuer, _appSettings.CurrentValue.JwtSettings.Audience,
                _appSettings.CurrentValue.FrontendSettings.BaseFrontendUrl, _appSettings.CurrentValue.EpaadSettings.BaseEpaadUrl,
                _appSettings.CurrentValue.EpaadSettings.EpaadEmail, _appSettings.CurrentValue.EpaadSettings.EpaadPassword));
            builder.RegisterModule(new PostgresRepositories.AutofacModule(_appSettings.CurrentValue.DbConnectionString,
                _appSettings.CurrentValue.CryptographySettings.EncryptionKey, _appSettings.CurrentValue.CryptographySettings.EncryptionIV));
            builder.RegisterLogging(_appSettings.CurrentValue.LoggerSettings);

            builder.RegisterType<RemovableAsyncCacheProvider>()
                .As<IRemovableAsyncCacheProvider>()
                .SingleInstance();

            builder.RegisterEmailJobClient(_appSettings.CurrentValue.EmailClientSettings, null);
        }
    }
}
