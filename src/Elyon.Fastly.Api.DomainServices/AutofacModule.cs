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
using Elyon.Fastly.Api.Domain.Services;

namespace Elyon.Fastly.Api.DomainServices
{
    public class AutofacModule : Module
    {
        private const string jwtKeyParamName = "jwtKey";
        private const string jwtIssuerParamName = "jwtIssuer";
        private const string jwtAudienceParamName = "jwtAudience";
        private const string baseFrontendUrlParamName = "baseFrontendUrl";
        private const string baseEpaadUrlParamName = "baseEpaadUrl";
        private const string epaadEmailParamName = "epaadEmail";
        private const string epaadPasswordParamName = "epaadPassword";

        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly string _baseFrontendUrl;
        private readonly string _baseEpaadUrl;
        private readonly string _epaadEmail;
        private readonly string _epaadPassword;

#pragma warning disable CA1054 // Uri parameters should not be strings
        public AutofacModule(string jwtKey, string jwtIssuer, string jwtAudience, string baseFrontendUrl,
            string baseEpaadUrl, string epaadEmail, string epaadPassword)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            _jwtKey = jwtKey;
            _jwtIssuer = jwtIssuer;
            _jwtAudience = jwtAudience;
            _baseFrontendUrl = baseFrontendUrl;
            _baseEpaadUrl = baseEpaadUrl;
            _epaadEmail = epaadEmail;
            _epaadPassword = epaadPassword;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersService>()
                .As<IUsersService>()
                .SingleInstance();

            builder.RegisterType<AuthorizeService>()
                .As<IAuthorizeService>()
                .SingleInstance()
                .WithParameter(jwtKeyParamName, _jwtKey)
                .WithParameter(jwtIssuerParamName, _jwtIssuer)
                .WithParameter(jwtAudienceParamName, _jwtAudience);

            builder.RegisterType<EmailSenderService>()
                .As<IEmailSenderService>()
                .SingleInstance()
                .WithParameter(baseFrontendUrlParamName, _baseFrontendUrl);

            builder.RegisterType<CitiesService>()
                .As<ICitiesService>()
                .SingleInstance();

            builder.RegisterType<OrganizationTypesService>()
                .As<IOrganizationTypesService>()
                .SingleInstance();

            builder.RegisterType<OrganizationsService>()
                .As<IOrganizationsService>()
                .SingleInstance();

            builder.RegisterType<TestingPersonnelsService>()
                .As<ITestingPersonnelsService>()
                .SingleInstance();

            builder.RegisterType<TestingPersonnelInvitationsService>()
                .As<ITestingPersonnelInvitationsService>()
                .SingleInstance();

            builder.RegisterType<EpaadService>()
                .As<IEpaadService>()
                .SingleInstance()
                .WithParameter(baseEpaadUrlParamName, _baseEpaadUrl)
                .WithParameter(epaadEmailParamName, _epaadEmail)
                .WithParameter(epaadPasswordParamName, _epaadPassword);

            builder.RegisterType<LamaCompaniesService>()
                .As<ILamaCompaniesService>()
                .SingleInstance();

            builder.RegisterType<OrganizationNotesService>()
                .As<IOrganizationNotesService>()
                .SingleInstance();

            builder.RegisterType<InfoSessionFollowUpService>()
                .As<IInfoSessionFollowUpService>()
                .SingleInstance();

            builder.RegisterType<AttachmentsSeedService>()
                .As<IAttachmentsSeedService>()
                .SingleInstance();
        }
    }
}
