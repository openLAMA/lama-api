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
using Elyon.Fastly.Api.Domain;
using Elyon.Fastly.Api.Domain.Repositories;
using Elyon.Fastly.Api.PostgresRepositories.Helpers;
using Prime.Sdk.PostgreSql;

namespace Elyon.Fastly.Api.PostgresRepositories
{
    public class AutofacModule : Module
    {
        private const string encryptionKeyParamName = "encryptionKey";
        private const string encryptionIVParamName = "encryptionIV";

        private readonly string _connectionString;
        private readonly string _encryptionKey;
        private readonly string _encryptionIV;

        public AutofacModule(string connectionString, string encryptionKey, string encryptionIV)
        {
            _connectionString = connectionString;
            _encryptionKey = encryptionKey;
            _encryptionIV = encryptionIV;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterPostgreSql(
                _connectionString,
                connString => new ApiContext(connString, false), 
                dbConn => new ApiContext(dbConn));

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile(ctx.Resolve<IAESCryptography>()));
            }));
            
            builder.RegisterType<UsersRepository>()
                .As<IUsersRepository>()
                .SingleInstance();

            builder.RegisterType<UserConfirmationTokensRepository>()
                .As<IUserConfirmationTokensRepository>()
                .SingleInstance();

            builder.RegisterType<AESCryptography>()
                .As<IAESCryptography>()
                .SingleInstance()
                .WithParameter(encryptionKeyParamName, _encryptionKey)
                .WithParameter(encryptionIVParamName, _encryptionIV);

            builder.RegisterType<CitiesRepository>()
               .As<ICitiesRepository>()
               .SingleInstance();

            builder.RegisterType<OrganizationTypesRepository>()
               .As<IOrganizationTypesRepository>()
               .SingleInstance();

            builder.RegisterType<OrganizationsRepository>()
               .As<IOrganizationsRepository>()
               .SingleInstance();

            builder.RegisterType<SupportPersonOrgTypeDefaultRepository>()
               .As<ISupportPersonOrgTypeDefaultRepository>()
               .SingleInstance();

            builder.RegisterType<TestingPersonnelStatusesRepository>()
               .As<ITestingPersonnelStatusesRepository>()
               .SingleInstance();

            builder.RegisterType<TestingPersonnelsRepository>()
               .As<ITestingPersonnelsRepository>()
               .SingleInstance();

            builder.RegisterType<TestingPersonnelInvitationsRepository>()
               .As<ITestingPersonnelInvitationsRepository>()
               .SingleInstance();

            builder.RegisterType<TestingPersonnelInvitationConfirmationTokensRepository>()
               .As<ITestingPersonnelInvitationConfirmationTokensRepository>()
               .SingleInstance();

            builder.RegisterType<TestingPersonnelConfirmationsRepository>()
               .As<ITestingPersonnelConfirmationsRepository>()
               .SingleInstance();

            builder.RegisterType<LamaCompaniesRepository>()
               .As<ILamaCompaniesRepository>()
               .SingleInstance();

            builder.RegisterType<OrganizationNotesRepository>()
                .As<IOrganizationNotesRepository>()
                .SingleInstance();

            builder.RegisterType<InfoSessionFollowUpRepository>()
                .As<IInfoSessionFollowUpRepository>()
                .SingleInstance();

            builder.RegisterType<AttachmentsSeedRepository>()
                .As<IAttachmentsSeedRepository>()
                .SingleInstance();
        }
    }
}
